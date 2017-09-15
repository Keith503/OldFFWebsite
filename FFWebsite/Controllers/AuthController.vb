Imports System.Web.Http
Imports System.Security.Claims
Imports Newtonsoft.Json


Public Class AuthController
    Inherits ApiController

    'this temporary class is only used to pass the userid and password via a post in JSON
    Class tempUser
        Private m_UserID As String
        Public Property UserID() As String
            Get
                Return m_UserID
            End Get
            Set(ByVal value As String)
                m_UserID = value
            End Set
        End Property

        Private m_Password As String
        Public Property Password() As String
            Get
                Return m_Password
            End Get
            Set(ByVal value As String)
                m_Password = value
            End Set
        End Property
    End Class

    Public Function Login(<FromBody()> ByVal value As String) As IHttpActionResult
        '-----------------------------------------------------------------------------
        'Function:  Login
        'Purpose:   Validate User ID and Password and create Security Cookie 
        'Input:     User ID and Password 
        'Returns:   Boolean True-Valid USer, False-Invalid User 
        'Comments:  The following are examples of how this all works.
        '           https://www.codeproject.com/Articles/751897/ASP-NET-Identity-with-webforms
        '           http://www.aspsnippets.com/Articles/Simple-User-Login-Form-example-in-ASPNet.aspx
        '           create the claim to go into the cookie.  
        '           future calls can retrieve the claim information from the cookie
        '           data such as roles can be 'carried' in the cookie to verify what the person
        '           has access to
        '-----------------------------------------------------------------------------
        Dim obj As tempUser
        Dim bolIsValid As Boolean = False
        Dim m_cFFServer As New cFFWebSiteServer

        'Convert into object from JSON 
        obj = JsonConvert.DeserializeObject(Of tempUser)(value)

        'Go check credentials with the server 
        Try
            bolIsValid = m_cFFServer.ValidateUserID(obj.UserID, obj.Password)
        Catch ex As Exception
            bolIsValid = False
        End Try

        'see above URLs for examples of what can be carried in claim
        If bolIsValid Then
            Dim claims = New List(Of Claim)()
            claims.Add(New Claim(ClaimTypes.Name, obj.UserID))
            claims.Add(New Claim(ClaimTypes.NameIdentifier, obj.UserID))
            'claims.Add(New Claim(ClaimTypes.Email, "brockallen@gmail.com"))
            Dim id = New ClaimsIdentity(claims, "ApplicationCookie")

            'use owin to create cookie/token to be used for REST functions that are protected
            Dim authenticationManager = HttpContext.Current.GetOwinContext().Authentication
            authenticationManager.SignIn(New Microsoft.Owin.Security.AuthenticationProperties() With {
                    .IsPersistent = True
                    }, id)

            '.ExpiresUtc = DateTime.UtcNow.AddHours(.5)
        End If

        Return Ok("logged in")
    End Function

    'this function will log the person off and destroy the cookie
    <HttpGet>
    Public Function Logout() As IHttpActionResult
        Dim authenticationManager = HttpContext.Current.GetOwinContext().Authentication
        'Dim authenticationManager = ctx.Authentication
        authenticationManager.SignOut()
        Return Ok("logged off")
    End Function

End Class

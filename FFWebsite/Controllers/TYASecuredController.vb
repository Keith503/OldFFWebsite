Imports System.Net
Imports System.Web.Http

'This is what secures this class - do not remove it 
'<Authorize>
Public Class TYASecuredController
    Inherits ApiController
    <HttpGet>
    Public Function TestSecurity() As IHttpActionResult
        Return Ok("It Works!")
    End Function


End Class

Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class FrogforceController
        Inherits ApiController

        Public Function GetNewsList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)
            Try
                NewsList = m_cFFServer.GetNewsItemList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetNewsList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetEventList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventList As New List(Of cEventItem)

            Try
                EventList = m_cFFServer.GetEventItemList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetEventList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventList)
        End Function

        Public Function TestGetEventList() As IHttpActionResult
            Dim EventList As New List(Of cEventItem)
            Return Ok(EventList)
        End Function

        Private Function BuildErrorMsg(ByVal strFunctionName As String, strThrownError As String) As String
            Dim strErrMsg As String
            Dim mControllerName As String = "api/FFController/"
            strErrMsg = mControllerName & strFunctionName & " failed! Return Code: " & strThrownError

            'logger.Error(strErrMsg)

            Return strErrMsg
        End Function
    End Class

End Namespace
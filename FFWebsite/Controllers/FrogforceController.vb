Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json

Namespace Controllers
    Public Class FrogforceController
        Inherits ApiController

        Public Function GetNewsItemList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cTypeItem)

            Try
                NewsList = m_cFFServer.GetNewsItemList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetNewsItemList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetNewsItem(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsItem As New cNewsItem
            Try
                NewsItem = m_cFFServer.GetNewsItembyID(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetNewsList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsItem)
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

        Public Function GetCategoryList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim CategoryList As New List(Of cTypeItem)

            Try
                CategoryList = m_cFFServer.GetCategoryList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetCategoryList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(CategoryList)
        End Function

        Public Function GetStatusList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim StatusList As New List(Of cTypeItem)

            Try
                StatusList = m_cFFServer.GetStatusList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetStatusList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(StatusList)
        End Function
        Public Function GetPriorityList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim PriorityList As New List(Of cTypeItem)

            Try
                PriorityList = m_cFFServer.GetPriorityList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetPriorityList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(PriorityList)
        End Function

        Public Function GetAuthorList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim AuthorList As New List(Of cTypeItem)

            Try
                AuthorList = m_cFFServer.GetAuthorList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetAuthorList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(AuthorList)
        End Function

        Public Function UpdateNewsItem(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cNewsItem
            Dim m_cFFServer As New cFFWebSiteServer
            Try
                obj = JsonConvert.DeserializeObject(Of cNewsItem)(value)
                m_cFFServer.UpdateNewsItem(obj)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdateNewsItem", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function GetNewsCarouselList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)

            Try
                NewsList = m_cFFServer.GetNewsCarouselList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetNewsCarouselList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetTopNewsItems(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)

            Try
                NewsList = m_cFFServer.GetTopNewsItems(CInt(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTopNewsItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetTopNewsItemsNoDups(ByVal id As String) As IHttpActionResult
            'ID = news item to ignore 
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)

            Try
                NewsList = m_cFFServer.GetTopNewsItems(5, CInt(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTopNewsItemsNoDups", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetNewsPage(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)

            Try
                NewsList = m_cFFServer.GetNewsPage(CInt(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTopNewsItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetEventItems() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventList As New List(Of cEventItem)

            Try
                EventList = m_cFFServer.GetEventList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetEventItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventList)
        End Function

        Public Function GetAllEventItems() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventList As New List(Of cEventItem)

            Try
                EventList = m_cFFServer.GetAllEventList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetEventItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventList)
        End Function


        Public Function GetEventSingleItem() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventItem As New cEventItem
            Try
                EventItem = m_cFFServer.GetEventItembyID()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetEventItem", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventItem)
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
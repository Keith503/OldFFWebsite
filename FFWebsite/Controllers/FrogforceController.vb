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
                Dim errmsg As String = BuildErrorMsg("GetNewsPage", ex.Message.ToString)
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


        Public Function GetEventSingleItem(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventItem As New cEventItem
            Dim ItemID As Integer
            Try
                ItemID = CInt(id)
                EventItem = m_cFFServer.GetEventItembyID(ItemID)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetEventItem", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventItem)
        End Function
        Public Function GetRelatedItems(ByVal id As String) As IHttpActionResult
            'ID = news item to ignore 
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)
            Dim NewsItem As New cNewsItem

            Try
                'First go get the main news iteam and obtain the category id 
                NewsItem = m_cFFServer.GetNewsItembyID(CLng(id))
                NewsList = m_cFFServer.GetRelatedItems(3, NewsItem.Category_ID, CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetRelatedItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function

        Public Function GetRelatedbyCategoryItems(ByVal id As String) As IHttpActionResult
            'ID = news item to ignore 
            Dim m_cFFServer As New cFFWebSiteServer
            Dim NewsList As New List(Of cNewsItem)

            Try
                'First go get nes items related to this category  
                NewsList = m_cFFServer.GetRelatedByCategoryItems(4, CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetRelatedbyCategoryItems", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(NewsList)
        End Function
        '---------------------------------------------------------------------------------------------------
        '  Scouting Functions  
        '---------------------------------------------------------------------------------------------------
        Public Function GetScoutingEventList() As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim EventList As New List(Of cEventItem)

            Try
                EventList = m_cFFServer.GetScoutingEventList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetScoutingEventList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(EventList)
        End Function

        Public Function GetScoutingMatchList(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim MatchList As New List(Of cMatchItem)

            Try
                MatchList = m_cFFServer.GetScoutingMatchList(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetScoutingMatchList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(MatchList)
        End Function

        Public Function GetScoutingScoresforTeam(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim MatchList As New List(Of cMatchItem)
            Dim lEventID As Long = 2
            Dim ScoreList As New List(Of cAllianceScore)

            Try
                'first go get a list of matches that this team has done 
                MatchList = m_cFFServer.GetScoutingMatchesforTeam(lEventID, CLng(id))
                ScoreList = m_cFFServer.GetScoutingScoresforTeam(lEventID, CLng(id), MatchList)

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetScoutingMatchList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(ScoreList)
        End Function

        Public Function GetTeamsInMatch(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim MatchList As New List(Of cMatchTeams)

            'Id = schedule id  

            Try
                'first go get a list of matches that this team has done 
                MatchList = m_cFFServer.GetTeamsinMatch(CLng(id))

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTeamsinMatch", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(MatchList)
        End Function

        Public Function GetGearRanking(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim GearList As New List(Of cGearStats)

            'Id = event id   
            Try
                'first go get a list of matches that this team has done 
                GearList = m_cFFServer.GetGearRanking(CLng(id))

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetGearRanking", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(GearList)
        End Function

        Public Function GetClimbRanking(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim ClimbList As New List(Of cClimbStats)

            'Id = event id   
            Try
                'first go get a list of matches that this team has done 
                ClimbList = m_cFFServer.GetClimbRanking(CLng(id))

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetClimbRanking", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(ClimbList)
        End Function
        Public Function GetScoutingDump(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim ScoutList As New List(Of cTabletDataFixed)

            'Id = event id   
            Try
                'first go get a list of matches that this team has done 
                ScoutList = m_cFFServer.DumpScoutingData(CLng(id))

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetScoutingDump", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(ScoutList)
        End Function
        Public Function GetShootingRanking(ByVal id As String) As IHttpActionResult
            Dim m_cFFServer As New cFFWebSiteServer
            Dim ClimbList As New List(Of cClimbStats)

            'Id = event id   
            Try
                'first go get a list of matches that this team has done 
                ClimbList = m_cFFServer.GetClimbRanking(CLng(id))

            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetClimbRanking", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(ClimbList)
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
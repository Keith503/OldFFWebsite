Imports MySql.Data.MySqlClient
Imports System.Threading.Tasks

'   Class Name:      cFFWebSiteServer 
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object provides database services for the Frog Force 503 Website     
'   Change history:

Public Class cFFWebSiteServer
    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public sServerName As String = "cFFWebSiteServer"

    Public Function GetNewsItemList() As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetPriorityItemList
        'Purpose:	return list of priority items       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "SELECT ID, Title_Text from FFWebsite.News"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TypeRow As New cTypeItem
                With TypeRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TypeRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetPriorityList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetNewsItembyID(ByVal lNewsID As Long) As cNewsItem
        '---------------------------------------------------------------------------------------
        'Function:	GetNewsItembyID
        'Purpose:	return single news item for a given id        
        'Input:     ID of news item to read  
        'Returns:   Returns new item object 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim NewsRow As New cNewsItem

        strSQL = "SELECT N.ID, N.Post_date, N.Priority_ID, N.Author_ID, N.Category_ID, N.Status_ID " &
                 ",N.LastUpdate_date, N.LastUpdate_ID, N.Approved_ID, N.Approved_Date, N.Image1_Name " &
                 ",N.Image2_Name, N.Image3_Name,N.Title_text,N.Body_text " &
                 ",C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author " &
                 " From FFWebsite.News N " &
                 " Left outer join FFWebsite.Users U on N.Author_ID = U.ID " &
                 " Left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " Where N.ID = " & lNewsID.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Post_Date = TestNullDate(dr, 1)
                    .Priority_ID = TestNullLong(dr, 2)
                    .Author_ID = TestNullLong(dr, 3)
                    .Category_ID = TestNullLong(dr, 4)
                    .Status_ID = TestNullLong(dr, 5)
                    .LastUpdate_Date = TestNullDate(dr, 6)
                    .LastUpdate_ID = TestNullLong(dr, 7)
                    .Approved_ID = TestNullLong(dr, 8)
                    .Approved_Date = TestNullDate(dr, 9)
                    .Image1_Name = TestNullString(dr, 10)
                    .Image2_Name = TestNullString(dr, 11)
                    .Image3_Name = TestNullString(dr, 12)
                    .Title_text = TestNullString(dr, 13)
                    .Body_text = TestNullString(dr, 14)
                    .Category_Name = TestNullString(dr, 15)
                    .Author_Name = TestNullString(dr, 16)
                End With
            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetNewsItemList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return NewsRow

    End Function

    Public Function GetNewsCarouselList() As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetNewsCarouselList
        'Purpose:	return list of top 10 news items for carousel        
        'Input:     Nothing 
        'Returns:   list of cNewsItem objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB

        strSQL = "Select id, Title_text, post_date, Image1_Name, Body_Text from FFWebsite.News " &
                  " where Status_ID = 4 " &
                  " order by post_date desc " &
                  " LIMIT 10"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Post_Date = TestNullDate(dr, 2)
                    .Image1_Name = TestNullString(dr, 3)
                    .Body_text = TestNullString(dr, 4)
                End With

                details.Add(NewsRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetNewsCarouselList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function


    Public Function GetTopNewsItems(ByVal itemCount As Integer, Optional ByVal ignoreItem As Integer = 0) As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetTop3NewsItems
        'Purpose:	return list of top n news items for index page         
        'Input:     The number of items to return  
        'Returns:   list of cNewsItem objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim strSelect As String

        'If optional parm is submmitted - ignore that news id 
        If ignoreItem = 0 Then
            strSelect = ""
        Else
            strSelect = " and N.id <> " + ignoreItem.ToString
        End If

        strSQL = "Select N.id, N.Title_text, N.post_date, C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author, N.Image1_Name, N.Image2_Name, mid(N.Body_Text,1,150) " &
                 " from FFWebsite.News N " &
                 " left outer join FFWebsite.Users U on N.Author_ID = U.ID " &
                 " left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " where N.Status_ID = 4 " & strSelect &
                 " order by N.post_date desc " &
                 " LIMIT " + itemCount.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Post_Date = TestNullDate(dr, 2)
                    .Category_Name = TestNullString(dr, 3)
                    .Author_Name = TestNullString(dr, 4)
                    .Image1_Name = TestNullString(dr, 5)
                    .Image2_Name = TestNullString(dr, 6)
                    .Body_text = TestNullString(dr, 7)
                End With

                details.Add(NewsRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetNewsCarouselList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetNewsPage(ByVal itemID As Integer) As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetNewsPage
        'Purpose:	return list of 12 items starting with given item number 
        'Input:     Item number to begin read 
        'Returns:   list of cNewsItem objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB

        strSQL = "Select N.id, N.Title_text, N.post_date, C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author, N.Image1_Name, mid(N.Body_Text,1,150), N.Image2_Name " &
                 " from FFWebsite.News N " &
                 " left outer join FFWebsite.Users U on N.Author_ID = U.ID " &
                 " left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " where N.Status_ID = 4 " &
                 "  and N.id > " + itemID.ToString &
                 " order by N.post_date desc " &
                 " LIMIT 12"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Post_Date = TestNullDate(dr, 2)
                    .Category_Name = TestNullString(dr, 3)
                    .Author_Name = TestNullString(dr, 4)
                    .Image1_Name = TestNullString(dr, 5)
                    .Body_text = TestNullString(dr, 6)
                    .Image2_Name = TestNullString(dr, 7)

                End With

                details.Add(NewsRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetNewsPage", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function




    Public Function GetEventItemList() As List(Of cEventItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetEventItemList
        'Purpose:	return list of event items       
        'Input:     Number of events to return, (0 = all rows) 
        'Returns:   Returns list event items objects 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySQLDataReader
        Dim details As New List(Of cEventItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "Select E.ID, E.Start_date, E.End_date, E.Type_ID, E.Status_ID, E.Student_Lead_ID, TM.Last_name+', '+TM.First_name AS Student_Lead_Name" &
                 ", E.Mentor_ID, M.Last_name+', '+M.First_name AS Mentor_Name, E.Approved_By_ID, E.Approved_date, E.Transportation_Provided" &
                 ", E.Location_text, E.Title_text, E.Body_text, E.Transportation_text " &
                 " FROM (Events AS E LEFT JOIN Team_Members AS TM ON E.Student_lead_id = TM.ID) LEFT JOIN Team_Members AS M ON E.Mentor_ID = M.ID" &
                 " WHERE (((E.Start_date)>DateAdd('d',-1,Now()))) " &
                 " ORDER BY E.Start_date"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim EventRow As New cEventItem
                With EventRow
                    .ID = TestNullLong(dr, 0)
                    .Start_Date = TestNullDate(dr, 1)
                    .End_Date = TestNullDate(dr, 2)
                    .Type_ID = TestNullLong(dr, 3)
                    .Status_ID = TestNullLong(dr, 4)
                    .Student_Lead_ID = TestNullLong(dr, 5)
                    .Student_Lead_Name = TestNullString(dr, 6)
                    .Mentor_ID = TestNullLong(dr, 7)
                    .Mentor_Name = TestNullString(dr, 8)
                    .ApprovedBy_ID = TestNullLong(dr, 9)
                    .Approved_Date = TestNullDate(dr, 10)
                    .Transport_Flag = TestNullLong(dr, 11)
                    .Location_text = TestNullString(dr, 12)
                    .Title_text = TestNullString(dr, 13)
                    .Body_text = TestNullString(dr, 14)
                    .Transportation_text = TestNullString(dr, 15)

                End With

                details.Add(EventRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetEventItemList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Sub UpdateNewsItem(NI As cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	UpdateNewsItem
        'Purpose:	Update News Database Table         
        'Input:     
        'Returns:   Returns nothing 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim lret As Long

        'If ID is zero go insert a row in table - else Update it  
        If NI.ID = 0 Then
            strSQL = "insert into FFWebsite.News (Post_Date,Priority_ID,Author_ID,Category_ID,Status_ID,LastUpdate_date,LastUpdate_ID,Approved_ID,Approved_date,Image1_name,Image2_name,Image3_name,Title_text,Body_text) " &
                     " values (" &
                     strQuote & NI.Post_Date.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     NI.Priority_ID.ToString() & strComma &
                     NI.Author_ID.ToString & strComma &
                     NI.Category_ID.ToString & strComma &
                     NI.Status_ID.ToString & strComma &
                     strQuote & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     NI.LastUpdate_ID.ToString & strComma &
                     NI.Approved_ID.ToString & strComma &
                     strQuote & NI.Approved_Date.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     strQuote & NI.Image1_Name & strQuote & strComma &
                     strQuote & NI.Image2_Name & strQuote & strComma &
                     strQuote & NI.Image3_Name & strQuote & strComma &
                     strQuote & NI.Title_text & strQuote & strComma &
                     strQuote & NI.Body_text & strQuote & ")"
        Else
            'Update existing record 
            strSQL = "Update FFWebsite.News " &
                     "   Set Post_Date = " & strQuote & NI.Post_Date.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     "     Priority_ID = " & NI.Priority_ID.ToString & strComma &
                     "       Author_ID = " & NI.Author_ID.ToString & strComma &
                     "     Category_ID = " & NI.Category_ID.ToString & strComma &
                     "       Status_ID = " & NI.Status_ID.ToString & strComma &
                     " LastUpdate_date = " & strQuote & DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     "   LastUpdate_ID = " & NI.LastUpdate_ID & strComma &
                     "     Approved_ID = " & NI.Approved_ID & strComma &
                     "   Approved_Date = " & strQuote & NI.Approved_Date.ToString("yyyy-MM-dd hh:mm:ss") & strQuote & strComma &
                     "     Image1_Name = " & strQuote & NI.Image1_Name & strQuote & strComma &
                     "     Image2_Name = " & strQuote & NI.Image2_Name & strQuote & strComma &
                     "     Image3_Name = " & strQuote & NI.Image3_Name & strQuote & strComma &
                     "      Title_Text = " & strQuote & NI.Title_text & strQuote & strComma &
                     "       Body_Text = " & strQuote & NI.Body_text & strQuote &
                     "  Where ID = " & NI.ID.ToString
        End If

        'Execute SQL Command 
        Try
            lret = m_cFFWebSiteDB.ExecNonQuery(strSQL)
        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("UpdateNewsItem", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

    End Sub
    Public Function GetCategoryList() As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetCategoryItemList
        'Purpose:	return list of category items       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg category list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySQLDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "SELECT ID, Description_text from FFWebsite.Category_Types"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TypeRow As New cTypeItem
                With TypeRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TypeRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetCategoryList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetStatusList() As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetStatusItemList
        'Purpose:	return list of status items       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg status list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "SELECT ID, Description_text from FFWebsite.Status_Types"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TypeRow As New cTypeItem
                With TypeRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TypeRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetStatusList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetAuthorList() As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetAuthorItemList
        'Purpose:	return list of possible authors items       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg author list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "Select ID, concat(Concat(Last_name,', '), First_name) as User_name from FFWebsite.Users "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TypeRow As New cTypeItem
                With TypeRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TypeRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetStatusList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetPriorityList() As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetPriorityItemList
        'Purpose:	return list of priority items       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "SELECT ID, Description_text from FFWebsite.Priority_Types"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TypeRow As New cTypeItem
                With TypeRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TypeRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetPriorityList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetEventList() As List(Of cEventItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetEventList
        'Purpose:	return list of event items       
        'Input:     Nothing 
        'Returns:   list of cEvent objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cEventItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "select ID, start_date, Title_text, Location_text " &
                 " from FFWebsite.Events " &
                 " where start_date >= DATE_SUB(Now(), INTERVAL 36 DAY)" &
                 " order by start_date desc "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim EventRow As New cEventItem
                With EventRow
                    .ID = TestNullLong(dr, 0)
                    .Start_Date = TestNullDate(dr, 1)
                    .Title_text = TestNullString(dr, 2)
                    .Location_text = TestNullString(dr, 3)
                End With

                details.Add(EventRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetEventList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetEventItembyID(ByVal lEventID As Long) As cEventItem
        '---------------------------------------------------------------------------------------
        'Function:	GetEventItembyID
        'Purpose:	return single event item for a given id        
        'Input:     ID of event item to read  
        'Returns:   Returns new event item object 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim EventRow As New cEventItem

        strSQL = "select E.ID, E.Start_date, E.Title_text, E.Location_text, E.Body_text" &
                 ", concat(Concat(M.Last_name,', '), M.First_name) as Mentor_name " &
                 ", concat(Concat(S.Last_name,', '), S.First_name) as Student_name " &
                 " From FFWebsite.Events E " &
                 " Left outer join FFWebsite.Users M on E.Mentor_ID = M.ID " &
                 " Left outer join FFWebsite.Users S on E.StudentLead_ID = S.ID " &
                 " where E.ID = " & lEventID.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                With EventRow
                    .ID = TestNullLong(dr, 0)
                    .Start_Date = TestNullDate(dr, 1)
                    .Title_text = TestNullString(dr, 2)
                    .Location_text = TestNullString(dr, 3)
                    .Body_text = TestNullString(dr, 4)
                    .Mentor_Name = TestNullString(dr, 5)
                    .Student_Lead_Name = TestNullString(dr, 6)
                End With
            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetEventItemByID", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return EventRow

    End Function
    Public Function GetAllEventList() As List(Of cEventItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetAllEventList
        'Purpose:	return list of event items       
        'Input:     Nothing 
        'Returns:   list of cEvent objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cEventItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "select ID, start_date, Title_text, Location_text, Body_text " &
                 " from FFWebsite.Events " &
                 " order by start_date desc " &
                 " LIMIT 15"


        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim EventRow As New cEventItem
                With EventRow
                    .ID = TestNullLong(dr, 0)
                    .Start_Date = TestNullDate(dr, 1)
                    .Title_text = TestNullString(dr, 2)
                    .Location_text = TestNullString(dr, 3)
                    .Body_text = TestNullString(dr, 4)
                End With

                details.Add(EventRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetAllEventList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetRelatedItems(ByVal itemCount As Integer, ByVal CategoryID As Integer, ByVal MainItemID As Long) As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetRelatedItems
        'Purpose:	return list of top n related news items          
        'Input:     The number of items to return, categoryid of related items to search for   
        'Returns:   list of cNewsItem objects 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB

        strSQL = "Select N.id, N.Title_text, N.post_date, C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author, N.Image1_Name, N.Image2_Name, mid(N.Body_Text,1,150) " &
                 " from FFWebsite.News N " &
                 " left outer join FFWebsite.Users U On N.Author_ID = U.ID " &
                 " left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " where N.Status_ID = 4 " &
                 " and N.Category_ID = " & CategoryID.ToString &
                 " and N.ID <> " + MainItemID.ToString &
                 " order by N.post_date desc " &
                 " LIMIT " + itemCount.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Post_Date = TestNullDate(dr, 2)
                    .Category_Name = TestNullString(dr, 3)
                    .Author_Name = TestNullString(dr, 4)
                    .Image1_Name = TestNullString(dr, 5)
                    .Image2_Name = TestNullString(dr, 6)
                    .Body_text = TestNullString(dr, 7)
                End With

                details.Add(NewsRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetNewsCarouselList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetRelatedByCategoryItems(ByVal itemCount As Integer, ByVal CategoryID As Integer) As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetRelatedItems
        'Purpose:	return list of top n related news items          
        'Input:     The number of items to return, categoryid of related items to search for   
        'Returns:   list of cNewsItem objects 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB

        strSQL = "Select N.id, N.Title_text, N.post_date, C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author, N.Image1_Name, N.Image2_Name, mid(N.Body_Text,1,150) " &
                 " from FFWebsite.News N " &
                 " left outer join FFWebsite.Users U On N.Author_ID = U.ID " &
                 " left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " where N.Status_ID = 4 " &
                 " and N.Category_ID = " & CategoryID.ToString &
                 " order by N.post_date desc " &
                 " LIMIT " + itemCount.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Post_Date = TestNullDate(dr, 2)
                    .Category_Name = TestNullString(dr, 3)
                    .Author_Name = TestNullString(dr, 4)
                    .Image1_Name = TestNullString(dr, 5)
                    .Image2_Name = TestNullString(dr, 6)
                    .Body_text = TestNullString(dr, 7)
                End With

                details.Add(NewsRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetNewsCarouselList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return details
    End Function



    '---------------------------------------------------------------------------------------------------
    '  Private Functions 
    '---------------------------------------------------------------------------------------------------
    Private Function BuildErrorMsg(ByVal strFunctionName As String, strThrownError As String) As String
        '---------------------------------------------------------------------------------------
        'Function:	BuildErrorMsg
        'Purpose:	Combine error details into a single message string containg server name, 
        '           Function throwing() Error And Error message text 
        'Input:     function anme and thrown error text   
        'Returns:   Returns string with combined error text 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strErrMsg As String
        strErrMsg = sServerName & ":" & strFunctionName & " failed! Return Code: " & strThrownError

        '  logger.Error(strErrMsg)

        Return strErrMsg
    End Function


End Class

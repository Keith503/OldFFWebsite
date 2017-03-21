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
        'Input:     catergory ID of news items to read 
        '           0-means all categories  
        'Returns:   list of cNewsItem objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim strSQLWhere As String

        If itemID = 0 Then
            strSQLWhere = ""
        Else
            strSQLWhere = "  and N.Category_ID = " + itemID.ToString
        End If

        strSQL = "Select N.id, N.Title_text, N.post_date, C.Description_text, concat(concat(U.Last_Name,', '),U.First_Name) as Author, N.Image1_Name, mid(N.Body_Text,1,150), N.Image2_Name " &
                 " from FFWebsite.News N " &
                 " left outer join FFWebsite.Users U on N.Author_ID = U.ID " &
                 " left outer join FFWebsite.Category_Types C on N.Category_ID = C.ID " &
                 " where N.Status_ID = 4 " & strSQLWhere &
                 " order by N.post_date desc " &
                 " LIMIT 25"

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
                     strQuote & RemoveQuotes(NI.Body_text) & strQuote & ")"
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

        strSQL = "SELECT ID, Description_text from FFWebsite.Category_Types order by Description_text"

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
    '  Scouting Functions  
    '---------------------------------------------------------------------------------------------------
    Public Function GetScoutingEventList() As List(Of cEventItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetScoutingEventList
        'Purpose:	return list of event items       
        'Input:     Nothing 
        'Returns:   list of cEvent objects  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cEventItem)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select ID, name,datestart " &
                 " from ffscouting.events " &
                 " order by name "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim EventRow As New cEventItem
                With EventRow
                    .ID = TestNullLong(dr, 0)
                    .Title_text = TestNullString(dr, 1)
                    .Start_Date = TestNullDate(dr, 2)

                End With

                details.Add(EventRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetScoutingEventList", ex.Message.ToString)
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

    Public Function GetScoutingMatchList(ByVal lEventID As Long) As List(Of cMatchItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetScoutingMatchList
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cMatchItem objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cMatchItem)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select ID, eventID, MatchNumber,Description,StartTime,TournamentLevel " &
                 " from ffscouting.eventschedule " &
                 " Where EventID = " & lEventID.ToString &
                 " order by MatchNumber "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim MatchRow As New cMatchItem
                With MatchRow
                    .ID = TestNullLong(dr, 0)
                    .EventID = TestNullLong(dr, 1)
                    .MatchNumber = TestNullLong(dr, 2)
                    .Description = TestNullString(dr, 3)
                    .StartTime = TestNullDate(dr, 4)
                    .TournamentLevel = TestNullString(dr, 5)
                End With

                details.Add(MatchRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetScoutingMatchList", ex.Message.ToString)
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

    Public Function GetTeamsinMatch(ByVal lScheduleID As Long) As List(Of cMatchTeams)
        '---------------------------------------------------------------------------------------
        'Function:	GetTeamsinMatch
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cMatchTeams objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cMatchTeams)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select st.scheduleID, st.TeamNumber, t.NameShort, st.Station " &
                 " from ffscouting.scheduleteams st  " &
                 " left outer join ffscouting.teams t on st.TeamNumber = t.TeamNumber " &
                 " Where st.ScheduleID = " & lScheduleID.ToString &
                 " order by st.Station "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim MatchRow As New cMatchTeams
                With MatchRow
                    .ScheduleID = TestNullLong(dr, 0)
                    .TeamNumber = TestNullLong(dr, 1)
                    .TeamName = TestNullString(dr, 2)
                    .Station = TestNullString(dr, 3)
                End With

                details.Add(MatchRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetTeamsinMatch", ex.Message.ToString)
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
    Public Function GetScoutingMatchesforTeam(ByVal lEventID As Long, ByVal lTeamNumber As Long) As List(Of cMatchItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetScoutingMatchedforTeam
        'Purpose:	return list of matches that a particular team is in for a given event        
        'Input:     Nothing 
        'Returns:   list of cMatchItem objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cMatchItem)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select es.MatchNumber,st.Station" &
                 " from ffscouting.eventschedule es" &
                 "  left outer join ffscouting.scheduleteams st on es.id = st.scheduleID " &
                 " Where es.EventID = " & lEventID.ToString &
                 " and st.teamnumber = " & lTeamNumber.ToString &
                 " order by es.MatchNumber,st.station "

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim MatchRow As New cMatchItem
                With MatchRow
                    .MatchNumber = TestNullLong(dr, 0)
                    .Station = TestNullString(dr, 1)
                End With

                details.Add(MatchRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetScoutingScoresforTeam", ex.Message.ToString)
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

    Public Function GetScoutingScoresforTeam(ByVal lEventID As Long, ByVal lTeamNumber As Long, ByVal Matchobj As List(Of cMatchItem)) As List(Of cAllianceScore)
        '---------------------------------------------------------------------------------------
        'Function:	GetScoutingScoresforTeam
        'Purpose:	return list of scores for a given set of matches for a particular event         
        'Input:     Nothing 
        'Returns:   list of cAllianceScore objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cAllianceScore)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0
        Dim strSQLWhere As String = ""
        Dim strWork As String
        Dim boolFirstTime As Boolean = True
        Dim ScoutList As List(Of cTabletData)
        Dim AllianceList As List(Of cMatchTeams)
        Dim strType As String
        Dim strTeams As String

        'first build the where clause for the select 
        'For Each cMatchItem In Matchobj
        ' If Not boolFirstTime Then
        ' strSQLWhere = strSQLWhere + " or "
        ' Else
        ' boolFirstTime = False
        ' End If
        '
        '        If Mid(cMatchItem.Station, 1, 1) = "R" Then
        '        strWork = "Red"
        '        Else
        '        strWork = "Blue"
        '        End If
        '        strSQLWhere = strSQLWhere + "(es.matchnumber = " + cMatchItem.MatchNumber.ToString + " and sc.type = " + strQuote + strWork + strQuote + ") "
        '        Next

        strSQL = "select st.eventid, st.matchnumber, st.TeamNumber, st.Station   " &
                 ", sc.autoFuelLow, sc.autoFuelHigh, sc.rotor1Auto + sc.rotor2Auto as AutonRotor  " &
                 ", sc.autoPoints, sc.TeleopPoints, sc.TeleopFuelPoints,sc.teleoptakeoffpoints, sc.foulPoints  " &
                 " From ffscouting.scheduleteams st " &
                 " Left outer join ffscouting.alliancescores sc on " &
                 " (st.eventid = sc.eventid And st.MatchNumber = sc.matchNumber And mid(sc.Type,1,3) = mid(st.Station,1,3))  " &
                 " where st.eventID = " & lEventID.ToString &
                 " And st.teamnumber = " & lTeamNumber.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()

                Dim ScoreRow As New cAllianceScore
                With ScoreRow
                    .MatchNumber = TestNullLong(dr, 1)
                    strType = TestNullString(dr, 2)
                    .AutoFuelLow = TestNullLong(dr, 4)
                    .AutoFuelHigh = TestNullLong(dr, 5)
                    .AutoRotor = TestNullLong(dr, 6)
                    .AutoPoints = TestNullLong(dr, 7)
                    .TeleopPoints = TestNullLong(dr, 8)
                    .TeleopFuelPoints = TestNullLong(dr, 9)
                    .TeleopTakeOffPoints = TestNullLong(dr, 10)
                    .FoulPoints = TestNullLong(dr, 11)

                    AllianceList = GetAllianceTeams(lEventID, .MatchNumber, strType)
                    strTeams = ""
                    For Each cMatchTeam In AllianceList
                        strTeams = strTeams + CStr(cMatchTeam.TeamNumber) + ","
                    Next
                    Mid(strTeams, Len(strTeams), 1) = " "
                    .AllianceTeams = strTeams

                End With


                details.Add(ScoreRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetScoutingMatchesforTeam", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseDataReader()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        'go get any manual scouting data observed and enterd on the tablets 
        ScoutList = GetScoutingObservationsforTeam(lEventID, lTeamNumber, Matchobj)

        'loop through each observation 
        For Each cTabletData In ScoutList
            'Loop through each alliace score record to find on to add data 
            For Each cAlliancescore In details
                'match on match number 
                If cAlliancescore.MatchNumber = cTabletData.MatchNumber Then
                    With cAlliancescore
                        .ScoutBreachlineA = cTabletData.BreachLineA
                        strWork = cTabletData.GearLocation
                        Select Case strWork
                            Case "C"
                                .ScoutGearLocationA = "Center"
                            Case "L"
                                .ScoutGearLocationA = "Left"
                            Case "R"
                                .ScoutGearLocationA = "Right"
                            Case Else
                                .ScoutGearLocationA = ""
                        End Select
                        .ScoutScore50A = cTabletData.ScoreatLeast50A
                        .ScoutScoreGearA = cTabletData.ScoreGearA
                        .ScoutScoreHighA = cTabletData.ScoreHighFuelA
                        .ScoutScoreLowA = cTabletData.ScoreLowFuelA
                        .ScoutGearT = cTabletData.ScoreGearT
                        .ScoutScoreHighT = cTabletData.ScoreHighFuelT
                        .ScoutClimb = cTabletData.Climb
                        .ScoutDropGears = cTabletData.DropGears
                        .ScoutTechDiff = cTabletData.TechDiff
                        .ScoutTotalHighFuel = cTabletData.TotalHighFuelScore
                    End With

                End If
            Next
        Next


        m_cFFWebSiteDB = Nothing

        Return details
    End Function

    Public Function GetScoutingObservationsforTeam(ByVal lEventID As Long, ByVal lTeamNumber As Long, ByVal Matchobj As List(Of cMatchItem)) As List(Of cTabletData)
        '---------------------------------------------------------------------------------------
        'Function:	GetScoutingobservationsforTeam
        'Purpose:	return list of scores for a given set of matches for a particular event         
        'Input:     Nothing 
        'Returns:   list of cTabletData objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTabletData)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0
        Dim strSQLWhere As String = ""
        Dim strWork As String
        Dim boolFirstTime As Boolean = True

        'first build the where clause for the select 
        For Each cMatchItem In Matchobj
            If Not boolFirstTime Then
                strSQLWhere = strSQLWhere + " Or "
            Else
                boolFirstTime = False
            End If

            If Mid(cMatchItem.Station, 1, 1) = "R" Then
                strWork = "Red"
            Else
                strWork = "Blue"
            End If
            strSQLWhere = strSQLWhere + "(matchnumber = " + cMatchItem.MatchNumber.ToString + " And Alliance = " + strQuote + strWork + strQuote + ") "
        Next

        strSQL = "Select id,eventid,teamnumber,matchnumber,BreachLineA,ScoreGearA,GearLocation,ScoreHighFuelA " &
                 ",ScoreatLeast50A,ScoreLowFuelA,ScoreGearT,ScoreHighFuelT,ScoreLowFuelT,Climb,ClimbLocation" &
                 ",DropGears,TechDiff,TotalHighFuelScore " &
                 " from ffscouting.scoutdata  " &
                 " where eventID = " & lEventID.ToString &
                 " And teamnumber = " & lTeamNumber.ToString &
                 " And (" & strSQLWhere & ") order by matchNumber"

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()

                Dim TabletRow As New cTabletData
                With TabletRow
                    .ID = TestNullLong(dr, 0)
                    .EventID = TestNullLong(dr, 1)
                    .TeamNumber = TestNullLong(dr, 2)
                    .MatchNumber = TestNullLong(dr, 3)
                    .BreachLineA = TestNullBoolean(dr, 4)
                    .ScoreGearA = TestNullBoolean(dr, 5)
                    .GearLocation = TestNullString(dr, 6)
                    .ScoreHighFuelA = TestNullLong(dr, 7)
                    .ScoreatLeast50A = TestNullBoolean(dr, 8)
                    .ScoreLowFuelA = TestNullBoolean(dr, 9)
                    .ScoreGearT = TestNullLong(dr, 10)
                    .ScoreHighFuelT = TestNullLong(dr, 11)
                    .ScoreLowFuelT = TestNullBoolean(dr, 12)
                    .Climb = TestNullBoolean(dr, 13)
                    .ClimbLocation = TestNullString(dr, 14)
                    .DropGears = TestNullLong(dr, 15)
                    .TechDiff = TestNullString(dr, 16)
                    .TotalHighFuelScore = TestNullLong(dr, 17)
                End With

                details.Add(TabletRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetScoutingObservationsforTeam", ex.Message.ToString)
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
    Public Function GetAllianceTeams(ByVal lEventID As Long, lMatchNumber As Long, sAlliance As String) As List(Of cMatchTeams)
        '---------------------------------------------------------------------------------------
        'Function:	GetAllianceTeams
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cMatchTeams objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cMatchTeams)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0
        Dim strColor As String

        If Mid(sAlliance, 1, 1) = "R" Then
            strColor = "Red%"
        Else
            strColor = "Blue%"
        End If

        strSQL = "Select es.eventid,es.matchnumber,st.teamnumber, st.station " &
                 " From ffscouting.eventschedule es " &
                 " Left outer join ffscouting.scheduleteams st On es.ID = st.scheduleid  " &
                 "  where es.eventid = " & lEventID.ToString &
                 " And es.matchnumber = " & lMatchNumber.ToString &
                 " And st.station Like " & strQuote & strColor & strQuote

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim MatchRow As New cMatchTeams
                With MatchRow
                    .TeamNumber = TestNullLong(dr, 2)
                End With

                details.Add(MatchRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetAllianceTeams", ex.Message.ToString)
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

    Public Function GetGearRanking(ByVal lEventID As Long) As List(Of cGearStats)
        '---------------------------------------------------------------------------------------
        'Function:	GetGearRanking
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cGearStats objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cGearStats)
        Dim m_cFFScoutingDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "Select concat(concat(sd.teamNumber, '-'), t.NameShort) as Team " &
                 ", sum(sd.scoregearA),sum(sd.scoregeart), sum(sd.ScoreGearA + sd.ScoreGearT) " &
                 " from ffscouting.scoutdata sd " &
                 " left outer join ffscouting.teams t on sd.TeamNumber = t.TeamNumber " &
                 " where sd.eventid = " & lEventID.ToString &
                 " Group by concat(concat(sd.teamNumber, '-'), t.NameShort) " &
                 " order by sum(sd.ScoreGearA + sd.ScoreGearT) desc "

        'Execute SQL Command 
        Try
            dr = m_cFFScoutingDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim GearRow As New cGearStats
                With GearRow
                    .Team = TestNullString(dr, 0)
                    .AutonGears = TestNullLong(dr, 1)
                    .TeleOpGears = TestNullLong(dr, 2)
                    .TotalGears = TestNullLong(dr, 3)
                End With

                details.Add(GearRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetGearRanking", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFScoutingDB.cmd.Dispose()
            m_cFFScoutingDB.CloseDataReader()
            m_cFFScoutingDB.CloseConnection()
        End Try

        m_cFFScoutingDB = Nothing

        Return details
    End Function

    Public Function GetClimbRanking(ByVal lEventID As Long) As List(Of cClimbStats)
        '---------------------------------------------------------------------------------------
        'Function:	GetClimbRanking
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cClimbStats objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cClimbStats)
        Dim m_cFFScoutingDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select concat(concat(sd.teamNumber, '-'), t.NameShort) as Team, sum(sd.climb) " &
                 " from ffscouting.scoutdata sd " &
                 " left outer join ffscouting.teams t on sd.TeamNumber = t.TeamNumber " &
                 " where sd.eventid = " & lEventID.ToString &
                 " Group by concat(concat(sd.teamNumber, '-'), t.NameShort) " &
                 " order by sum(sd.climb) desc "

        'Execute SQL Command 
        Try
            dr = m_cFFScoutingDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim ClimbRow As New cClimbStats
                With ClimbRow
                    .Team = TestNullString(dr, 0)
                    .TotalClimb = TestNullLong(dr, 1)
                End With

                details.Add(ClimbRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetClimbRanking", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFScoutingDB.cmd.Dispose()
            m_cFFScoutingDB.CloseDataReader()
            m_cFFScoutingDB.CloseConnection()
        End Try

        m_cFFScoutingDB = Nothing

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

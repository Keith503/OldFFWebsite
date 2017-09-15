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
                  " LIMIT 15"

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
                 " order by N.post_date desc "
        '   " LIMIT 25"

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
                 " where start_date >= Now() " &
                 " order by start_date Asc " &
                 " LIMIT 5 "

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
    Public Function GetTeamsAtEvent(ByVal lEventID As Long) As List(Of cTypeItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetEventList
        'Purpose:	return list of event items       
        'Input:     Nothing 
        'Returns:   list of cEvent objects containg priority list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTypeItem)
        Dim m_cFFWebSiteDB As New cFFScoutingDB
        Dim i As Integer = 0

        strSQL = "select et.TeamNumber, t.NameShort " &
                  " From ffscouting.eventteams et " &
                  " Left outer join ffscouting.teams t on et.TeamNumber = t.TeamNumber " &
                  " where et.EventID = " & lEventID.ToString

        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TeamRow As New cTypeItem
                With TeamRow
                    .ID = TestNullLong(dr, 0)
                    .Description_text = TestNullString(dr, 1)
                End With

                details.Add(TeamRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetTeamsAtEvent", ex.Message.ToString)
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

        strSQL = "Select ID, name,datestart " &
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

        strSQL = "Select ID, eventID, MatchNumber,Description,StartTime,TournamentLevel " &
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

        strSQL = "Select st.scheduleID, st.TeamNumber, t.NameShort, st.Station " &
                 " from ffscouting.scheduleteams st  " &
                 " left outer join ffscouting.teams t On st.TeamNumber = t.TeamNumber " &
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

        strSQL = "Select es.MatchNumber,st.Station" &
                 " from ffscouting.eventschedule es" &
                 "  left outer join ffscouting.scheduleteams st On es.id = st.scheduleID " &
                 " Where es.EventID = " & lEventID.ToString &
                 " And st.teamnumber = " & lTeamNumber.ToString &
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
        ' strSQLWhere = strSQLWhere + " Or "
        ' Else
        ' boolFirstTime = False
        ' End If
        '
        '        If Mid(cMatchItem.Station, 1, 1) = "R" Then
        '        strWork = "Red"
        '        Else
        '        strWork = "Blue"
        '        End If
        '        strSQLWhere = strSQLWhere + "(es.matchnumber = " + cMatchItem.MatchNumber.ToString + " And sc.type = " + strQuote + strWork + strQuote + ") "
        '        Next

        strSQL = "Select st.eventid, st.matchnumber, st.TeamNumber, st.Station   " &
                 ", sc.autoFuelLow, sc.autoFuelHigh, sc.rotor1Auto, sc.rotor2Auto  " &
                 ", sc.autoPoints, sc.TeleopPoints, sc.TeleopFuelPoints,sc.teleoptakeoffpoints, sc.foulPoints  " &
                 ", sc.autorotorpoints, sc.autoMobilityPoints,sc.AdjustPoints,sc.TotalPoints " &
                 ", sc.rotor1Engaged, sc.rotor2Engaged, sc.rotor3Engaged,sc.rotor4Engaged " &
                 " From ffscouting.scheduleteams st " &
                 " Left outer join ffscouting.alliancescores sc On " &
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
                    strType = TestNullString(dr, 3)
                    .AutoFuelLow = TestNullLong(dr, 4)
                    .AutoFuelHigh = TestNullLong(dr, 5)
                    .AutoRotor = 0
                    If TestNullBoolean(dr, 6) Then .AutoRotor = .AutoRotor + 1
                    If TestNullBoolean(dr, 7) Then .AutoRotor = .AutoRotor + 1
                    .AutoPoints = TestNullLong(dr, 8)
                    .TeleopPoints = TestNullLong(dr, 9)
                    .TeleopFuelPoints = TestNullLong(dr, 10)
                    .TeleopTakeOffPoints = TestNullLong(dr, 11)
                    .FoulPoints = TestNullLong(dr, 12)
                    .AutoRotorPoints = TestNullLong(dr, 13)
                    .AutoMobilityPoints = TestNullLong(dr, 14)
                    .Adjustpoints = TestNullLong(dr, 15)
                    .TotalPoints = TestNullLong(dr, 16)
                    .TotalRotorPoints = 0
                    If TestNullBoolean(dr, 6) Then
                        .TotalRotorPoints = .TotalRotorPoints + 0  'already in total auton points 
                    Else
                        If TestNullBoolean(dr, 17) Then
                            .TotalRotorPoints = .TotalRotorPoints + 40
                        End If
                    End If
                    If TestNullBoolean(dr, 7) Then
                        .TotalRotorPoints = .TotalRotorPoints + 0   'already in total auton points 
                    Else
                        If TestNullBoolean(dr, 18) Then
                            .TotalRotorPoints = .TotalRotorPoints + 40
                        End If
                    End If
                    If TestNullBoolean(dr, 19) Then .TotalRotorPoints = .TotalRotorPoints + 40
                    If TestNullBoolean(dr, 20) Then .TotalRotorPoints = .TotalRotorPoints + 40

                    If (TestNullBoolean(dr, 17) And TestNullBoolean(dr, 18) And TestNullBoolean(dr, 19) And TestNullBoolean(dr, 20)) Then
                        .TotalRotorPoints = .TotalRotorPoints + 100
                    End If
                    AllianceList = GetAllianceTeams(lEventID, .MatchNumber, strType)
                    strTeams = ""
                    For Each cMatchTeam In AllianceList
                        If lTeamNumber = cMatchTeam.TeamNumber Then
                            strTeams = strTeams + "<b>" + CStr(cMatchTeam.TeamNumber) + "</b>" + ","
                        Else
                            strTeams = strTeams + CStr(cMatchTeam.TeamNumber) + ","
                        End If
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
                        .ScoutAutonHighFuelScore = cTabletData.TotalAutonHighFuelScore
                        .ScoutScoreGearA = cTabletData.ScoreGearA
                        .ScoutScoreHighA = cTabletData.ScoreHighFuelA
                        .ScoutScoreLowA = cTabletData.ScoreLowFuelA
                        .ScoutGearT = cTabletData.ScoreGearT
                        .ScoutScoreHighT = cTabletData.ScoreHighFuelT
                        .ScoutClimb = cTabletData.Climb
                        .ScoutDropGears = cTabletData.DropGears
                        .ScoutTechDiff = cTabletData.TechDiff
                        .ScoutTotalHighFuel = cTabletData.TotalHighFuelScore
                        .ScoutClimbLocation = cTabletData.ClimbLocation
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
                 ",TotalAutonHighFuelScore,ScoreLowFuelA,ScoreGearT,ScoreHighFuelT,ScoreLowFuelT,Climb,ClimbLocation" &
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
                    .TotalAutonHighFuelScore = TestNullLong(dr, 8)
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

    Public Function DumpScoutingData(ByVal lEventID As Long) As List(Of cTabletData)
        '---------------------------------------------------------------------------------------
        'Function:	DumpScoutingData
        'Purpose:	return all tablet data for manual input into excel spreadsheet - just in case          
        'Input:     Nothing 
        'Returns:   list of cTabletData objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTabletData)
        Dim m_cFFWebSiteDB As New cFFScoutingDB

        strSQL = "Select id,eventid,teamnumber,matchnumber,BreachLineA,ScoreGearA,GearLocation,ScoreHighFuelA " &
                 ",TotalAutonHighFuelScore,ScoreLowFuelA,ScoreGearT,ScoreHighFuelT,ScoreLowFuelT,Climb,ClimbLocation" &
                 ",DropGears,TechDiff,TotalHighFuelScore,ScoutName,Alliance " &
                 " from ffscouting.scoutdata  " &
                 " where eventID = " & lEventID.ToString

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
                    .TotalAutonHighFuelScore = TestNullLong(dr, 8)
                    .ScoreLowFuelA = TestNullBoolean(dr, 9)
                    .ScoreGearT = TestNullLong(dr, 10)
                    .ScoreHighFuelT = TestNullLong(dr, 11)
                    .ScoreLowFuelT = TestNullBoolean(dr, 12)
                    .Climb = TestNullBoolean(dr, 13)
                    .ClimbLocation = TestNullString(dr, 14)
                    .DropGears = TestNullLong(dr, 15)
                    .TechDiff = TestNullString(dr, 16)
                    .TotalHighFuelScore = TestNullLong(dr, 17)
                    .ScoutName = TestNullString(dr, 18)
                    .Alliance = TestNullString(dr, 19)



                End With

                details.Add(TabletRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("DumpScoutingData", ex.Message.ToString)
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
    Public Function GetShootingRanking(ByVal lEventID As Long) As List(Of cClimbStats)
        '---------------------------------------------------------------------------------------
        'Function:	GetShootingRanking
        'Purpose:	return list of Match items       
        'Input:     Nothing 
        'Returns:   list of cClimbStats objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cClimbStats)
        Dim m_cFFScoutingDB As New cFFScoutingDB
        Dim i As Integer = 0
        'TODO   need to finish this - query has not been updated !!!!!

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
    Public Function UpdateNbotInterest(ByVal obj As cNbotInterest) As Long
        '******************************************************************************
        '*  Name:       UpdateNbotInterest 
        '*  Purpose:    Update database with nBot Interest data 
        '*  Input:      nBot Interest object 
        '*  returns:    ?????? 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFFWebSiteDB
        Dim lRet As Long = 0

        'make sure there are no quotes in the prior experience field 
        obj.PriorExperience = RemoveQuotes(obj.PriorExperience)

        strSQL = "Insert into FFWebsite.Nbot_Interest(StudentFirstName,StudentLastName,StudentPhone,StudenteMail," &
                 "SchoolID,Grade,Gender,Parent1Name,Parent1eMail,Parent1Phone,Parent2Name,Parent2eMail," &
                 "FirstProgram,Question1,Question2,Question3,PriorExperience) " &
                 "values(" &
                  strQuote & obj.StudentFirstName & strQuote & strComma &
                  strQuote & obj.StudentLastName & strQuote & strComma &
                  strQuote & obj.StudentPhone & strQuote & strComma &
                  strQuote & obj.StudenteMail & strQuote & strComma &
                  obj.SchoolID.ToString & strComma &
                  obj.Grade.ToString & strComma &
                  strQuote & obj.Gender & strQuote & strComma &
                  strQuote & obj.Parent1Name & strQuote & strComma &
                  strQuote & obj.Parent1eMail & strQuote & strComma &
                  strQuote & obj.Parent1Phone & strQuote & strComma &
                  strQuote & obj.Parent2Name & strQuote & strComma &
                  strQuote & obj.Parent2eMail & strQuote & strComma &
                  obj.FirstProgram.ToString & strComma &
                  strQuote & obj.Question1 & strQuote & strComma &
                  strQuote & obj.Question2 & strQuote & strComma &
                  strQuote & obj.Question3 & strQuote & strComma &
                  strQuote & obj.PriorExperience & strQuote & ")"

        Try
            DBServer.ExecNonQuery(strSQL)
        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error inserting UpdateNbotInterest! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        DBServer = Nothing
        Return lRet
    End Function
    Public Function GetNbotList() As List(Of cNbotInterest)
        '---------------------------------------------------------------------------------------
        'Function:	GetNbotList
        'Purpose:	return list of cNbotInterest items        
        'Input:     Nothing 
        'Returns:   list of cNbotInterest objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cNbotInterest)
        Dim m_cFFWebsiteDB As New cFFWebSiteDB

        strSQL = "select ID,StudentFirstName,StudentLastName,StudentPhone,StudenteMail,SchoolID, " &
                 "Grade,Gender,Parent1Name,Parent1eMail,Parent1Phone,Parent2Name,Parent2eMail, " &
                 "FirstProgram,Question1,Question2,Question3, PriorExperience " &
                 "from FFWebsite.Nbot_Interest " &
                 " order by id "

        'Execute SQL Command 
        Try
            dr = m_cFFWebsiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim NBotRow As New cNbotInterest
                With NBotRow
                    .ID = TestNullLong(dr, 0)
                    .StudentFirstName = TestNullString(dr, 1)
                    .StudentLastName = TestNullString(dr, 2)
                    .StudentPhone = TestNullString(dr, 3)
                    .StudenteMail = TestNullString(dr, 4)
                    .SchoolID = TestNullLong(dr, 5)
                    .Grade = TestNullLong(dr, 6)
                    .Gender = TestNullString(dr, 7)
                    .Parent1Name = TestNullString(dr, 8)
                    .Parent1eMail = TestNullString(dr, 9)
                    .Parent1Phone = TestNullString(dr, 10)
                    .Parent2Name = TestNullString(dr, 11)
                    .Parent2eMail = TestNullString(dr,12)
                    .FirstProgram = TestNullLong(dr, 13)
                    .Question1 = TestNullString(dr, 14)
                    .Question2 = TestNullString(dr, 15)
                    .Question3 = TestNullString(dr, 16)
                    .PriorExperience = TestNullString(dr, 17)
                End With

                details.Add(NBotRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetNbotList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebsiteDB.cmd.Dispose()
            m_cFFWebsiteDB.CloseDataReader()
            m_cFFWebsiteDB.CloseConnection()
        End Try

        m_cFFWebsiteDB = Nothing

        Return details
    End Function
    Public Function UpdateFTCKickoffRegister(ByVal obj As cFTCKickoffRegister) As Long
        '******************************************************************************
        '*  Name:       UpdateFTCKickoffRegister 
        '*  Purpose:    Update database with FTC Kickoff Registration data 
        '*  Input:      FTC Kickoff Register object 
        '*  returns:    ?????? 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFFWebSiteDB
        Dim lRet As Long = 0
        Dim bolTeamOnFile As Boolean

        'make sure there are no quotes in the school name and team name fields 
        obj.School = RemoveQuotes(obj.School)
        obj.TeamName = RemoveQuotes(obj.TeamName)

        'Test to see if team is already on file 
        bolTeamOnFile = CheckFTCRegTeamID(obj.ID)

        If bolTeamOnFile = True Then
            'Update 
            strSQL = "Update FFWebsite.FTC_Kickoff_Register" &
                     " Set TeamName = " & strQuote & obj.TeamName & strQuote & strComma &
                     " Experience = " & strQuote & obj.Experience & strQuote & strComma &
                     " School = " & strQuote & obj.School & strQuote & strComma &
                     " TeamContactName = " & strQuote & obj.TeamContactName & strQuote & strComma &
                     " TeamContactEmail = " & strQuote & obj.TeamContactEmail & strQuote & strComma &
                     " TeamContactPhone = " & strQuote & obj.TeamContactPhone & strQuote & strComma &
                     " MentorCount = " & obj.MentorCount.ToString & strComma &
                     " StudentCount = " & obj.StudentCount.ToString &
                     " Where ID = " & obj.ID.ToString
        Else
            'Insert 
            strSQL = "Insert into FFWebsite.FTC_Kickoff_Register(ID,  TeamName, Experience,School,TeamContactName, TeamContactEmail," &
                 "TeamContactPhone,MentorCount,StudentCount) values(" &
                  obj.ID.ToString & strComma &
                  strQuote & obj.TeamName & strQuote & strComma &
                  strQuote & obj.Experience & strQuote & strComma &
                  strQuote & obj.School & strQuote & strComma &
                  strQuote & obj.TeamContactName & strQuote & strComma &
                  strQuote & obj.TeamContactEmail & strQuote & strComma &
                  strQuote & obj.TeamContactPhone & strQuote & strComma &
                  obj.MentorCount.ToString & strComma &
                  obj.StudentCount.ToString & ")"
        End If

        Try
            DBServer.ExecNonQuery(strSQL)
        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error updateing FTC Registration! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        DBServer = Nothing
        Return lRet
    End Function

    Public Function CheckFTCRegTeamID(ByVal lTeamID As Long) As Boolean
        '---------------------------------------------------------------------------------------
        'Function:	CheckFTCRegTeamID
        'Purpose:	Determine if a team is already in the database to avoid adding duplicates        
        'Input:     Team Number to read  
        'Returns:   Returns boolean - true found, false - not found  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim obj As Object
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim retBool As Boolean = False
        Dim lTeamNo As Long

        strSQL = "SELECT ID From FFWebsite.FTC_Kickoff_Register R" &
                 " Where R.ID = " & lTeamID.ToString

        'Execute SQL Command 
        Try
            obj = m_cFFWebSiteDB.ExecSVQuery(strSQL)
            If Not obj Is Nothing Then
                If TypeOf obj Is Integer Then
                    lTeamNo = CLng(obj)
                    retBool = True
                End If
            End If
        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("CheckFTCRegTeamID", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return retBool

    End Function

    Public Function GetFTCKickoffList() As List(Of cFTCKickoffRegister)
        '---------------------------------------------------------------------------------------
        'Function:	GetFTCKickoffList
        'Purpose:	return list of cFTCKickoffRefister items        
        'Input:     Nothing 
        'Returns:   list of cFTCKickoffRegister objects   
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cFTCKickoffRegister)
        Dim m_cFFWebsiteDB As New cFFWebSiteDB

        strSQL = "select ID,TeamName,Experience,School,TeamContactName,TeamContactEmail," &
                 "TeamContactPhone,MentorCount,StudentCount " &
                 "from FFWebsite.FTC_Kickoff_Register " &
                 " order by id "

        'Execute SQL Command 
        Try
            dr = m_cFFWebsiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim FTCKickoffRow As New cFTCKickoffRegister
                With FTCKickoffRow
                    .ID = TestNullLong(dr, 0)
                    .TeamName = TestNullString(dr, 1)
                    .Experience = TestNullString(dr, 2)
                    .School = TestNullString(dr, 3)
                    .TeamContactName = TestNullString(dr, 4)
                    .TeamContactEmail = TestNullString(dr, 5)
                    .TeamContactPhone = TestNullString(dr, 6)
                    .MentorCount = TestNullLong(dr, 7)
                    .StudentCount = TestNullLong(dr, 8)
                End With

                details.Add(FTCKickoffRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetFTCKickoffList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebsiteDB.cmd.Dispose()
            m_cFFWebsiteDB.CloseDataReader()
            m_cFFWebsiteDB.CloseConnection()
        End Try

        m_cFFWebsiteDB = Nothing

        Return details
    End Function


    Public Function UpdateFLLWorkshopRegister(ByVal obj As cFTCKickoffRegister) As Long
        '******************************************************************************
        '*  Name:       UpdateFLLWorkshopRegister 
        '*  Purpose:    Update database with FLL Workshop Registration data 
        '*  Input:      FTC Kickoff Register object (FLL & FTC use the same object)  
        '*  returns:    ?????? 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFFWebSiteDB
        Dim lRet As Long = 0
        Dim bolTeamOnFile As Boolean

        'make sure there are no quotes in the school name and team name fields 
        obj.School = RemoveQuotes(obj.School)
        obj.TeamName = RemoveQuotes(obj.TeamName)

        'Test to see if team is already on file 
        bolTeamOnFile = CheckFLLRegTeamID(obj.ID)

        If bolTeamOnFile = True Then
            'Update 
            strSQL = "Update FFWebsite.FLL_Workshop_Register" &
                     " Set TeamName = " & strQuote & obj.TeamName & strQuote & strComma &
                     " Experience = " & strQuote & obj.Experience & strQuote & strComma &
                     " School = " & strQuote & obj.School & strQuote & strComma &
                     " TeamContactName = " & strQuote & obj.TeamContactName & strQuote & strComma &
                     " TeamContactEmail = " & strQuote & obj.TeamContactEmail & strQuote & strComma &
                     " TeamContactPhone = " & strQuote & obj.TeamContactPhone & strQuote & strComma &
                     " MentorCount = " & obj.MentorCount.ToString & strComma &
                     " StudentCount = " & obj.StudentCount.ToString &
                     " Where ID = " & obj.ID.ToString
        Else
            'Insert 
            strSQL = "Insert into FFWebsite.FLL_Workshop_Register(ID,  TeamName, Experience,School,TeamContactName, TeamContactEmail," &
                 "TeamContactPhone,MentorCount,StudentCount) values(" &
                  obj.ID.ToString & strComma &
                  strQuote & obj.TeamName & strQuote & strComma &
                  strQuote & obj.Experience & strQuote & strComma &
                  strQuote & obj.School & strQuote & strComma &
                  strQuote & obj.TeamContactName & strQuote & strComma &
                  strQuote & obj.TeamContactEmail & strQuote & strComma &
                  strQuote & obj.TeamContactPhone & strQuote & strComma &
                  obj.MentorCount.ToString & strComma &
                  obj.StudentCount.ToString & ")"
        End If

        Try
            DBServer.ExecNonQuery(strSQL)
        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error updateing FLL Workshop Registration! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        DBServer = Nothing
        Return lRet
    End Function

    Public Function CheckFLLRegTeamID(ByVal lTeamID As Long) As Boolean
        '---------------------------------------------------------------------------------------
        'Function:	CheckFLLRegTeamID
        'Purpose:	Determine if a team is already in the database to avoid adding duplicates        
        'Input:     Team Number to read  
        'Returns:   Returns boolean - true found, false - not found  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim obj As Object
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim retBool As Boolean = False
        Dim lTeamNo As Long

        strSQL = "SELECT ID From FFWebsite.FLL_Workshop_Register R" &
                 " Where R.ID = " & lTeamID.ToString

        'Execute SQL Command 
        Try
            obj = m_cFFWebSiteDB.ExecSVQuery(strSQL)
            If Not obj Is Nothing Then
                If TypeOf obj Is Integer Then
                    lTeamNo = CLng(obj)
                    retBool = True
                End If
            End If
        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("CheckFLLRegTeamID", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return retBool

    End Function

    Public Function GetFLLWorkshopList() As List(Of cFTCKickoffRegister)
        '---------------------------------------------------------------------------------------
        'Function:	GetFLLWorkshopList
        'Purpose:	return list of cFLLKickoffRegister items        
        'Input:     Nothing 
        'Returns:   list of cFTCKickoffRegister objects (FTC & FLL use same object)    
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cFTCKickoffRegister)
        Dim m_cFFWebsiteDB As New cFFWebSiteDB

        strSQL = "select ID,TeamName,Experience,School,TeamContactName,TeamContactEmail," &
                 "TeamContactPhone,MentorCount,StudentCount " &
                 "from FFWebsite.FLL_Workshop_Register " &
                 " order by id "

        'Execute SQL Command 
        Try
            dr = m_cFFWebsiteDB.ExecDRQuery(strSQL)
            While dr.Read()
                Dim FTCKickoffRow As New cFTCKickoffRegister
                With FTCKickoffRow
                    .ID = TestNullLong(dr, 0)
                    .TeamName = TestNullString(dr, 1)
                    .Experience = TestNullString(dr, 2)
                    .School = TestNullString(dr, 3)
                    .TeamContactName = TestNullString(dr, 4)
                    .TeamContactEmail = TestNullString(dr, 5)
                    .TeamContactPhone = TestNullString(dr, 6)
                    .MentorCount = TestNullLong(dr, 7)
                    .StudentCount = TestNullLong(dr, 8)
                End With

                details.Add(FTCKickoffRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetFLLWorkshopList", ex.Message.ToString)
            'logger.Error(strErr)
            Throw New Exception(strErr)

        Finally
            m_cFFWebsiteDB.cmd.Dispose()
            m_cFFWebsiteDB.CloseDataReader()
            m_cFFWebsiteDB.CloseConnection()
        End Try

        m_cFFWebsiteDB = Nothing

        Return details
    End Function



    Public Function ValidateUserID(ByVal sUserID As String, ByVal sPassword As String) As Boolean
        '---------------------------------------------------------------------------------------
        'Function:	ValidateUserID
        'Purpose:	Check if a submitted user id is on file and passwords match         
        'Input:     Team Number to read  
        'Returns:   Returns boolean - true found, false - not found  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim obj As Object
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim retBool As Boolean = False
        Dim sPwdOnFile As String = ""

        strSQL = "SELECT Password From FFWebsite.Users U" &
                 " Where U.UserID = " & sUserID

        'Execute SQL Command 
        Try
            obj = m_cFFWebSiteDB.ExecSVQuery(strSQL)
            If Not obj Is Nothing Then
                If TypeOf obj Is String Then
                    sPwdOnFile = CStr(obj)
                End If
            End If
        Catch ex As Exception
            retBool = False
        Finally
            m_cFFWebSiteDB.cmd.Dispose()
            m_cFFWebSiteDB.CloseConnection()
        End Try

        m_cFFWebSiteDB = Nothing

        Return retBool

    End Function

    '---------------------------------------------------------------------------------------------------
    '  Private Functions 
    '---------------------------------------------------------------------------------------------------


    'AES Key Encrypt 
    Private Function AESEncrypt(ByVal plaintext As String, ByVal key As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim ciphertext As String = ""
        Try
            AES.GenerateIV()
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(key))

            AES.Mode = Security.Cryptography.CipherMode.CBC
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(plaintext)
            ciphertext = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Return Convert.ToBase64String(AES.IV) & Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    'AES Key Decrypt 
    Private Function AESDecrypt(ByVal ciphertext As String, ByVal key As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim plaintext As String = ""
        Dim iv As String = ""
        Try
            Dim ivct = ciphertext.Split(CChar("="))
            iv = ivct(0) & "=="
            ciphertext = ivct(2) & "=="

            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(key))
            AES.IV = Convert.FromBase64String(iv)
            AES.Mode = Security.Cryptography.CipherMode.CBC
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(ciphertext)
            plaintext = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return plaintext
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function




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

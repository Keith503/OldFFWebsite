Imports System.Data.Odbc
Imports System.Threading.Tasks

'   Class Name:      cFFWebSiteServer 
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object provides database services for the Frog Force 503 Website     
'   Change history:

Public Class cFFWebSiteServer
    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public sServerName As String = "cFFWebSiteServer"

    Public Function GetNewsItemList() As List(Of cNewsItem)
        '---------------------------------------------------------------------------------------
        'Function:	GetNewsItemList
        'Purpose:	return list of news items       
        'Input:     nothing 
        'Returns:   Returns list new items objects 
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As OdbcDataReader
        Dim details As New List(Of cNewsItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB

        strSQL = "SELECT N.ID, N.Post_date, N.Image1_ID, I1.filename, N.Image2_ID, Images.Filename, N.Image3_ID" &
                 ", Images_1.Filename, N.Priority_ID, N.Author_ID, N.Type_ID, N.Status_ID, N.Event_ID, N.Approved_ID" &
                 ", N.Approved_Date, N.Title_text, N.Body_text " &
                 " FROM ((News AS N LEFT JOIN images AS I1 ON N.Image1_ID = I1.ID) " &
                 " LEFT JOIN Images ON N.Image2_ID = Images.ID) " &
                 " LEFT JOIN Images AS Images_1 ON N.Image3_ID = Images_1.ID " &
                 " ORDER BY N.Post_date DESC "


        'Execute SQL Command 
        Try
            dr = m_cFFWebSiteDB.ExecDRQuery(strSQL)
            While dr.Read()

                Dim NewsRow As New cNewsItem
                With NewsRow
                    .ID = TestNullLong(dr, 0)
                    .Post_Date = TestNullDate(dr, 1)
                    .Image1_ID = TestNullLong(dr, 2)
                    .Image1_Name = TestNullString(dr, 3)
                    .Image2_ID = TestNullLong(dr, 4)
                    .Image2_Name = TestNullString(dr, 5)
                    .Image3_ID = TestNullLong(dr, 6)
                    .Image3_Name = TestNullString(dr, 7)
                    .Priority_ID = TestNullLong(dr, 8)
                    .Author_ID = TestNullLong(dr, 9)
                    .Type_ID = TestNullLong(dr, 10)
                    .Status_ID = TestNullLong(dr, 11)
                    .Event_ID = TestNullLong(dr, 12)
                    .ApprovedBy_ID = TestNullLong(dr, 13)
                    .Approved_Date = TestNullDate(dr, 14)
                    '.Title_text = TestNullString(dr, 15)
                    .Title_text = "Test Title"
                    '.Body_text = TestNullString(dr, 16)
                    .Body_text = "Test Body"
                End With
                details.Add(NewsRow)
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
        Dim dr As OdbcDataReader
        Dim details As New List(Of cEventItem)
        Dim m_cFFWebSiteDB As New cFFWebSiteDB
        Dim i As Integer = 0

        strSQL = "SELECT E.ID, E.Start_date, E.End_date, E.Type_ID, E.Status_ID, E.Student_Lead_ID, TM.Last_name+', '+TM.First_name AS Student_Lead_Name" &
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

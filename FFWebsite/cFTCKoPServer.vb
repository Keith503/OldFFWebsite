Imports MySql.Data.MySqlClient
Imports System.Threading.Tasks

'   Class Name:      cFTCKoPServer 
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object provides database services for the Frog Force FTC Kit of Parts Database     
'   Change history:

Public Class cFTCKoPServer
    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public sServerName As String = "cFTCKoPServer"

    Public Function GetTeamList() As List(Of cTeam)
        '---------------------------------------------------------------------------------------
        'Function:	GetTeamList
        'Purpose:	return list of Teams       
        'Input:     Nothing 
        'Returns:   list of cTypeItem objects containg Team list  
        '----------------------------------------------------------------------------------- ---> 	
        Dim strSQL As String = ""
        Dim dr As MySqlDataReader
        Dim details As New List(Of cTeam)
        Dim m_cFTCKopDB As New cFTCKoPDB

        Dim i As Integer = 0

        strSQL = "select Team_ID,Description_text " &
                 " from FTCKoP.Teams " &
                 " order by Team_ID "

        'Execute SQL Command 
        Try
            dr = m_cFTCKopDB.ExecDRQuery(strSQL)
            While dr.Read()
                i = i + 1
                Dim TeamRow As New cTeam
                With TeamRow
                    .TeamID = TestNullLong(dr, 0)
                    .TeamName = TestNullString(dr, 1)
                End With

                details.Add(TeamRow)

            End While

            dr.Close()

        Catch ex As Exception

            Dim strErr As String = BuildErrorMsg("GetTeamList", ex.Message.ToString)
            Throw New Exception(strErr)

        Finally
            m_cFTCKopDB.cmd.Dispose()
            m_cFTCKopDB.CloseDataReader()
            m_cFTCKopDB.CloseConnection()
        End Try

        m_cFTCKopDB = Nothing

        Return details
    End Function


    Public Function GetInventoryList() As List(Of cInventory)
        '******************************************************************************
        '*  Name:       GetInventoryList 
        '*  Purpose:    Create list of all Parts in Inventory database   
        '*  Input:      Nothing 
        '*  returns:    List of cInventory Objects  
        '******************************************************************************
        Dim details As New List(Of cInventory)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read inventory database for this part number  
        strSQL = "SELECT I.Part_ID, P.Description_text, I.Quantity, I.Quantity_Distributed " &
                 " FROM FTCKoP.Inventory I " &
                 " LEFT OUTER JOIN FTCKoP.Parts P ON I.Part_ID = P.Part_ID " &
                 " ORDER BY I.Part_ID "
        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim InventoryRow As New cInventory
                With InventoryRow
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .Quantity = TestNullLong(dr, 2)
                    .Quantity_Distributed = TestNullLong(dr, 3)
                End With
                details.Add(InventoryRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("GetInventoryList", ex.Message.ToString)
            Throw New Exception(strErr)

        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseDataReader()
            DBServer.CloseConnection()
        End Try

        DBServer = Nothing
        Return details
    End Function

    Public Function GetPartsInKit(ByVal lKitID As Long) As List(Of cKit)
        '******************************************************************************
        '*  Name:       GetPartsInKit()
        '*  Purpose:    Create list of all parts for a given kit in database   
        '*  Input:      KitID 
        '*  returns:    List of cKitPart Objects  
        '******************************************************************************
        Dim details As New List(Of cKit)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read team database   
        strSQL = "SELECT K.Part_ID, P.Description_text, K.Quantity " &
                 " FROM FTCKoP.Kits K " &
                 " LEFT OUTER JOIN FTCKoP.Parts P ON K.Part_ID = P.Part_ID " &
                 " WHERE K.Kit_ID = " & lKitID.ToString &
                 " ORDER BY K.Part_ID "

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim KitPartRow As New cKit
                With KitPartRow
                    .KitID = lKitID
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .Quantity = TestNullLong(dr, 2)
                End With
                details.Add(KitPartRow)
            End While
            dr.Close()
            '
        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error GetPartList:", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseDataReader()
            DBServer.CloseConnection()
        End Try
        DBServer = Nothing
        Return details
    End Function

    Public Function GetPartList() As List(Of cPart)
        '******************************************************************************
        '*  Name:       GetPartList 
        '*  Purpose:    Create list of all Parts in database   
        '*  Input:      Nothing 
        '*  returns:    List of cPart Objects  
        '******************************************************************************
        Dim details As New List(Of cPart)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read Part database   
        strSQL = "Select Part_ID, Description_text, Cost_amount from FTCKoP.Parts order by part_ID"

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim PartRow As New cPart
                With PartRow
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .UnitCost = TestNullDouble(dr, 2)

                End With
                details.Add(PartRow)
            End While
            dr.Close()

        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error getting part list. Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseDataReader()
            DBServer.CloseConnection()
        End Try
        DBServer = Nothing
        Return details
    End Function
    Public Function GetPart(ByVal lPartID As Long) As cPart
        '******************************************************************************
        '*  Name:       GetPart 
        '*  Purpose:    Create and populate a cPart object for a given part number  
        '*  Input:      PartID
        '*  returns:    cPart Object  
        '******************************************************************************
        Dim PartRow As New cPart
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this part number  
        strSQL = "Select Part_ID, Description_text, Cost_amount from FTCKoP.Parts where Part_id = " & lPartID.ToString

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With PartRow
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .UnitCost = TestNullDouble(dr, 2)
                End With
            End While
            dr.Close()

        Catch ex As Exception
            'indicate part not found in return data 
            Dim strErr As String = BuildErrorMsg("Part not found!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseDataReader()
            DBServer.CloseConnection()
        End Try
        DBServer = Nothing
        Return PartRow
    End Function
    Public Function UpdatePart(ByVal lPartID As Long, ByVal sDescription As String, ByVal dUnitCost As Double) As Long
        '******************************************************************************
        '*  Name:       UpdatePart 
        '*  Purpose:    Update Access database with part data 
        '*  Input:      PartID, Description, unit cost
        '*  returns:    ID of part record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim PartRow As cPart

        PartRow = GetPart(lPartID)
        lRet = PartRow.PartID

        'if return part number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Parts(Part_ID, Description_text, Cost_amount) " &
                "values(" & lPartID.ToString & strComma &
                strQuote & sDescription & strQuote & strComma &
                dUnitCost.ToString & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting part! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            Finally
                DBServer.cmd.Dispose()
                DBServer.CloseConnection()
            End Try
        Else
            strSQL = "Update FTCKoP.Parts " &
                        " set Description_text = " & strQuote & sDescription & strQuote & strComma &
                        "  Cost_Amount = " & dUnitCost.ToString("#####.##") &
                        " where Part_id = " & lPartID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating part! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            Finally
                DBServer.cmd.Dispose()
                DBServer.CloseConnection()
            End Try
        End If

        DBServer = Nothing
        Return lRet
    End Function
    Public Function DeletePart(ByVal lPartID As Long) As Long
        '******************************************************************************
        '*  Name:       DeletePart 
        '*  Purpose:    Remove part from team database 
        '*  Input:      PartID
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim PartRow As cPart

        PartRow = GetPart(lPartID)
        lRet = PartRow.PartID

        strSQL = "delete from FTCKoP.Parts " &
                     " where part_id = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            'indicate error to caller   
            Dim strErr As String = BuildErrorMsg("Error deleting Part! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseConnection()
        End Try

        DBServer = Nothing
        Return lRet
    End Function
    Public Function GetTeam(ByVal lTeamID As Long) As cTeam
        '******************************************************************************
        '*  Name:       GetTeam 
        '*  Purpose:    Create and populate a cTeam object for a given team number  
        '*  Input:      TeamID
        '*  returns:    cTeam Object  
        '******************************************************************************
        Dim TeamRow As New cTeam
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this part number  
        strSQL = "Select Team_ID, Description_text from FTCKoP.Teams where Team_id = " & lTeamID.ToString

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With TeamRow
                    .TeamID = TestNullLong(dr, 0)
                    .TeamName = TestNullString(dr, 1)
                End With
            End While
            dr.Close()

        Catch ex As Exception
            'indicate team not found in return data 
            Dim strErr As String = BuildErrorMsg("Team not found!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.cmd.Dispose()
            DBServer.CloseDataReader()
            DBServer.CloseConnection()
        End Try
        DBServer = Nothing

        Return TeamRow
    End Function
    Public Function UpdateTeam(ByVal lTeamID As Long, ByVal sTeamName As String) As Long
        '******************************************************************************
        '*  Name:       UpdateTeam 
        '*  Purpose:    Update Access database with team data 
        '*  Input:      TeamID, TeamName 
        '*  returns:    ID of team Record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim TeamRow As cTeam

        TeamRow = GetTeam(lTeamID)
        lRet = TeamRow.TeamID

        'if return part number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Teams(Team_ID, Description_text) " &
               "values(" & lTeamID.ToString & strComma &
               strQuote & sTeamName & strQuote & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting team! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        Else
            strSQL = "Update FTCKoP.Teams " &
                        " set Description_text = " & strQuote & sTeamName & strQuote &
                        " where Team_id = " & lTeamID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating team! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        End If

        DBServer = Nothing
        Return lRet
    End Function
    Public Function DeleteTeam(ByVal lTeamID As Long) As Long
        '******************************************************************************
        '*  Name:       DeleteTeam()
        '*  Purpose:    Remove team from team database 
        '*  Input:      TeamID
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim TeamRow As cTeam

        TeamRow = GetTeam(lTeamID)
        lRet = TeamRow.TeamID
        strSQL = "delete from FTCKoP.Teams " &
                 " where Team_id = " & lTeamID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            'indicate error to caller   
            Dim strErr As String = BuildErrorMsg("Error deleting team! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        DBServer = Nothing
        Return lRet
    End Function

    Public Function GetKitList() As List(Of cKit_Types)
        '******************************************************************************
        '*  Name:       GetKitList 
        '*  Purpose:    Create list of all kits in database   
        '*  Input:      Nothing 
        '*  returns:    List of cKit Objects  
        '******************************************************************************
        Dim details As New List(Of cKit_Types)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read team database   
        strSQL = "Select Kit_ID, Description_text from FTCKoP.Kit_Types order by kit_ID"

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim KitRow As New cKit_Types
                With KitRow
                    .KitID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                End With
                details.Add(KitRow)
            End While
            dr.Close()

        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("GetKitList", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return details
    End Function
    Public Function GetKit(ByVal lKitID As Long) As cKit_Types
        '******************************************************************************
        '*  Name:       GetKit 
        '*  Purpose:    Create and populate a cKit object for a given kit number  
        '*  Input:      KitID
        '*  returns:    cKit Object  
        '******************************************************************************
        Dim KitRow As New cKit_Types
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this kit number  
        strSQL = "Select Kit_ID, Description_text from FTCKoP.Kit_Types where Kit_id = " & lKitID.ToString

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With KitRow
                    .KitID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                End With
            End While
            dr.Close()

        Catch ex As exception
            'indicate kit not found in return data 
            Dim strErr As String = BuildErrorMsg("Kit not found!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return KitRow
    End Function

    Public Function UpdateKit(ByVal lKitID As Long, ByVal sDesc As String) As Long
        '******************************************************************************
        '*  Name:       UpdateKit 
        '*  Purpose:    Update Access database with kit data 
        '*  Input:      KitID, Description 
        '*  returns:    ID of Kit Record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim KitRow As cKit_Types

        KitRow = GetKit(lKitID)
        lRet = KitRow.KitID

        'if return Kit number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Kit_Types(Kit_ID, Description_text) " &
                "values(" & lKitID.ToString & strComma &
                strQuote & sDesc & strQuote & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting kit! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        Else
            strSQL = "Update FTCKoP.Kit_Types " &
                         " set Description_text = " & strQuote & sDesc & strQuote &
                         " where Kit_id = " & lKitID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating kit! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        End If
        DBServer = Nothing
        Return lRet
    End Function
    Public Function DeleteKit(ByVal lKitID As Long) As Long
        '******************************************************************************
        '*  Name:       DeleteKit 
        '*  Purpose:    Remove Kit from team database 
        '*  Input:      KitID
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim KitRow As cKit_Types

        KitRow = GetKit(lKitID)
        lRet = KitRow.KitID
        strSQL = "delete from FTCKoP.Kit_Types " &
                     " where Kit_id = " & lKitID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            'indicate error to caller   
            Dim strErr As String = BuildErrorMsg("Error deleting kit! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try
        DBServer = Nothing
        Return lRet
    End Function

    Public Function UpdateInventory(ByVal lPartID As Long, ByVal lQuantity As Long, ByVal lQuantity_Distributed As Long) As Long
        '******************************************************************************
        '*  Name:       UpdateInventory 
        '*  Purpose:    Update database with inventory data 
        '*  Input:      PartID, Quantity, and Qantity_Distributed 
        '*  returns:    ID of part record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim InventoryRow As cInventory
        Dim lNewQuantity As Long
        Dim lNewDistributed As Long

        InventoryRow = GetInventorybyPart(lPartID)
        lRet = InventoryRow.PartID

        'if return part number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Inventory(Part_ID, Quantity, Quantity_distributed) " &
                "values(" & lPartID.ToString & strComma &
                lQuantity.ToString & strComma &
                lQuantity_Distributed.ToString & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting inventory! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        Else
            'calculate quantity as the current quantity plus the new quantity 
            lNewQuantity = InventoryRow.Quantity + lQuantity
            lNewDistributed = InventoryRow.Quantity_Distributed + lQuantity_Distributed
            'lNewDistributed = 0

            strSQL = "Update FTCKoP.Inventory " &
                         " set Quantity = " & lNewQuantity.ToString & strComma &
                         "  Quantity_Distributed = " & lNewDistributed.ToString &
                         " where Part_id = " & lPartID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating inventory! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        End If
        DBServer = Nothing
        Return lRet
    End Function
    Public Function GetInventorybyPart(ByVal lPartID As Long) As cInventory
        '******************************************************************************
        '*  Name:       GetInventorybyPart 
        '*  Purpose:    Create and populate a cInventory object for a given part number  
        '*  Input:      PartID
        '*  returns:    cInventory Object  
        '******************************************************************************
        Dim InventoryRow As New cInventory
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this part number  
        strSQL = "SELECT I.Part_ID, P.Description_text, I.Quantity, I.Quantity_Distributed " &
                 " FROM FTCKoP.Inventory I LEFT OUTER JOIN FTCKoP.Parts P ON I.Part_ID = P.Part_ID " &
                 " WHERE I.Part_ID = " & lPartID.ToString

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With InventoryRow
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .Quantity = TestNullLong(dr, 2)
                    .Quantity_Distributed = TestNullLong(dr, 3)
                End With
            End While
            dr.Close()

        Catch ex As Exception
            'indicate part not found in return data 
            Dim strErr As String = BuildErrorMsg("Part not found in Inventory!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()
            DBServer = Nothing
        End Try

        Return InventoryRow
    End Function
    Public Function UpdateTeamInventory(ByVal lTeamID As Long, ByVal lPartID As Long, ByVal lQuantity As Long) As Long
        '******************************************************************************
        '*  Name:       UpdateTeamInventory 
        '*  Purpose:    Update Access database with Team inventory data 
        '*  Input:      Team_ID, PartID, Quantity, Returned Quantity  
        '*  returns:    ID of part record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim Team_InventoryRow As New cTeam_Inventory
        Dim lNewQuantity As Long

        Try
            Team_InventoryRow = GetTeamInventorybyPart(lTeamID, lPartID)
            lRet = Team_InventoryRow.PartID
        Catch ex As Exception
            lRet = 0
        End Try

        'if return part number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Team_Inventory(Team_ID, Part_ID, Quantity) " &
                "values(" & lTeamID.ToString & strComma & lPartID.ToString & strComma &
                lQuantity.ToString & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting team inventory! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        Else
            'calculate quantity as the current quantity plus the new quantity 
            lNewQuantity = Team_InventoryRow.Quantity + lQuantity

            strSQL = "Update FTCKoP.Team_Inventory " &
                         " set Quantity = " & lNewQuantity.ToString &
                         " where Team_id = " & lTeamID.ToString &
                         " and Part_id = " & lPartID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating Team inventory! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        End If
        DBServer = Nothing
        Return lRet
    End Function
    Public Function GetTeamInventorybyPart(ByVal lTeamID As Long, ByVal lPartID As Long) As cTeam_Inventory
        '*****************************************************************************
        '*  Name:       GetTeamInventorybyPart 
        '*  Purpose:    Create and populate a cInventory object for a given team & part number  
        '*  Input:      TeamID, PartID 
        '*  returns:    cTeam_Inventory Object  
        '******************************************************************************
        Dim Team_InventoryRow As New cTeam_Inventory
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this part number  
        strSQL = "SELECT I.Part_ID, P.Description_text, I.Quantity, I.Returned " &
                     " FROM FTCKoP.Team_Inventory I LEFT OUTER JOIN FTCKoP.Parts P ON I.Part_ID = P.Part_ID " &
                     " WHERE I.Team_ID = " & lTeamID.ToString &
                     " and I.Part_ID = " & lPartID.ToString
        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With Team_InventoryRow
                    .PartID = TestNullLong(dr, 0)
                    .Description = TestNullString(dr, 1)
                    .Quantity = TestNullLong(dr, 2)
                    .Returned = TestNullLong(dr, 3)
                End With
            End While
            dr.Close()
            '
        Catch ex As Exception
            'indicate Part Not found in return data 
            Dim strErr As String = BuildErrorMsg("Team not found in Inventory!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return Team_InventoryRow
    End Function
    Public Function GetKitPartTeamInvList(ByVal lTeamID As Long, ByVal KitID As Integer) As List(Of cKitPartTeamInv)
        '******************************************************************************
        '*  Name:       GetKitPartTeamInvList 
        '*  Purpose:    Create list of all parts in a kit    
        '*  Input:      Nothing 
        '*  returns:    List of cKitPartTeamInv Objects  
        '******************************************************************************
        Dim details As New List(Of cKitPartTeamInv)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader
        Dim strWork As String = ""

        'THis allowed for a list of kit ids to be passed-changed to allow on a single kit id 
        'For i = 0 To KitList.Count - 1
        ' strWork = strWork + KitList(i).ToString + ","
        ' Next
        'strWork = Mid(strWork, 1, Len(strWork - 1))
        strWork = CStr(KitID)

        'Go read team database   
        strSQL = "SELECT Kit_Types.Description_text AS Kit_Name, Kits.Part_ID, Parts.Description_text, Kits.Quantity AS Kit_Quantity, Team_Inventory.Quantity AS Team_Quantity, " &
                     " Team_Inventory.Quantity - Kits.Quantity AS Difference " &
                     " FROM FTCKoP.Kits INNER JOIN FTCKoP.Parts ON Kits.Part_ID = Parts.Part_ID " &
                     "  INNER JOIN FTCKoP.Kit_Types ON Kits.Kit_ID = Kit_Types.Kit_ID " &
                     "  LEFT OUTER JOIN FTCKoP.Team_Inventory ON Kits.Part_ID = Team_Inventory.Part_ID " &
                     " WHERE Kits.Kit_ID IN (" & strWork & ") " &
                     " And Team_Inventory.Team_ID = " & lTeamID.ToString &
                     " ORDER BY Kit_Types.Description_text, Kits.Part_ID "

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim InvRow As New cKitPartTeamInv

                With InvRow
                    .KitName = dr.GetString(0)
                    .PartID = dr.GetValue(1)
                    .PartName = dr.GetString(2)
                    .KitQuantity = dr.GetValue(3)
                    .TeamQuantity = dr.GetValue(4)
                    .Difference = dr.GetValue(5)
                End With
                details.Add(InvRow)
            End While
            dr.Close()

        Catch ex As Exception
            'indicate error to caller 
            Dim strErr As String = BuildErrorMsg("Error getting GetKitPartTeamInvList. Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try

        DBServer = Nothing
        Return details
    End Function

    Public Function DeleteInventory(ByVal lPartID As Long) As Long
        '******************************************************************************
        '*  Name:       DeleteInventory
        '*  Purpose:    Remove part from inventory database 
        '*  Input:      PartID
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0

        strSQL = "delete from FTCKoP.Parts " &
                     " where part_id = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            'indicate error to caller   
            Dim strErr As String = BuildErrorMsg("Error deleting Inventory! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        Return lRet
    End Function

    Public Function GetKitPart(ByVal lKitID As Long, ByVal lPartID As Long) As cKit
        '******************************************************************************
        '*  Name:       GetKitPart 
        '*  Purpose:    Create and populate a cKitPart object for a given kit and Part number  
        '*  Input:      KitID, PartID 
        '*  returns:    cKitPart Object  
        '******************************************************************************
        Dim KitPartRow As New cKit
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this kit and part number  
        strSQL = "Select Quantity from FTCKoP.Kits where Kit_id = " & lKitID.ToString &
                 " and Part_ID = " & lPartID.ToString

        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With KitPartRow
                    .KitID = lKitID
                    .PartID = lPartID
                    .Quantity = dr.GetValue(0)
                End With
            End While
            dr.Close()

        Catch ex As Exception
            'indicate kitpart not found in return data 
            Dim strErr As String = BuildErrorMsg("Kit and part not found!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return KitPartRow
    End Function

    Public Function UpdateKitPart(ByVal lKitID As Long, lPartID As Long, ByVal lQty As Long) As Long
        '******************************************************************************
        '*  Name:       UpdateKitPart 
        '*  Purpose:    Update Access database with kitpart data 
        '*  Input:      KitID, PartId, Quantity 
        '*  returns:    ID of KitPart Record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim KitPartRow As cKit

        KitPartRow = GetKitPart(lKitID, lPartID)
        lRet = KitPartRow.KitID

        'if return Kit number is zero - must be an insert else update 
        If lRet = 0 Then
            strSQL = "Insert into FTCKoP.Kits(Kit_ID, Part_Id, Quantity) " &
                "values(" & lKitID.ToString & strComma &
                lPartID.ToString & strComma &
                lQty.ToString & ")"

            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller 
                Dim strErr As String = BuildErrorMsg("Error inserting kitpart! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        Else
            strSQL = "Update Kits " &
                         " set Quantity = " & lQty.ToString &
                         " where Kit_id = " & lKitID.ToString &
                         " and part_ID = " & lPartID.ToString
            Try
                DBServer.ExecNonQuery(strSQL)
            Catch ex As Exception
                'indicate error to caller   
                Dim strErr As String = BuildErrorMsg("Error updating kitpart! Msg-", ex.Message.ToString)
                Throw New Exception(strErr)
            End Try
        End If

        Return lRet
    End Function
    Public Function DeleteKitPart(ByVal lKitID As Long, ByVal lPartID As Long) As Long
        '******************************************************************************
        '*  Name:       DeleteKitPart 
        '*  Purpose:    Remove KitPart from team database 
        '*  Input:      KitID, PartID 
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0

        strSQL = "delete from FTCKoP.Kits " &
                     " where Kit_id = " & lKitID.ToString &
                     " and part_ID = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("Error deleting kitpart! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        Return lRet
    End Function

    Public Function GetInventorybyTeam(ByVal lTeamID As Long) As cTeam_Inventory
        '******************************************************************************
        '*  Name:       GetInventorybyTeam 
        '*  Purpose:    Create and populate a cInventory object for a given part number  
        '*  Input:      TeamID
        '*  returns:    cTeam_Inventory Object  
        '******************************************************************************
        Dim Team_InventoryRow As New cTeam_Inventory
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read part database for this part number  
        strSQL = "SELECT I.Part_ID, P.Description_text, I.Quantity, I.Returned " &
                     " FROM FTCKoP.Team_Inventory I LEFT OUTER JOIN Parts P ON I.Part_ID = P.Part_ID " &
                     " WHERE I.Team_ID = " & lTeamID.ToString
        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                With Team_InventoryRow
                    .PartID = dr.GetValue(0)
                    .Description = dr.GetString(1)
                    .Quantity = dr.GetValue(2)
                    .Returned = dr.GetValue(3)
                End With
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("Team not found in Inventory!", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return Team_InventoryRow
    End Function

    Public Function GetTeamInventoryList(ByVal lTeamID As Long) As List(Of cTeam_Inventory)
        '******************************************************************************
        '*  Name:       GetTeamInventoryList 
        '*  Purpose:    Create list of all Parts in Team Inventory database for a given team    
        '*  Input:      Team ID  
        '*  returns:    List of cTeam_Inventory Objects  
        '******************************************************************************
        Dim details As New List(Of cTeam_Inventory)
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim dr As MySqlDataReader

        'Go read inventory database for this part number  
        strSQL = "SELECT I.Part_ID, P.Description_text, I.Quantity, I.Returned " &
                     " FROM FTCKoP.Team_Inventory I LEFT OUTER JOIN FTCKoP.Parts P ON I.Part_ID = P.Part_ID " &
                     " WHERE I.Team_ID = " & lTeamID.ToString &
                     " ORDER BY I.Part_ID "
        Try
            dr = DBServer.ExecDRQuery(strSQL)
            While dr.Read
                Dim Team_InventoryRow As New cTeam_Inventory
                With Team_InventoryRow '
                    .PartID = dr.GetValue(0)
                    .Description = dr.GetString(1)
                    .Quantity = dr.GetValue(2)
                    If IsDBNull(dr.GetValue(3)) Then
                        .Returned = 0
                    Else
                        .Returned = dr.GetValue(3)
                    End If

                End With
                details.Add(Team_InventoryRow)
            End While
            dr.Close()

        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("Error getting Team inventory list. Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        Finally
            DBServer.CloseConnection()

        End Try
        DBServer = Nothing
        Return details
    End Function

    Public Function UpdateReturned(ByVal lTeamID As Long, ByVal lPartID As Long, ByVal lRetQuantity As Long) As Long
        '******************************************************************************
        '*  Name:       UpdateReturned 
        '*  Purpose:    Update Access database with new Returned Quantity  
        '*  Input:      Team_ID, PartID, Returned Quantity  
        '*  returns:    ID of part record '
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0
        Dim TeamInv As cTeam_Inventory
        TeamInv = GetTeamInventorybyPart(lTeamID, lPartID)
        If lRetQuantity > TeamInv.Quantity Then
            Throw New System.Exception("Error updating Team Inventory Returned Quantity > Quantity!")
        End If

        strSQL = "Update FTCKoP.Team_Inventory " &
                 " set Returned = " & lRetQuantity.ToString &
                 " where Team_id = " & lTeamID.ToString &
                 " and Part_id = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("Error updating Team Inventory returned Quantity! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        TeamInv = Nothing
        Return lRet
    End Function
    Public Function ResetReturned(ByVal lTeamID As Long, ByVal lPartID As Long, ByVal lRetQuantity As Long) As Long
        '******************************************************************************
        '*  Name       ResetReturned()
        '*  Purpose:    Update team inventory by moving returned to quantity and zeroing out returned   
        '*  Input:      Team_ID, PartID, Returned Quantity  
        '*  returns:    ID of part record 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0

        strSQL = "Update FTCKoP.Team_Inventory " &
                         " set Quantity = " & lRetQuantity.ToString &
                         " , Returned = " & "0" &
                         " where Team_id = " & lTeamID.ToString &
                         " and Part_id = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
        Catch ex As Exception
            Dim strErr As String = BuildErrorMsg("Error reseting team Inventory with returned Quantity! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        Return lRet
    End Function


    Public Function DeleteTeamInventory(ByVal lTeamID As Long, ByVal lPartID As Long) As Long
        '******************************************************************************
        '*  Name:       DeleteTeamInventory
        '*  Purpose:    Remove part from team inventory database 
        '*  Input:      TeamID, PartID
        '*  returns:    return code (0-good, -1 error) 
        '******************************************************************************
        Dim strSQL As String
        Dim DBServer As New cFTCKoPDB
        Dim lRet As Long = 0

        strSQL = "delete from FTCKoP.Team_Iventory " &
                     " where team_id = " & lTeamID.ToString &
                     " and part_id = " & lPartID.ToString
        Try
            DBServer.ExecNonQuery(strSQL)
            lRet = 0
        Catch ex As Exception
            'indicate error to caller   
            Dim strErr As String = BuildErrorMsg("Error deleting Team Inventory! Msg-", ex.Message.ToString)
            Throw New Exception(strErr)
        End Try

        DBServer = Nothing
        Return lRet
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

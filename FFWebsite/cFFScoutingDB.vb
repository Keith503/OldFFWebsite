'   Class Name:      cFFScoutingDB 
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object provides connection services to the Frog Force Website Database. 
'                    The database is the default sql server database that comes with Visual Studio 
'   Change history:
'
Imports MySql.Data.MySqlClient

Public Class cFFScoutingDB
    Public cn As MySqlConnection
    Public dr As MySqlDataReader
    Public cmd As MySqlCommand


    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Public Function GetConnection() As MySqlConnection
        Dim sConn As String

        'Build connection string   
        'sConn = "DSN=TEST;UID=" & sUserID & ";PWD=" & sPassword & ";"
        'sConn = "DSN=FFWebsite" & ";"
        'sConn = "server=50.62.209.111;User ID=FFAdmin;Password=17@Frog01;Database=FFWebsite;pooling=False"
        'sConn = "server=FFCollector;User ID=ffuser;Password=ff503;Database=FFScouting;pooling=False"
        'sConn = "server=100.86.182.232;User ID=ffuser;Password=ff503;Database=FFScouting;pooling=False"
        sConn = "server=50.62.209.111;User ID=FFAdmin;Password=17@Frog01;Database=ffscouting;pooling=true"
        If cn Is Nothing Then
            Try
                cn = New MySqlConnection(sConn)
                cn.Open()
            Catch o As MySqlException
                'logger.Error("cFFWebDB:GetConnection - " & o.Message.ToString)
                Throw New Exception(o.Message.ToString)
            End Try
        End If

        'Return to caller 
        Return cn
    End Function
    Public Sub CloseConnection()
        If (cn IsNot Nothing) Then
            If cn.State = ConnectionState.Open Then
                cn.Close()
                cn = Nothing
            End If
        End If

    End Sub
    Public Sub CloseDataReader()
        If dr.IsClosed Then
        Else
            dr.Close()
            dr = Nothing
        End If
        'Go close database connection 
        CloseConnection()
    End Sub
    Public Function ExecNonQuery(strSQL As String) As Long
        'Dim cmd As MySQLCommand
        Dim lRetCode As Long = 0

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New MySqlCommand(strSQL, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch o As MySqlException
            lRetCode = o.ErrorCode
            Throw New Exception(o.Message.ToString)
        Finally
            'Go close database connection 
            CloseConnection()
        End Try

        Return lRetCode
    End Function

    Public Function ExecDRQuery(strSQL As String) As MySqlDataReader
        'Dim cmd As MySQLCommand

        Dim lRetCode As Long = 0

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New MySqlCommand(strSQL, cn)
            dr = cmd.ExecuteReader()
            'cmd.Dispose()
        Catch o As MySqlException
            'logger.Error("Error cFFWebDB:ExecDRQuery - " & o.Message.ToString)
            lRetCode = o.ErrorCode
            Throw New Exception(o.Message.ToString)
        Finally
            'CloseConnection()
        End Try

        Return dr
    End Function

    Public Function ExecSVQuery(strSQL As String) As Object
        'Dim cmd As MySQLCommand
        Dim oRetData As Object

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New MySqlCommand(strSQL, cn)
            oRetData = cmd.ExecuteScalar()
            cmd.Dispose()
        Catch o As MySqlException
            Throw New Exception(o.Message.ToString)
        Finally
            'Go close database connection 
            CloseConnection()
        End Try

        Return oRetData
    End Function

End Class

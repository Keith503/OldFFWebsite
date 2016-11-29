'   Class Name:      cFFWebSiteDB 
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object provides connection services to the Frog Force Website Database. 
'                    The database is the default sql server database that comes with Visual Studio 
'   Change history:

Imports System.Data.Odbc


Public Class cFFWebSiteDB
    Public cn As OdbcConnection
    Public dr As OdbcDataReader
    Public cmd As OdbcCommand


    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Public Function GetConnection() As OdbcConnection
        Dim sConn As String

        'Build connection string   
        'sConn = "DSN=TEST;UID=" & sUserID & ";PWD=" & sPassword & ";"
        sConn = "DSN=FFWebsite" & ";"

        If cn Is Nothing Then
            Try
                cn = New OdbcConnection(sConn)
                cn.Open()
            Catch o As OdbcException
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
        'Dim cmd As OdbcCommand
        Dim lRetCode As Long = 0

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New OdbcCommand(strSQL, cn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
        Catch o As OdbcException
            lRetCode = o.ErrorCode
            Throw New Exception(o.Message.ToString)
        Finally
            'Go close database connection 
            CloseConnection()
        End Try

        Return lRetCode
    End Function

    Public Function ExecDRQuery(strSQL As String) As OdbcDataReader
        'Dim cmd As OdbcCommand

        Dim lRetCode As Long = 0

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New OdbcCommand(strSQL, cn)
            dr = cmd.ExecuteReader()
            cmd.Dispose()
        Catch o As OdbcException
            'logger.Error("Error cFFWebDB:ExecDRQuery - " & o.Message.ToString)
            lRetCode = o.ErrorCode
            Throw New Exception(o.Message.ToString)
        Finally
            CloseConnection()
        End Try

        Return dr
    End Function

    Public Function ExecSVQuery(strSQL As String) As Object
        'Dim cmd As OdbcCommand
        Dim oRetData As Object

        'Go get a connection to database 
        cn = GetConnection()
        'Execute SQL Command  
        Try
            cmd = New OdbcCommand(strSQL, cn)
            oRetData = cmd.ExecuteScalar()
            cmd.Dispose()
        Catch o As OdbcException
            Throw New Exception(o.Message.ToString)
        Finally
            'Go close database connection 
            CloseConnection()
        End Try

        Return oRetData
    End Function

End Class


Imports MySql.Data.MySqlClient

Module mFFWebsite
    Public strComma As String = ","
    Public strQuote As String = "'"
    Public strPound As String = "#"
    Public strDblQuote As String = Chr(34)

    Public ReadOnly Property Epoch() As DateTime
        Get
            Return New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        End Get
    End Property

    Public Function FromUnix(ByVal seconds As Integer, local As Boolean) As DateTime
        Dim dt = Epoch.AddSeconds(seconds)
        If local Then dt = dt.ToLocalTime
        Return dt
    End Function

    Public Function ToUnix(ByVal dt As DateTime) As Integer
        Dim i As Integer
        Dim localZone As TimeZone = TimeZone.CurrentTimeZone
        If dt.Kind = DateTimeKind.Local Then dt = dt.ToUniversalTime
        Dim currentOffset As TimeSpan = localZone.GetUtcOffset(dt)
        dt = dt - currentOffset
        i = CInt((dt - Epoch).TotalSeconds)
        Return i
    End Function

    Public Function GetDate(sType As String) As Date
        'return date object with correct date based on request code 
        Dim dToday As Date = Date.Now
        Dim dretDate As Date
        'Get current date  
        dToday = New Date(dToday.Year, dToday.Month, dToday.Day, 0, 0, 0)
        Select Case sType
            Case "D"    ' Current Date 
                dretDate = dToday
            Case "T"
                dretDate = dToday.AddDays(1)
            Case "W"
                dretDate = dToday.AddDays(-7)
            Case "M"
                dretDate = dToday.AddDays(-31)
            Case "Y"
                dretDate = dToday.AddDays(-365)
            Case "PW"
                dretDate = dToday.AddDays(-7)
            Case "O"
                dretDate = New DateTime(2015, 1, 1)
            Case "PO"
                dretDate = New DateTime(2014, 1, 1)
            Case "PD"     'Previous Day 
                dretDate = dToday.AddDays(-1)
            Case "PM"     'last day of previous month 
                dretDate = New DateTime(dToday.Year, dToday.Month, 1).AddDays(-1)
            Case "SM"     'first day of current month 
                dretDate = New DateTime(dToday.Year, dToday.Month, 1)
            Case "SP"     'start day of previous of previous month 
                dretDate = New DateTime(dToday.Year, dToday.Month, 1).AddMonths(-1)
        End Select
        Return dretDate

    End Function
    Public Function DateCalc(sFunction As String, dInDate As Date) As Date
        'return date object with correct date based on request code 
        Dim dToday As Date = Date.Now
        Dim dretDate As Date
        Dim i As Integer
        Dim iWeekday As Integer
        Dim iDiff As Integer

        'Get current date  
        dToday = New Date(dToday.Year, dToday.Month, dToday.Day)
        Select Case sFunction
            Case "P"     ' previous day 
                dretDate = dInDate.AddDays(-1)
            Case "PB"    ' Previous Business Day 
                i = dInDate.DayOfWeek
                'check for sunday or monday 
                If i = DayOfWeek.Sunday Then
                    dretDate = dInDate.AddDays(-2)
                Else
                    If i = DayOfWeek.Monday Then
                        dretDate = dInDate.AddDays(-3)
                    Else
                        dretDate = dInDate.AddDays(-1)
                    End If
                End If
            Case "WS"     ' Weekstart - find previous monday 
                iWeekday = dInDate.DayOfWeek      ' find what day of week the submitted date is (0-Sunday) 
                If iWeekday = 1 Then              ' if date is a monday we are done 
                    dretDate = dInDate
                Else
                    If iWeekday = 0 Then         ' if sunday we need to subtract 7 days 
                        iDiff = 7
                    Else
                        iDiff = iWeekday - 1
                    End If
                    iDiff = (iDiff) * -1
                    dretDate = dInDate.AddDays(iDiff)
                End If

            Case "WE"     ' Weekend - find following sunday  
                iWeekday = dInDate.DayOfWeek
                If iWeekday = 0 Then
                    dretDate = dInDate
                Else
                    iDiff = 7 - iWeekday
                    dretDate = dInDate.AddDays(iDiff)
                End If

        End Select
        Return dretDate

    End Function





    Public Function GetDateList(ByVal dStartDate As Date, ByVal dEndDate As Date) As List(Of Date)
        'returns list of dates between start and end dates 
        Dim datelist As New List(Of Date)
        Dim daterow As Date
        Dim yy As Integer
        Dim mm As Integer

        yy = dStartDate.Year
        mm = dStartDate.Month

        'for start date equate to first day of month 
        daterow = New DateTime(yy, mm, 1)
        Do While daterow <= dEndDate
            datelist.Add(daterow)
            daterow = DateAdd(DateInterval.Month, 1, daterow)
        Loop

        Return datelist
    End Function

    Public Function GetDateTimeList(ByVal dStartDate As Date, ByVal dEndDate As Date, ByVal iInterval As Integer) As List(Of Date)
        'returns list of dates between start and end dates 
        Dim datelist As New List(Of Date)
        Dim ddaterow As Date

        'just in case we got a day and time as input, force to start at beginning of day  
        ddaterow = New DateTime(dStartDate.Year, dStartDate.Month, dStartDate.Day, 0, 0, 0)
        Do While ddaterow <= dEndDate
            datelist.Add(ddaterow)
            ddaterow = ddaterow.AddMinutes(iInterval)
        Loop

        Return datelist
    End Function

    Public Function TestNullString(ByRef dr As MySQLDataReader, ByVal ord As Integer) As String
        Dim RetString As String = ""

        If Not dr.IsDBNull(ord) Then
            RetString = dr.GetString(ord)
        End If

        Return RetString
    End Function
    Public Function TestNullChar(ByRef dr As MySQLDataReader, ByVal ord As Integer) As String
        Dim RetChar As String = "0"

        If Not dr.IsDBNull(ord) Then
            RetChar = dr.GetValue(ord)
        End If

        Return RetChar
    End Function
    Public Function TestNullLong(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Long
        Dim RetLong As Long = 0

        If Not dr.IsDBNull(ord) Then
            RetLong = dr.GetValue(ord)
        End If

        Return RetLong
    End Function
    Public Function TestNullDate(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Date
        Dim Retdate As New Date(2000, 1, 1, 12, 0, 0)

        If Not dr.IsDBNull(ord) Then
            Retdate = dr.GetDateTime(ord)
        End If

        Return Retdate
    End Function

    Public Function TestRealNullDate(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Nullable(Of Date)
        Dim Retdate As Nullable(Of Date)

        If Not dr.IsDBNull(ord) Then
            Retdate = dr.GetDateTime(ord)
        End If

        Return Retdate
    End Function

    Public Function TestNullDouble(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Double
        Dim RetDBL As Double = 0

        If Not dr.IsDBNull(ord) Then
            RetDBL = dr.GetValue(ord)
        End If

        Return RetDBL
    End Function

    Public Function TestNullSingle(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Single
        Dim RetSing As Single = 0

        If Not dr.IsDBNull(ord) Then
            RetSing = dr.GetValue(ord)
        End If

        Return RetSing
    End Function

    Public Function TestNullBoolean(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Boolean
        Dim RetBool As Boolean = False

        If Not dr.IsDBNull(ord) Then
            RetBool = dr.GetBoolean(ord)
        End If

        Return RetBool
    End Function

    Public Function TestNullDecimal(ByRef dr As MySQLDataReader, ByVal ord As Integer) As Decimal
        Dim RetDec As Decimal = 0

        If Not dr.IsDBNull(ord) Then
            RetDec = dr.GetDecimal(ord)
        End If

        Return RetDec
    End Function
    Public Function RemoveQuotes(ByVal inStr As String) As String
        'removes all quotes from input string 
        Dim RetStr As String = ""
        Dim i As Integer

        For i = 1 To Len(inStr)
        Next

        If Mid(inStr, i, 1) <> strQuote Then
            If Mid(inStr, i, 1) <> strDblQuote Then
                RetStr = RetStr + Mid(inStr, i, 1)
            End If
        End If
        Return RetStr
    End Function
End Module

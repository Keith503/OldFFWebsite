
Public Class cTabletDataFixed
    Private m_ID As Long
    Private m_EventID As Long
    Private m_Key As String
    Private m_ScoutName As String
    Private m_TeamNumber As Long
    Private m_MatchNumber As Long
    Private m_Alliance As String
    Private m_BreachLineA As Boolean
    Private m_ScoreGearA As Boolean
    Private m_GearLocation As String
    Private m_ScoreHighFuelA As Long
    Private m_ScoreatLeast50A As Boolean
    Private m_ScoreLowFuelA As Boolean
    Private m_ScoreGearT As Long
    Private m_ScoreHighFuelT As Long
    Private m_TotalHighFuelScore As Long
    Private m_ScoreLowFuelT As Boolean
    Private m_Climb As Boolean
    Private m_ClimbLocation As String
    Private m_DropGears As Long
    Private m_TechDiff As String

    Public Property ID() As Long
        Get
            Return m_ID
        End Get
        Set(ByVal value As Long)
            m_ID = value
        End Set
    End Property

    Public Property EventID() As Long
        Get
            Return m_EventID
        End Get
        Set(ByVal value As Long)
            m_EventID = value
        End Set
    End Property

    '<CsvColumn(Name:="Key", FieldIndex:=1)>
    Public Property Key() As String
        Get
            Return m_Key
        End Get
        Set(value As String)
            m_Key = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoutName", FieldIndex:=2)> _
    '<CsvColumn(FieldIndex:=2)>
    Public Property ScoutName() As String
        Get
            Return m_ScoutName
        End Get
        Set(value As String)
            m_ScoutName = value
        End Set
    End Property

    '<CsvColumn(Name:="TeamNumber", FieldIndex:=3)> _
    '<CsvColumn(FieldIndex:=3)>
    Public Property TeamNumber() As Long
        Get
            Return m_TeamNumber
        End Get
        Set(value As Long)
            m_TeamNumber = value
        End Set
    End Property

    '<CsvColumn(Name:="MatchNumber", FieldIndex:=4)> _
    '<CsvColumn(FieldIndex:=4)>
    Public Property MatchNumber() As Long
        Get
            Return m_MatchNumber
        End Get
        Set(value As Long)
            m_MatchNumber = value
        End Set
    End Property

    '<CsvColumn(Name:="Alliance", FieldIndex:=5)> _
    '<CsvColumn(FieldIndex:=5)>
    Public Property Alliance() As String
        Get
            Return m_Alliance
        End Get
        Set(value As String)
            m_Alliance = value
        End Set
    End Property

    '<CsvColumn(Name:="BreachLineA", FieldIndex:=6)> _
    '<CsvColumn(FieldIndex:=6)>
    Public Property BreachLineA() As Boolean
        Get
            Return m_BreachLineA
        End Get
        Set(value As Boolean)
            m_BreachLineA = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoreGearA", FieldIndex:=7)> _
    '<CsvColumn(FieldIndex:=7)>
    Public Property ScoreGearA() As Boolean
        Get
            Return m_ScoreGearA
        End Get
        Set(value As Boolean)
            m_ScoreGearA = value
        End Set
    End Property



    '<CsvColumn(Name:="GearLocation", FieldIndex:=8)> _
    '<CsvColumn(FieldIndex:=8)>
    Public Property GearLocation() As String
        Get
            Return m_GearLocation
        End Get
        Set(value As String)
            m_GearLocation = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoreHighFuelA", FieldIndex:=9)> _
    '<CsvColumn(FieldIndex:=9)>
    Public Property ScoreHighFuelA() As Long
        Get
            Return m_ScoreHighFuelA
        End Get
        Set(value As Long)
            m_ScoreHighFuelA = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoreatLeast50A", FieldIndex:=10)> _
    '<CsvColumn(FieldIndex:=10)>
    Public Property ScoreatLeast50A() As Boolean
        Get
            Return m_ScoreatLeast50A
        End Get
        Set(value As Boolean)
            m_ScoreatLeast50A = value
        End Set
    End Property


    '<CsvColumn(Name:="ScoreLowFuelA", FieldIndex:=11)> _
    '<CsvColumn(FieldIndex:=11)>
    Public Property ScoreLowFuelA() As Boolean
        Get
            Return m_ScoreLowFuelA
        End Get
        Set(value As Boolean)
            m_ScoreLowFuelA = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoreGearT", FieldIndex:=12)> _
    '<CsvColumn(FieldIndex:=12)>
    Public Property ScoreGearT() As Long
        Get
            Return m_ScoreGearT
        End Get
        Set(value As Long)
            m_ScoreGearT = value
        End Set
    End Property

    '<CsvColumn(Name:="ScoreHighFuelT", FieldIndex:=13)> _
    '<CsvColumn(FieldIndex:=13)>
    Public Property ScoreHighFuelT() As Long
        Get
            Return m_ScoreHighFuelT
        End Get
        Set(value As Long)
            m_ScoreHighFuelT = value
        End Set
    End Property

    '<CsvColumn(Name:="TotalHighFuelScore", FieldIndex:=14)> _
    '<CsvColumn(FieldIndex:=14)>
    Public Property TotalHighFuelScore() As Long
        Get
            Return m_TotalHighFuelScore
        End Get
        Set(value As Long)
            m_TotalHighFuelScore = value
        End Set
    End Property



    '<CsvColumn(Name:="ScoreLowFuelT", FieldIndex:=15)> _
    '<CsvColumn(FieldIndex:=15)>
    Public Property ScoreLowFuelT() As Boolean
        Get
            Return m_ScoreLowFuelT
        End Get
        Set(value As Boolean)
            m_ScoreLowFuelT = value
        End Set
    End Property


    '<CsvColumn(Name:="Climb", FieldIndex:=16)> _
    '<CsvColumn(FieldIndex:=16)>
    Public Property Climb() As Boolean
        Get
            Return m_Climb
        End Get
        Set(value As Boolean)
            m_Climb = value
        End Set
    End Property


    '<CsvColumn(Name:="Climblocation", FieldIndex:=17)> _
    '<CsvColumn(FieldIndex:=17)>
    Public Property ClimbLocation() As String
        Get
            Return m_ClimbLocation
        End Get
        Set(value As String)
            m_ClimbLocation = value
        End Set
    End Property


    '<CsvColumn(Name:="DropGears", FieldIndex:=18)> _
    '<CsvColumn(FieldIndex:=18)>
    Public Property DropGears() As Long
        Get
            Return m_DropGears
        End Get
        Set(value As Long)
            m_DropGears = value
        End Set
    End Property



    '<CsvColumn(Name:="TechDiff", FieldIndex:=19)> _
    '<CsvColumn(FieldIndex:=19)>
    Public Property TechDiff() As String
        Get
            Return m_TechDiff
        End Get
        Set(value As String)
            m_TechDiff = value
        End Set
    End Property


End Class

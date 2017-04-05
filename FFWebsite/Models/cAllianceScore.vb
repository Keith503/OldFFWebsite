'   Class Name:      cAllianceScore
'   Author:          Keith Moore     
'   Date:            February 2017 
'   Description:     The object creates a data object to hold the alliance scores  
'   Change history:

Public Class cAllianceScore
    'Private Data Definitions 
    Private m_MatchNumber As Long
    Private m_AllianceTeams As String
    Private m_AutoFuelLow As Integer
    Private m_AutoFuelHigh As Integer
    Private m_AutoRotor As Integer
    Private m_AutoPoints As Integer
    Private m_ScoutBreachLineA As Boolean
    Private m_ScoutScoreGearA As Boolean
    Private m_ScoutGearLocationA As String
    Private m_ScoutScoreHighA As Long
    Private m_ScoutScoreLowA As Boolean
    Private m_ScoutAutonHighFuelScore As Integer
    Private m_TeleopPoints As Integer
    Private m_TeleopFuelPoints As Integer
    Private m_TeleopTakeOffPoints As Integer
    Private m_FoulPoints As Integer
    Private m_ScoutGearT As Integer
    Private m_ScoutScoreHighT As Integer
    Private m_TotalHighFuelScore As Integer
    Private m_ScoutClimb As Boolean
    Private m_ScoutDropGears As Integer
    Private m_ScoutTechDiff As String
    Private m_ScoutTotalHighFuel As Integer
    Private m_AutoRotorPoints As Integer
    Private m_AutoMobilityPoints As Integer
    Private m_Adjustpoints As Integer
    Private m_TotalPoints As Integer
    Private m_TotalRotorPoints As Integer
    Private m_ScoutClimbLocation As String

    Public Property MatchNumber() As Long
        Get
            Return m_MatchNumber
        End Get
        Set(ByVal value As Long)
            m_MatchNumber = value
        End Set
    End Property
    Public Property AllianceTeams() As String
        Get
            Return m_Allianceteams
        End Get
        Set(ByVal value As String)
            m_AllianceTeams = value
        End Set
    End Property
    Public Property AutoFuelLow() As Integer
        Get
            Return m_AutoFuelLow
        End Get
        Set(ByVal value As Integer)
            m_AutoFuelLow = value
        End Set
    End Property

    Public Property AutoFuelHigh() As Integer
        Get
            Return m_AutoFuelHigh
        End Get
        Set(ByVal value As Integer)
            m_AutoFuelHigh = value
        End Set
    End Property

    Public Property AutoRotor() As Integer
        Get
            Return m_AutoRotor
        End Get
        Set(ByVal value As Integer)
            m_AutoRotor = value
        End Set
    End Property

    Public Property AutoPoints() As Integer
        Get
            Return m_AutoPoints
        End Get
        Set(ByVal value As Integer)
            m_AutoPoints = value
        End Set
    End Property

    Public Property ScoutBreachlineA() As Boolean
        Get
            Return m_ScoutBreachLineA
        End Get
        Set(ByVal value As Boolean)
            m_ScoutBreachLineA = value
        End Set
    End Property

    Public Property ScoutScoreGearA() As Boolean
        Get
            Return m_ScoutScoreGearA
        End Get
        Set(ByVal value As Boolean)
            m_ScoutScoreGearA = value
        End Set
    End Property

    Public Property ScoutGearLocationA() As String
        Get
            Return m_ScoutGearLocationA
        End Get
        Set(ByVal value As String)
            m_ScoutGearLocationA = value
        End Set
    End Property

    Public Property ScoutClimbLocation() As String
        Get
            Return m_ScoutClimbLocation
        End Get
        Set(ByVal value As String)
            m_ScoutClimbLocation = value
        End Set
    End Property

    Public Property ScoutScoreHighA() As Long
        Get
            Return m_ScoutScoreHighA
        End Get
        Set(ByVal value As Long)
            m_ScoutScoreHighA = value
        End Set
    End Property

    Public Property ScoutScoreLowA() As Boolean
        Get
            Return m_ScoutScoreLowA
        End Get
        Set(ByVal value As Boolean)
            m_ScoutScoreLowA = value
        End Set
    End Property

    Public Property ScoutAutonHighFuelScore() As Integer
        Get
            Return m_ScoutAutonHighFuelScore
        End Get
        Set(ByVal value As Integer)
            m_ScoutAutonHighFuelScore = value
        End Set
    End Property

    Public Property TeleopPoints() As Long
        Get
            Return m_TeleopPoints
        End Get
        Set(ByVal value As Long)
            m_TeleopPoints = value
        End Set
    End Property

    Public Property TeleopFuelPoints() As Long
        Get
            Return m_TeleopFuelPoints
        End Get
        Set(ByVal value As Long)
            m_TeleopFuelPoints = value
        End Set
    End Property

    Public Property TeleopTakeOffPoints() As Long
        Get
            Return m_TeleopTakeOffPoints
        End Get
        Set(ByVal value As Long)
            m_TeleopTakeOffPoints = value
        End Set
    End Property

    Public Property FoulPoints() As Long
        Get
            Return m_FoulPoints
        End Get
        Set(ByVal value As Long)
            m_FoulPoints = value
        End Set
    End Property

    Public Property ScoutGearT() As Long
        Get
            Return m_ScoutGearT
        End Get
        Set(ByVal value As Long)
            m_ScoutGearT = value
        End Set
    End Property

    Public Property ScoutScoreHighT() As Long
        Get
            Return m_ScoutScoreHighT
        End Get
        Set(ByVal value As Long)
            m_ScoutScoreHighT = value
        End Set
    End Property

    Public Property TotalHighFuelScore() As Long
        Get
            Return m_TotalHighFuelScore
        End Get
        Set(ByVal value As Long)
            m_TotalHighFuelScore = value
        End Set
    End Property

    Public Property ScoutClimb() As Boolean
        Get
            Return m_ScoutClimb
        End Get
        Set(ByVal value As Boolean)
            m_ScoutClimb = value
        End Set
    End Property

    Public Property ScoutDropGears() As Long
        Get
            Return m_ScoutDropGears
        End Get
        Set(ByVal value As Long)
            m_ScoutDropGears = value
        End Set
    End Property

    Public Property ScoutTechDiff() As String
        Get
            Return m_ScoutTechDiff
        End Get
        Set(ByVal value As String)
            m_ScoutTechDiff = value
        End Set
    End Property


    Public Property ScoutTotalHighFuel() As Long
        Get
            Return m_ScoutTotalHighFuel
        End Get
        Set(ByVal value As Long)
            m_ScoutTotalHighFuel = value
        End Set
    End Property


    Public Property AutoRotorPoints() As Long
        Get
            Return m_AutoRotorPoints
        End Get
        Set(ByVal value As Long)
            m_AutoRotorPoints = value
        End Set
    End Property


    Public Property AutoMobilityPoints() As Long
        Get
            Return m_AutoMobilityPoints
        End Get
        Set(ByVal value As Long)
            m_AutoMobilityPoints = value
        End Set
    End Property


    Public Property Adjustpoints() As Long
        Get
            Return m_Adjustpoints
        End Get
        Set(ByVal value As Long)
            m_Adjustpoints = value
        End Set
    End Property


    Public Property TotalPoints() As Long
        Get
            Return m_TotalPoints
        End Get
        Set(ByVal value As Long)
            m_TotalPoints = value
        End Set
    End Property

    Public Property TotalRotorPoints() As Long
        Get
            Return m_TotalRotorPoints
        End Get
        Set(ByVal value As Long)
            m_TotalRotorPoints = value
        End Set
    End Property
End Class

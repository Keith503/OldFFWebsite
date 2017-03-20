'   Class Name:      cMatchItem
'   Author:          Keith Moore     
'   Date:            March 2017  
'   Description:     The object contains the Match Item Data Elements 
'   Change history:

Public Class cMatchItem
    'Private Data Definitions 
    Private m_ID As Long
    Private m_EventID As Long
    Private m_MatchNumber As Long
    Private m_Description As String
    Private m_StartTime As Date
    Private m_TournamentLevel As String
    Private m_Station As String

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
    Public Property MatchNumber() As Long
        Get
            Return m_MatchNumber
        End Get
        Set(ByVal value As Long)
            m_MatchNumber = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return m_Description
        End Get
        Set(ByVal value As String)
            m_Description = value
        End Set
    End Property

    Public Property StartTime() As Date
        Get
            Return m_StartTime
        End Get
        Set(ByVal value As Date)
            m_StartTime = value
        End Set
    End Property

    Public Property TournamentLevel As String
        Get
            Return m_TournamentLevel
        End Get
        Set(ByVal value As String)
            m_TournamentLevel = value
        End Set
    End Property

    Public Property Station As String
        Get
            Return m_Station
        End Get
        Set(ByVal value As String)
            m_Station = value
        End Set
    End Property

End Class

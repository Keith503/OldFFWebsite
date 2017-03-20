'   Class Name:      cMatchTeams
'   Author:          Keith Moore     
'   Date:            February 2017 
'   Description:     The object creates a general object be used to type Data Elements 
'   Change history:

Public Class cMatchTeams
    'Private Data Definitions 
    Private m_ScheduleID As Long
    Private m_TeamNumber As Long
    Private m_TeamName As String
    Private m_Station As String


    Public Property ScheduleID() As Long
        Get
            Return m_ScheduleID
        End Get
        Set(ByVal value As Long)
            m_ScheduleID = value
        End Set
    End Property

    Public Property TeamNumber() As Long
        Get
            Return m_TeamNumber
        End Get
        Set(ByVal value As Long)
            m_TeamNumber = value
        End Set
    End Property

    Public Property TeamName() As String
        Get
            Return m_TeamName
        End Get
        Set(ByVal value As String)
            m_TeamName = value
        End Set
    End Property

    Public Property Station() As String
        Get
            Return m_Station
        End Get
        Set(ByVal value As String)
            m_Station = value
        End Set
    End Property

End Class

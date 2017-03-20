'   Class Name:      cGearStats
'   Author:          Keith Moore     
'   Date:            February 2017 
'   Description:     The object creates a general object be used to type Data Elements 
'   Change history:

Public Class cGearStats
    'Private Data Definitions 
    Private m_Team As String
    Private m_AutonGears As Integer
    Private m_TeleopGears As Integer
    Private m_TotalGears As Integer

    Public Property Team() As String
        Get
            Return m_Team
        End Get
        Set(ByVal value As String)
            m_Team = value
        End Set
    End Property

    Public Property AutonGears() As Integer
        Get
            Return m_AutonGears
        End Get
        Set(ByVal value As Integer)
            m_AutonGears = value
        End Set
    End Property

    Public Property TeleOpGears() As Integer
        Get
            Return m_TeleopGears
        End Get
        Set(ByVal value As Integer)
            m_TeleopGears = value
        End Set
    End Property

    Public Property TotalGears() As Integer
        Get
            Return m_TotalGears
        End Get
        Set(ByVal value As Integer)
            m_TotalGears = value
        End Set
    End Property

End Class

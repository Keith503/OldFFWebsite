'   Class Name:      cClimbStats
'   Author:          Keith Moore     
'   Date:            February 2017 
'   Description:     The object creates a general object be used to type Data Elements 
'   Change history:

Public Class cClimbStats
    'Private Data Definitions 
    Private m_Team As String
    Private m_TotalClimb As Integer

    Public Property Team() As String
        Get
            Return m_Team
        End Get
        Set(ByVal value As String)
            m_Team = value
        End Set
    End Property

    Public Property TotalClimb() As Integer
        Get
            Return m_TotalClimb
        End Get
        Set(ByVal value As Integer)
            m_TotalClimb = value
        End Set
    End Property

End Class

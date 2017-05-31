'   Class Name:      cTeam  
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Team Database 
'   Change history

Public Class cTeam
    Private m_TeamID As Long
    Private m_TeamName As String
    Public Property TeamID() As Long
        Get
            Return m_TeamID
        End Get
        Set(ByVal value As Long)
            m_TeamID = value
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


End Class
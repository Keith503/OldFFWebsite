'   Class Name:      cFTCKickoffRegister
'   Author:          Keith Moore     
'   Date:            May 2017  
'   Description:     The object provides data services for the FTC Kickoff Database 
'   Change history

Public Class cFTCKickoffRegister
    Private m_ID As Long   'Team Number is ID 
    Private m_TeamName As String
    Private m_Experience As String
    Private m_School As String
    Private m_TeamContactName As String
    Private m_TeamContactEmail As String
    Private m_TeamContactPhone As String
    Private m_MentorCount As Integer
    Private m_StudentCount As Integer


    Public Property ID() As Long
        Get
            Return m_ID
        End Get
        Set(ByVal value As Long)
            m_ID = value
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
    Public Property Experience() As String
        Get
            Return m_Experience
        End Get
        Set(ByVal value As String)
            m_Experience = value
        End Set
    End Property
    Public Property School() As String
        Get
            Return m_School
        End Get
        Set(ByVal value As String)
            m_School = value
        End Set
    End Property
    Public Property TeamContactName() As String
        Get
            Return m_TeamContactName
        End Get
        Set(ByVal value As String)
            m_TeamContactName = value
        End Set
    End Property
    Public Property TeamContactEmail() As String
        Get
            Return m_TeamContactEmail
        End Get
        Set(ByVal value As String)
            m_TeamContactEmail = value
        End Set
    End Property
    Public Property TeamContactPhone() As String
        Get
            Return m_TeamContactPhone
        End Get
        Set(ByVal value As String)
            m_TeamContactPhone = value
        End Set
    End Property
    Public Property MentorCount() As Integer
        Get
            Return m_MentorCount
        End Get
        Set(ByVal value As Integer)
            m_MentorCount = value
        End Set
    End Property
    Public Property StudentCount() As Integer
        Get
            Return m_StudentCount
        End Get
        Set(ByVal value As Integer)
            m_StudentCount = value
        End Set
    End Property

End Class
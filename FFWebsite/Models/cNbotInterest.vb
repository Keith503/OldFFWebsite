'   Class Name:      cNBotInterest
'   Author:          Keith Moore     
'   Date:            May 2017  
'   Description:     The object provides data services for the Nbot Database 
'   Change history

Public Class cNbotInterest
    Private m_ID As Long
    Private m_StudentFirstName As String
    Private m_StudentLastName As String
    Private m_StudentPhone As String
    Private m_StudenteMail As String
    Private m_SchoolID As Integer
    Private m_Grade As Integer
    Private m_Gender As String
    Private m_Parent1Name As String
    Private m_Parent1eMail As String
    Private m_Parent1Phone As String
    Private m_Parent2Name As String
    Private m_Parent2eMail As String
    Private m_FirstProgram As Integer
    Private m_Question1 As String
    Private m_Question2 As String
    Private m_Question3 As String
    Private m_PriorExperience As String

    Public Property ID() As Long
        Get
            Return m_ID
        End Get
        Set(ByVal value As Long)
            m_ID = value
        End Set
    End Property

    Public Property StudentFirstName() As String
        Get
            Return m_StudentFirstName
        End Get
        Set(ByVal value As String)
            m_StudentFirstName = value
        End Set
    End Property
    Public Property StudentLastName() As String
        Get
            Return m_StudentLastName
        End Get
        Set(ByVal value As String)
            m_StudentLastName = value
        End Set
    End Property
    Public Property StudenteMail() As String
        Get
            Return m_StudenteMail
        End Get
        Set(ByVal value As String)
            m_StudenteMail = value
        End Set
    End Property
    Public Property StudentPhone() As String
        Get
            Return m_StudentPhone
        End Get
        Set(ByVal value As String)
            m_StudentPhone = value
        End Set
    End Property
    Public Property SchoolID() As Integer
        Get
            Return m_SchoolID
        End Get
        Set(ByVal value As Integer)
            m_SchoolID = value
        End Set
    End Property
    Public Property Grade() As Integer
        Get
            Return m_Grade
        End Get
        Set(ByVal value As Integer)
            m_Grade = value
        End Set
    End Property
    Public Property Gender() As String
        Get
            Return m_Gender
        End Get
        Set(ByVal value As String)
            m_Gender = value
        End Set
    End Property

    Public Property Parent1Name() As String
        Get
            Return m_Parent1Name
        End Get
        Set(ByVal value As String)
            m_Parent1Name = value
        End Set
    End Property
    Public Property Parent1eMail() As String
        Get
            Return m_Parent1eMail
        End Get
        Set(ByVal value As String)
            m_Parent1eMail = value
        End Set
    End Property
    Public Property Parent1Phone() As String
        Get
            Return m_Parent1Phone
        End Get
        Set(ByVal value As String)
            m_Parent1Phone = value
        End Set
    End Property
    Public Property Parent2Name() As String
        Get
            Return m_Parent2Name
        End Get
        Set(ByVal value As String)
            m_Parent2Name = value
        End Set
    End Property
    Public Property Parent2eMail() As String
        Get
            Return m_Parent2eMail
        End Get
        Set(ByVal value As String)
            m_Parent2eMail = value
        End Set
    End Property
    Public Property FirstProgram() As Integer
        Get
            Return m_FirstProgram
        End Get
        Set(ByVal value As Integer)
            m_FirstProgram = value
        End Set
    End Property
    Public Property Question1() As String
        Get
            Return m_Question1
        End Get
        Set(ByVal value As String)
            m_Question1 = value
        End Set
    End Property
    Public Property Question2() As String
        Get
            Return m_Question2
        End Get
        Set(ByVal value As String)
            m_Question2 = value
        End Set
    End Property
    Public Property Question3() As String
        Get
            Return m_Question3
        End Get
        Set(ByVal value As String)
            m_Question3 = value
        End Set
    End Property
    Public Property PriorExperience() As String
        Get
            Return m_PriorExperience
        End Get
        Set(ByVal value As String)
            m_PriorExperience = value
        End Set
    End Property
End Class
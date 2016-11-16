'   Class Name:      cEventItem
'   Author:          Keith Moore     
'   Date:            November 2016 
'   Description:     The object contains the Event Item Data Elements 
'   Change history:

Public Class cEventItem
    'Private Data Definitions 
    Private m_ID As Long
    Private m_Start_date As Date
    Private m_End_date As Date
    Private m_Type_ID As Long
    Private m_Status_ID As Long
    Private m_Student_Lead_ID As Long
    Private m_Student_Lead_Name As String
    Private m_Mentor_ID As Long
    Private m_Mentor_name As String
    Private m_ApprovedBy_ID As Long
    Private m_Approved_Date As Date
    Private m_Transport_flag As Long
    Private m_Location_text As String
    Private m_Title_text As String
    Private m_Body_text As String
    Private m_Transportation_text As String


    Public Property ID() As Long
        Get
            Return m_ID
        End Get
        Set(ByVal value As Long)
            m_ID = value
        End Set
    End Property

    Public Property Start_Date() As Date
        Get
            Return m_Start_date
        End Get
        Set(ByVal value As Date)
            m_Start_date = value
        End Set
    End Property

    Public Property End_Date() As Date
        Get
            Return m_End_date
        End Get
        Set(ByVal value As Date)
            m_End_date = value
        End Set
    End Property

    Public Property Type_ID() As Long
        Get
            Return m_Type_ID
        End Get
        Set(ByVal value As Long)
            m_Type_ID = value
        End Set
    End Property

    Public Property Status_ID() As Long
        Get
            Return m_Status_ID
        End Get
        Set(ByVal value As Long)
            m_Status_ID = value
        End Set
    End Property

    Public Property Student_Lead_ID() As Long
        Get
            Return m_Student_Lead_ID
        End Get
        Set(ByVal value As Long)
            m_Student_Lead_ID = value
        End Set
    End Property

    Public Property Student_Lead_Name() As String
        Get
            Return m_Student_Lead_Name
        End Get
        Set(ByVal value As String)
            m_Student_Lead_Name = value
        End Set
    End Property

    Public Property Mentor_ID() As Long
        Get
            Return m_Mentor_ID
        End Get
        Set(ByVal value As Long)
            m_Mentor_ID = value
        End Set
    End Property

    Public Property Mentor_Name() As String
        Get
            Return m_Mentor_name
        End Get
        Set(ByVal value As String)
            m_Mentor_name = value
        End Set
    End Property
    Public Property ApprovedBy_ID() As Long
        Get
            Return m_ApprovedBy_ID
        End Get
        Set(ByVal value As Long)
            m_ApprovedBy_ID = value
        End Set
    End Property

    Public Property Approved_Date() As Date
        Get
            Return m_Approved_Date
        End Get
        Set(ByVal value As Date)
            m_Approved_Date = value
        End Set
    End Property

    Public Property Transport_Flag() As Long
        Get
            Return m_Transport_flag
        End Get
        Set(ByVal value As Long)
            m_Transport_flag = value
        End Set
    End Property
    Public Property Location_text() As String
        Get
            Return m_Location_text
        End Get
        Set(ByVal value As String)
            m_Location_text = value
        End Set
    End Property

    Public Property Title_text() As String
        Get
            Return m_Title_text
        End Get
        Set(ByVal value As String)
            m_Title_text = value
        End Set
    End Property

    Public Property Body_text() As String
        Get
            Return m_Body_text
        End Get
        Set(ByVal value As String)
            m_Body_text = value
        End Set
    End Property

    Public Property Transportation_text() As String
        Get
            Return m_Transportation_text
        End Get
        Set(ByVal value As String)
            m_Transportation_text = value
        End Set
    End Property
End Class

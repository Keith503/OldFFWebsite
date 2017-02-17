'   Class Name:      cTypeItem
'   Author:          Keith Moore     
'   Date:            February 2017 
'   Description:     The object creates a general object be used to type Data Elements 
'   Change history:

Public Class cTypeItem
    'Private Data Definitions 
    Private m_ID As Long
    Private m_Description_text As String


    Public Property ID() As Long
        Get
            Return m_ID
        End Get
        Set(ByVal value As Long)
            m_ID = value
        End Set
    End Property

    Public Property Description_text() As String
        Get
            Return m_Description_text
        End Get
        Set(ByVal value As String)
            m_Description_text = value
        End Set
    End Property

End Class

'   Class Name:      cKit_Types
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Kit Database 
'   Change history

Public Class cKit_Types
    Private m_KitID As Long
    Private m_Description As String
    Public Property KitID() As Long
        Get
            Return m_KitID
        End Get
        Set(ByVal value As Long)
            m_KitID = value
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


End Class
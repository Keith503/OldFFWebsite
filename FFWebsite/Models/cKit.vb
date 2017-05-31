'   Class Name:      cKit
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Kit Database 
'   Change history

Public Class cKit
    Private m_KitID As Long
    Private m_PartID As Long
    Private m_Qty As Long
    Private m_Desc As String
    Public Property KitID() As Long
        Get
            Return m_KitID
        End Get
        Set(ByVal value As Long)
            m_KitID = value
        End Set
    End Property

    Public Property PartID() As Long
        Get
            Return m_PartID
        End Get
        Set(ByVal value As Long)
            m_PartID = value
        End Set
    End Property
    Public Property Quantity() As Long
        Get
            Return m_Qty
        End Get
        Set(ByVal value As Long)
            m_Qty = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return m_Desc
        End Get
        Set(ByVal value As String)
            m_Desc = value
        End Set
    End Property


End Class
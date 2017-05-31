'   Class Name:      cInventory 
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Inventory Database 
'   Change history

Public Class cInventory
    Private m_PartID As Long
    Private m_TeamID As Long
    Private m_Quantity As Integer
    Private m_Description As String
    Private m_Quantity_Distributed As Long
    Public Property PartID() As Long
        Get
            Return m_PartID
        End Get
        Set(ByVal value As Long)
            m_PartID = value
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

    Public Property Quantity() As Long
        Get
            Return m_Quantity
        End Get
        Set(ByVal value As Long)
            m_Quantity = value
        End Set
    End Property
    Public Property Quantity_Distributed() As Long
        Get
            Return m_Quantity_Distributed
        End Get
        Set(ByVal value As Long)
            m_Quantity_Distributed = value
        End Set
    End Property

    Public Property TeamID() As Long
        Get
            Return m_TeamID
        End Get
        Set(ByVal value As Long)
            m_TeamID = value
        End Set
    End Property
End Class

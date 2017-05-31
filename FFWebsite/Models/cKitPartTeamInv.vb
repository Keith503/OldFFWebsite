'   Class Name:      cKitPartTeamInv 
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Kit, Part, Team Inventoy Database 
'   Change history

Public Class cKitPartTeamInv
    Private m_KitName As String
    Private m_PartID As Long
    Private m_PartName As String
    Private m_KitQuantity As Integer
    Private m_TeamQuantity As Integer
    Private m_Difference As Integer

    Public Property KitName() As String
        Get
            Return m_KitName
        End Get
        Set(ByVal value As String)
            m_KitName = value
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
    Public Property PartName() As String
        Get
            Return m_PartName
        End Get
        Set(ByVal value As String)
            m_PartName = value
        End Set
    End Property
    Public Property KitQuantity() As Integer
        Get
            Return m_KitQuantity
        End Get
        Set(ByVal value As Integer)
            m_KitQuantity = value
        End Set
    End Property
    Public Property TeamQuantity() As Integer
        Get
            Return m_TeamQuantity
        End Get
        Set(ByVal value As Integer)
            m_TeamQuantity = value
        End Set
    End Property
    Public Property Difference() As Integer
        Get
            Return m_Difference
        End Get
        Set(ByVal value As Integer)
            m_Difference = value
        End Set
    End Property



End Class
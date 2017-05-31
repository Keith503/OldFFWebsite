'   Class Name:      cTeam_Inventory 
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Team Inventory Database 
'   Change history
Imports System.Data.OleDb
Public Class cTeam_Inventory
    Private m_PartID As Long
    Private m_Quantity As Integer
    Private m_Description As String
    Private m_Returned As Integer

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
    Public Property Returned() As Long
        Get
            Return m_Returned
        End Get
        Set(ByVal value As Long)
            m_Returned = value
        End Set
    End Property


End Class

'   Class Name:      cPart_Returned 
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the FTCKOP Database 
'   Change history
Imports System.Data.OleDb
Public Class cPart_Returned
    Private m_TeamID As Long
    Private m_PartID As Long
    Private m_Returned As Integer

    Public Property TeamID() As Long
        Get
            Return m_TeamID
        End Get
        Set(ByVal value As Long)
            m_TeamID = value
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

    Public Property Returned() As Long
        Get
            Return m_Returned
        End Get
        Set(ByVal value As Long)
            m_Returned = value
        End Set
    End Property


End Class

'   Class Name:      cPart 
'   Author:          Keith Moore     
'   Date:            May 2016  
'   Description:     The object provides data services for the Parts Database 
'   Change history
Public Class cPart
    Private m_PartID As Long
    Private m_Description As String
    Private m_UnitCost As Double
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

    Public Property UnitCost() As Double
        Get
            Return m_UnitCost
        End Get
        Set(ByVal value As Double)
            m_UnitCost = value
        End Set
    End Property

End Class

Public Class Product
    Private m_ID As Integer
    Private m_Name As String
    Private m_Category As String
    Private m_Price As Decimal

    Public Property ID() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return m_Name
        End Get
        Set(ByVal value As String)
            m_Name = value
        End Set
    End Property

    Public Property Category() As String
        Get
            Return m_Category
        End Get
        Set(ByVal value As String)
            m_Category = value
        End Set
    End Property
    Public Property Price() As Decimal
        Get
            Return m_Price
        End Get
        Set(ByVal value As Decimal)
            m_Price = value
        End Set
    End Property
End Class

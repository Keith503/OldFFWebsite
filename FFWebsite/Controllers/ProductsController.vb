Imports System.Net
Imports System.Web.Http


Namespace Controllers
    Public Class ProductsController
        Inherits ApiController


        Public Function GetAllProducts() As IHttpActionResult
            Dim Products As New List(Of Product)
            Dim ProductRow As New Product
            With ProductRow
                .ID = 1
                .Name = "tomato Soup"
                .Category = "Groceries"
                .Price = 1
            End With

            Products.Add(ProductRow)
            Dim ProductRow2 As New Product
            With ProductRow2
                .ID = 2
                .Name = "Yo Yo"
                .Category = "Toys"
                .Price = 1
            End With
            Products.Add(ProductRow2)

            Return Ok(Products)
        End Function

        Public Function GetProductbyID(id As Integer) As IHttpActionResult
            Dim Products As New List(Of Product)
            Dim ProductRow As New Product
            With ProductRow
                .ID = 1
                .Name = "tomato Soup"
                .Category = "Groceries"
                .Price = 1
            End With

            Products.Add(ProductRow)

            Return Ok(Products)
        End Function
    End Class
End Namespace
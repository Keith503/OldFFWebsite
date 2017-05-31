Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json
Imports iText.Kernel.Pdf
Imports iText.Kernel.Colors.Color
Imports iText.Layout
Imports iText.Layout.Element
Imports System.IO
Imports System.Net.Http.Headers
Imports System.Net.Http
Imports System.Web



Namespace Controllers
    Public Class FTCKOPController
        Inherits ApiController

        Public Function GetTeamList() As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim TeamList As New List(Of cTeam)

            Try
                TeamList = m_cFTCKoPServer.GetTeamList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTeamList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(TeamList)
        End Function

        Public Function GetTeamInventoryList(ByVal id As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim TeamInventoryList As New List(Of cTeam_Inventory)

            Try
                TeamInventoryList = m_cFTCKoPServer.GetTeamInventoryList(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTeamList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(TeamInventoryList)
        End Function

        Public Function GetInventoryList() As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim InventoryList As New List(Of cInventory)

            Try
                InventoryList = m_cFTCKoPServer.GetInventoryList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetInventoryList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(InventoryList)
        End Function

        Public Function GetPartList() As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim PartList As New List(Of cPart)

            Try
                PartList = m_cFTCKoPServer.GetPartList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetPartListList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(PartList)
        End Function

        Public Function GetPart(ByVal id As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim Part As New cPart

            Try
                Part = m_cFTCKoPServer.GetPart(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetPart", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(Part)
        End Function
        Public Function UpdatePart(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cPart
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cPart)(value)
                m_cFTCKoPServer.UpdatePart(obj.PartID, obj.Description, obj.UnitCost)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdatePart", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function RemovePart(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cPart
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cPart)(value)
                m_cFTCKoPServer.DeletePart(obj.PartID)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("RemovePart", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function GetTeam(ByVal id As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim Team As New cTeam

            Try
                Team = m_cFTCKoPServer.GetTeam(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetTeam", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(Team)
        End Function
        Public Function UpdateTeam(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cTeam
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cTeam)(value)
                m_cFTCKoPServer.UpdateTeam(obj.TeamID, obj.TeamName)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdateTeam", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function RemoveTeam(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cTeam
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cTeam)(value)
                m_cFTCKoPServer.DeleteTeam(obj.TeamID)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("RemoveTeam", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function
        Public Function GetKitList() As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim KitList As New List(Of cKit_Types)

            Try
                KitList = m_cFTCKoPServer.GetKitList()
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetKitList", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(KitList)
        End Function
        Public Function GetKit(ByVal id As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim Kit As New cKit_Types

            Try
                Kit = m_cFTCKoPServer.GetKit(CLng(id))
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetKit", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(Kit)
        End Function
        Public Function UpdateKit(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cKit_Types
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cKit_Types)(value)
                m_cFTCKoPServer.UpdateKit(obj.KitID, obj.Description)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdateKit", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function
        Public Function RemoveKit(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cKit_Types
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cKit_Types)(value)
                m_cFTCKoPServer.DeleteKit(obj.KitID)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("RemoveKit", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function
        Public Function UpdateInventory(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cInventory
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Try
                obj = JsonConvert.DeserializeObject(Of cInventory)(value)
                m_cFTCKoPServer.UpdateInventory(obj.PartID, obj.Quantity, obj.Quantity_Distributed)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdateInventory", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function UpdateKitInventory(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cKit
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim KitList As IList(Of cKit)
            Dim lRet As Long

            Try
                obj = JsonConvert.DeserializeObject(Of cKit)(value)
                'go get list of parts in this kit 
                KitList = m_cFTCKoPServer.GetPartsInKit(obj.KitID)
                'now loop through each part and add to inventory 
                For Each ckit In KitList
                    lRet = m_cFTCKoPServer.UpdateInventory(ckit.PartID, ckit.Quantity * obj.Quantity, 0)
                Next
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("UpdateKitInventory", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function ProcessPartAllocation(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cInventory
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim InventoryRow As cInventory
            Dim lRet As Long

            Try
                obj = JsonConvert.DeserializeObject(Of cInventory)(value)

                'first check part quantity 
                InventoryRow = m_cFTCKoPServer.GetInventorybyPart(obj.PartID)
                If obj.Quantity > (InventoryRow.Quantity - InventoryRow.Quantity_Distributed) Then
                    Dim strErr As String = BuildErrorMsg("ProcessPartAllocation", "Insufficient Quantity in Inventory to complete allocation-try again!")
                    Throw New Exception(strErr)
                End If

                'Quantity good - process allocation 
                'go update the team inventory 
                lRet = m_cFTCKoPServer.UpdateTeamInventory(obj.TeamID, obj.PartID, obj.Quantity)
                'now go decrement the inventory on hand 
                InventoryRow = m_cFTCKoPServer.GetInventorybyPart(obj.PartID)
                m_cFTCKoPServer.UpdateInventory(InventoryRow.PartID, obj.Quantity * -1, 0)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("ProcessPartAllocation", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function ProcessKitAllocation(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim obj As cInventory
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim InventoryRow As cInventory
            Dim lRet As Long
            Dim lKitID As Long
            Dim KitList As List(Of cKit)
            Dim lNewQuantity As Long
            Dim bQuantityOK As Boolean

            Try
                obj = JsonConvert.DeserializeObject(Of cInventory)(value)
                lKitID = obj.PartID     'borrowed the part id field to sotr kit in it- pull it back out l

                'first check part quantity
                KitList = m_cFTCKoPServer.GetPartsInKit(lKitID)

                For Each cKit In KitList
                    InventoryRow = m_cFTCKoPServer.GetInventorybyPart(cKit.PartID)
                    'calculate quantity needed 
                    lNewQuantity = obj.Quantity * cKit.Quantity
                    If lNewQuantity <= (InventoryRow.Quantity - InventoryRow.Quantity_Distributed) Then
                        bQuantityOK = True
                    Else
                        bQuantityOK = False
                        Exit For
                    End If
                Next

                If bQuantityOK = False Then
                    Dim strErr As String = BuildErrorMsg("ProcessKitAllocation", "Insufficient Quantity in Inventory to complete allocation-try again!")
                    Throw New Exception(strErr)
                End If

                'Quantity good - process allocation
                For Each cKit In KitList
                    'first go update the team inventory 
                    lRet = m_cFTCKoPServer.UpdateTeamInventory(obj.TeamID, cKit.PartID, cKit.Quantity * obj.Quantity)
                    lRet = m_cFTCKoPServer.UpdateInventory(cKit.PartID, cKit.Quantity * obj.Quantity * -1, 0)
                Next
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("ProcessKitAllocation", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function

        Public Function ProcessReturnCommit(<FromBody()> ByVal value As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim lRet As Long
            Dim TeamInvList As New List(Of cTeam_Inventory)
            Dim lTeamID As Long
            Dim TeamRow As cTeam_Inventory
            Dim obj As cTeam

            'ID is team ID 
            Try
                'get team inventory list
                obj = JsonConvert.DeserializeObject(Of cTeam)(value)
                lTeamID = obj.TeamID
                TeamInvList = m_cFTCKoPServer.GetTeamInventoryList(lTeamID)

                For Each TeamRow In TeamInvList
                    lRet = m_cFTCKoPServer.ResetReturned(lTeamID, TeamRow.PartID, TeamRow.Returned)
                Next
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("ProcessReturnCommit", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok()
        End Function




        Public Function GetKitCompare(ByVal id As String) As IHttpActionResult
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim CompareList As New List(Of cKitPartTeamInv)
            Dim lKitID As Long
            Dim lTeam As Long

            Try
                lKitID = CLng(id)
                lTeam = CLng(HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("team"))

                CompareList = m_cFTCKoPServer.GetKitPartTeamInvList(lTeam, lKitID)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetKit", ex.Message.ToString)
                Return Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox (ExpectationFailed Return Warning - Warning messagebox)
            End Try

            Return Ok(CompareList)
        End Function







        Public Function GetTeamInvReport(id As String) As IHttpActionResult
            Dim response As IHttpActionResult
            Dim lTeamID As Long
            Dim TeamInventoryList As New List(Of cTeam_Inventory)
            Dim TeamRow As cTeam_Inventory
            Dim Team As cTeam
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim bOddLine As Boolean
            Dim sTeamTitle As String

            'get the Team Number 
            lTeamID = CLng(id)
            'sYear = HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("year")
            Try
                'Go get inventory list
                TeamInventoryList = m_cFTCKoPServer.GetTeamInventoryList(CLng(id))
                Team = m_cFTCKoPServer.GetTeam(CLng(id))
                sTeamTitle = id + "-" + Team.TeamName


                Dim stream As MemoryStream = New MemoryStream()
                Dim writer = New PdfWriter(stream)
                Dim pdf = New PdfDocument(writer)
                Dim document = New Document(pdf)
                document.SetMargins(20, 20, 20, 20)

                Dim tableparms() As Single = {1, 3, 1, 1}
                Dim table = New Table(tableparms)
                table.SetWidthPercent(100)

                Dim strPath As String = HttpContext.Current.Request.PhysicalApplicationPath
                Dim titleFont As iText.Kernel.Font.PdfFont
                titleFont = iText.Kernel.Font.PdfFontFactory.CreateFont(strPath + "fonts\segoeui.ttf", Nothing, True)
                Dim baseFont As iText.Kernel.Font.PdfFont

                baseFont = iText.Kernel.Font.PdfFontFactory.CreateFont(strPath + "fonts\calibri.ttf", Nothing, True)

                'Create page headings
                table.AddHeaderCell(New Cell(1, 4).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                table.AddHeaderCell(New Cell(1, 4).Add("Team Inventory").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(titleFont).SetFontSize(20).SetFontColor(iText.Kernel.Colors.Color.BLACK).SetBold)
                table.AddHeaderCell(New Cell(1, 4).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(8).SetBold.SetUnderline)
                table.AddHeaderCell(New Cell(1, 4).Add(sTeamTitle).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(baseFont).SetFontSize(12).SetFontColor(iText.Kernel.Colors.Color.BLACK).SetBold.SetItalic)
                table.AddHeaderCell(New Cell(1, 4).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 2)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(8).SetBold.SetUnderline).SetHeight(72.0F)
                'blank row 
                table.AddHeaderCell(New Cell(1, 4).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))

                table.AddHeaderCell(New Cell().Add("Part ID").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Description").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Quantity").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Returned").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))

                'table.AddCell(New Cell(1, 6).Add("Cost Center Title").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell(1, 6).Add("Cat1-Cat2 Title").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10).SetBold.SetUnderline)

                bOddLine = True
                For Each TeamRow In TeamInventoryList
                    If bOddLine = True Then
                        bOddLine = False
                        '******* HERE IS HOW TO SET THE BACKGROUND COLOR OF A CELL/ROW - this could be used to do alternating row colors. 
                        table.AddCell(New Cell().Add(TeamRow.PartID).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Description).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Quantity).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Returned).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                    Else
                        bOddLine = True
                        table.AddCell(New Cell().Add(TeamRow.PartID).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Description).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Quantity).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(TeamRow.Returned).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))

                    End If
                Next

                'total line 
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)

                'table.AddCell(New Cell(1, 2).Add("Total").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("8,000").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("8,000").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("0").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("0.00%").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'blank row 
                'table.AddCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))

                document.Add(table)
                document.Close()
                m_cFTCKoPServer = Nothing

                'now bundle the stream up in a HTTP response message along with header information
                'to let the recieving client know what type of steam it is getting.
                Dim result As HttpResponseMessage = New HttpResponseMessage(HttpStatusCode.OK)
                result.Content = New ByteArrayContent(stream.GetBuffer())
                result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                result.Content.Headers.ContentDisposition.FileName = "budgetReport.pdf"
                result.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")
                response = ResponseMessage(result)
            Catch ex As Exception
                Dim errmsg As String = BuildErrorMsg("GetBudgetReport", ex.Message.ToString)
                response = Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox 
            End Try

            Return response
        End Function

        Public Function GetKitCompareReport(id As String) As IHttpActionResult
            Dim response As IHttpActionResult
            Dim lTeam As Long
            Dim CompareRow As cKitPartTeamInv
            Dim Team As cTeam
            Dim m_cFTCKoPServer As New cFTCKoPServer
            Dim bOddLine As Boolean
            Dim sTeamTitle As String
            Dim lKitID As Long
            Dim CompareList As New List(Of cKitPartTeamInv)

            Try
                lKitID = CLng(id)
                lTeam = CLng(HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("team"))
                CompareList = m_cFTCKoPServer.GetKitPartTeamInvList(lTeam, lKitID)
                Team = m_cFTCKoPServer.GetTeam(lTeam)
                sTeamTitle = lTeam.ToString + "-" + Team.TeamName

                Dim stream As MemoryStream = New MemoryStream()
                Dim writer = New PdfWriter(stream)
                Dim pdf = New PdfDocument(writer)

                Dim document = New Document(pdf)
                document.SetMargins(20, 20, 20, 20)

                Dim tableparms() As Single = {2, 1, 3, 1, 1, 1}
                Dim table = New Table(tableparms)
                table.SetWidthPercent(100)

                Dim strPath As String = HttpContext.Current.Request.PhysicalApplicationPath
                Dim titleFont As iText.Kernel.Font.PdfFont
                titleFont = iText.Kernel.Font.PdfFontFactory.CreateFont(strPath + "fonts\segoeui.ttf", Nothing, True)
                Dim baseFont As iText.Kernel.Font.PdfFont
                baseFont = iText.Kernel.Font.PdfFontFactory.CreateFont(strPath + "fonts\calibri.ttf", Nothing, True)

                'Create page headings
                table.AddHeaderCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                table.AddHeaderCell(New Cell(1, 6).Add("Team Inventory Compare to Kit Contents").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(titleFont).SetFontSize(20).SetFontColor(iText.Kernel.Colors.Color.BLACK).SetBold)
                table.AddHeaderCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(8).SetBold.SetUnderline)
                table.AddHeaderCell(New Cell(1, 6).Add(sTeamTitle).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(baseFont).SetFontSize(12).SetFontColor(iText.Kernel.Colors.Color.BLACK).SetBold.SetItalic)
                table.AddHeaderCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 2)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(8).SetBold.SetUnderline).SetHeight(72.0F)
                'blank row 
                table.AddHeaderCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))

                table.AddHeaderCell(New Cell().Add("Kit Name").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Part ID").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Part Description").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Kit Quantity").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Team Quantity").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))
                table.AddHeaderCell(New Cell().Add("Diff").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetFontColor(iText.Kernel.Colors.Color.BLUE))

                'table.AddCell(New Cell(1, 6).Add("Cost Center Title").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell(1, 6).Add("Cat1-Cat2 Title").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10).SetBold.SetUnderline)

                bOddLine = True
                For Each CompareRow In CompareList
                    If bOddLine = True Then
                        bOddLine = False
                        '******* HERE IS HOW TO SET THE BACKGROUND COLOR OF A CELL/ROW - this could be used to do alternating row colors. 
                        table.AddCell(New Cell().Add(CompareRow.KitName).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.PartID).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.PartName).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.KitQuantity).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.TeamQuantity).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.Difference).SetBackgroundColor(LIGHT_GRAY).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                    Else
                        bOddLine = True
                        table.AddCell(New Cell().Add(CompareRow.KitName).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.PartID).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.PartName).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.KitQuantity).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.TeamQuantity).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))
                        table.AddCell(New Cell().Add(CompareRow.Difference).SetBackgroundColor(WHITE).SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10))

                    End If
                Next

                'total line 
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)
                'table.AddCell(New Cell().Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetBorderBottom(New Borders.SolidBorder(iText.Kernel.Colors.Color.BLUE, 1)).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(11).SetBold.SetUnderline)

                'table.AddCell(New Cell(1, 2).Add("Total").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("8,000").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("8,000").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("0").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'table.AddCell(New Cell().Add("0.00%").SetBorder(iText.Layout.Borders.Border.NO_BORDER).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT).SetFont(baseFont).SetFontSize(10).SetBold)
                'blank row 
                'table.AddCell(New Cell(1, 6).Add("").SetBorder(iText.Layout.Borders.Border.NO_BORDER))

                document.Add(table)
                    document.Close()
                    m_cFTCKoPServer = Nothing

                    'now bundle the stream up in a HTTP response message along with header information
                    'to let the recieving client know what type of steam it is getting.
                    Dim result As HttpResponseMessage = New HttpResponseMessage(HttpStatusCode.OK)
                    result.Content = New ByteArrayContent(stream.GetBuffer())
                    result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    result.Content.Headers.ContentDisposition.FileName = "budgetReport.pdf"
                    result.Content.Headers.ContentType = New MediaTypeHeaderValue("application/pdf")
                    response = ResponseMessage(result)
                Catch ex As Exception
                    Dim errmsg As String = BuildErrorMsg("GetBudgetReport", ex.Message.ToString)
                response = Content(HttpStatusCode.InternalServerError, errmsg)     'Return Error - Danger msgbox 
            End Try

            Return response
        End Function


        Private Function BuildErrorMsg(ByVal strFunctionName As String, strThrownError As String) As String
            Dim strErrMsg As String
            Dim mControllerName As String = "api/FTCKOPController/"
            strErrMsg = mControllerName & strFunctionName & " failed! Return Code: " & strThrownError

            'logger.Error(strErrMsg)

            Return strErrMsg
        End Function
    End Class

End Namespace
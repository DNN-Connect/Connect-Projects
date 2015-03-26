Imports System.Drawing
Imports Connect.DNN.Modules.Projects.Common
Imports Syncfusion.Pdf
Imports Syncfusion.Pdf.Graphics

Namespace Documents
 Public Class ProjectDocument
  Inherits PdfDocument

  Public Property PageGraphics As PdfGraphics
  Public Property Page As PdfPage
  Public Property BlackPen As PdfPen
  Public Property GrayPen As PdfPen

  Public Property BrownBrush As PdfBrush

  Public Sub New(projectId As Integer)

   PageSettings.Size = PdfPageSize.A4
   PageSettings.Margins.All = 0.0F
   BlackPen = New PdfPen(PdfBrushes.Black, 1.0F)
   GrayPen = New PdfPen(PdfBrushes.Gray, 3.0F)
   BrownBrush = New PdfSolidBrush(New PdfColor(71, 41, 43))
   InitPage()

  End Sub

  Public Sub InitPage()

   Page = Pages.Add()
   PageGraphics = Page.Graphics

   'For y As Single = 10 To 300 Step 10
   ' PageGraphics.DrawLine(BlackPen, 0.0F, y.MillimetersToPoints(), 210.0F.MillimetersToPoints(), y.MillimetersToPoints())
   'Next
   'For x As Single = 10 To 200 Step 10
   ' PageGraphics.DrawLine(BlackPen, x.MillimetersToPoints(), 0.0F, x.MillimetersToPoints(), 297.0F.MillimetersToPoints())
   'Next

   PageGraphics.DrawRectangle(BrownBrush, 0.0F, 287.0F.MillimetersToPoints(), 210.0F.MillimetersToPoints(), 10.0F.MillimetersToPoints())

   Dim font As PdfFont = GetHelvetica(10, False)
   Dim te As New PdfTextElement("DNN Connect Association")
   te.StringFormat = New PdfStringFormat(PdfTextAlignment.Center)
   te.Font = font
   te.Brush = PdfBrushes.White
   te.Draw(Page, New RectangleF(10.0F.MillimetersToPoints(), (292.0F).MillimetersToPoints() - (font.MeasureString(te.Text).Height * 0.65F), 190.0F.MillimetersToPoints(), font.MeasureString(te.Text).Height))

  End Sub

  Public Function GetHelvetica(fontSize As Single, bold As Boolean) As PdfFont
   If bold Then
    Return New PdfStandardFont(PdfFontFamily.Helvetica, fontSize, PdfFontStyle.Bold)
   Else
    Return New PdfStandardFont(PdfFontFamily.Helvetica, fontSize)
   End If
  End Function

 End Class
End Namespace
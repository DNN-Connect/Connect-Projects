Imports System.Drawing
Imports Connect.DNN.Modules.Projects.Common
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
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

  Public Sub New(moduleId As Integer, projectId As Integer)

   PageSettings.Size = PdfPageSize.A3
   PageSettings.Margins.All = 0.0F
   BlackPen = New PdfPen(PdfBrushes.Black, 1.0F)
   GrayPen = New PdfPen(PdfBrushes.Gray, 3.0F)
   BrownBrush = New PdfSolidBrush(New PdfColor(71, 41, 43))
   InitPage()

   Dim project As Project = ProjectsController.GetProject(moduleId, projectId)
   Dim projectColor As Color = project.ProjectColor.ToColor()
   Dim contrastColor As Color = Color.White
   If projectColor.CanUseBlack Then
    contrastColor = Color.Black
   End If
   Dim bannerColor As New PdfColor(projectColor)

   PageGraphics.DrawRectangle(New PdfSolidBrush(bannerColor), 0.0F, 0.0F.MillimetersToPoints(), 297.0F.MillimetersToPoints(), 25.0F.MillimetersToPoints())
   Dim font As PdfFont = GetHelvetica(30, True)
   Dim te As New PdfTextElement(project.ProjectName)
   te.StringFormat = New PdfStringFormat(PdfTextAlignment.Left)
   te.Font = font
   te.Brush = New PdfSolidBrush(contrastColor)
   te.Draw(Page, New RectangleF(10.0F.MillimetersToPoints(), 20.0F.MillimetersToPoints() - (font.MeasureString(te.Text).Height), 200.0F.MillimetersToPoints(), font.MeasureString(te.Text).Height))

   Dim y As Single = 30.0F
   DrawTextBlock("Project Type", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.TypeDescription, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   y += 2
   DrawTextBlock("Owners", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Owners, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   y += 2
   DrawTextBlock("Status", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Status, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   y += 2
   DrawTextBlock("Url", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Url1, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   If project.Url2 <> "" Then
    DrawTextBlock(project.Url2, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   End If
   y += 2
   DrawTextBlock("Aims", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Aims, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   y += 2
   DrawTextBlock("Description", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Description, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)
   y += 2
   DrawTextBlock("Dependencies", GetHelvetica(18, False), projectColor, y, 10.0F, 100.0F)
   DrawTextBlock(project.Dependencies, GetCourier(12, False), Color.Black, y, 10.0F, 100.0F)

  End Sub

  Private Sub DrawTextBlock(text As String, font As PdfFont, fontColor As Color, ByRef top As Single, left As Single, width As Single)

   Dim te As New PdfTextElement(text)
   te.StringFormat = New PdfStringFormat(PdfTextAlignment.Left)
   te.Font = font
   te.Brush = New PdfSolidBrush(fontColor)
   Dim res As PdfLayoutResult = te.Draw(Page, New RectangleF(left.MillimetersToPoints(), top.MillimetersToPoints(), width.MillimetersToPoints(), (297.0F - top).MillimetersToPoints()))
   top = top + (Pt * res.Bounds.Height)

  End Sub

  Public Sub InitPage()

   Page = Pages.Add()
   PageGraphics = Page.Graphics

   PageGraphics.DrawRectangle(BrownBrush, 0.0F, 410.0F.MillimetersToPoints(), 297.0F.MillimetersToPoints(), 10.0F.MillimetersToPoints())
   DrawTextBlock("DNN Connect Association", GetHelvetica(10, False), Color.White, 412, 20, 200)

  End Sub

  Public Function GetHelvetica(fontSize As Single, bold As Boolean) As PdfFont
   If bold Then
    Return New PdfStandardFont(PdfFontFamily.Helvetica, fontSize, PdfFontStyle.Bold)
   Else
    Return New PdfStandardFont(PdfFontFamily.Helvetica, fontSize)
   End If
  End Function

  Public Function GetCourier(fontSize As Single, bold As Boolean) As PdfFont
   If bold Then
    Return New PdfStandardFont(PdfFontFamily.Courier, fontSize, PdfFontStyle.Bold)
   Else
    Return New PdfStandardFont(PdfFontFamily.Courier, fontSize)
   End If
  End Function

 End Class
End Namespace
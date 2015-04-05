Imports System.CodeDom
Imports System.Drawing
Imports System.Linq
Imports Connect.DNN.Modules.Projects.Common
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Controllers.ProjectTypes
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports Connect.DNN.Modules.Projects.Models.ProjectTypes
Imports Connect.DNN.Modules.Projects.Models.Urls
Imports DotNetNuke.Entities.Portals
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
  Public Property ModuleMapPath As String

  Public Sub New(moduleId As Integer, projectId As Integer)

   PageSettings.Size = PdfPageSize.A3
   PageSettings.Margins.All = 0.0F
   BlackPen = New PdfPen(PdfBrushes.Black, 1.0F)
   GrayPen = New PdfPen(PdfBrushes.Gray, 3.0F)
   BrownBrush = New PdfSolidBrush(New PdfColor(71, 41, 43))
   ModuleMapPath = HttpContext.Current.Server.MapPath(ModulePath)
   InitPage()

   Dim project As Project = ProjectsController.GetProject(moduleId, projectId)
   Dim projectTypes As IEnumerable(Of ProjectType) = ProjectTypesController.GetProjectTypes(projectId)
   Dim projectColor As Color = project.ProjectColor.ToColor()
   Dim iconPath As String = "images\"
   Dim contrastColor As Color = Color.White
   If projectColor.CanUseBlack Then
    contrastColor = Color.Black
    iconPath &= "fa-black\"
   Else
    iconPath &= "fa-white\"
   End If
   Dim bannerColor As New PdfColor(projectColor)
   Dim titleFontSize As Single = 18
   Dim bodyFontSize As Single = 12
   Dim tinyFontSize As Single = 9
   Dim distanceToNextHeading As Single = 3.0F
   Dim paddingUnderTitle As Single = 1.0F

   PageGraphics.DrawRectangle(New PdfSolidBrush(bannerColor), 0.0F, 0.0F.MillimetersToPoints(), 297.0F.MillimetersToPoints(), 25.0F.MillimetersToPoints())
   Dim font As PdfFont = GetHelvetica(30, True)
   Dim te As New PdfTextElement(project.ProjectName)
   te.StringFormat = New PdfStringFormat(PdfTextAlignment.Left)
   te.Font = font
   te.Brush = New PdfSolidBrush(contrastColor)
   te.Draw(Page, New RectangleF(10.0F.MillimetersToPoints(), 20.0F.MillimetersToPoints() - (font.MeasureString(te.Text).Height), 200.0F.MillimetersToPoints(), font.MeasureString(te.Text).Height))

   Dim x As Single = 272.0F
   For Each p As ProjectType In projectTypes
    Dim icon As New PdfBitmap(ModuleMapPath & iconPath & p.TypeIcon & ".png")
    PageGraphics.DrawImage(icon, x.MillimetersToPoints, 2.0F.MillimetersToPoints, 21.0F.MillimetersToPoints, 21.0F.MillimetersToPoints)
    x -= 25
   Next

   Dim y As Single = 40.0F
   DrawTextBlock("Project Type(s)", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   Dim types As String = String.Join(", ", projectTypes.Select(Function(t) t.TypeDescription))
   DrawTextBlock(types, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("License Type", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.LicenseType, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("Owners", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.Owners, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("People/Authors", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.People, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("Status", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.Status, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   If project.Urls.Count > 0 Then
    DrawTextBlock("Url", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
    y += paddingUnderTitle
    For Each url As UrlBase In project.Urls
     DrawTextBlock(url.Url, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
     If url.LastChecked IsNot Nothing Then
      DrawTextBlock(String.Format("Last checked {0:D}", url.LastChecked), GetCourier(tinyFontSize, False), Color.Gray, y, 10.0F, 130.0F)
     End If
    Next
   End If
   y += distanceToNextHeading
   DrawTextBlock("Aims", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.Aims, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("Description", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.Description, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)
   y += distanceToNextHeading
   DrawTextBlock("Dependencies", GetHelvetica(titleFontSize, False), projectColor, y, 10.0F, 130.0F)
   y += paddingUnderTitle
   DrawTextBlock(project.Dependencies, GetCourier(bodyFontSize, False), Color.Black, y, 10.0F, 130.0F)

   Dim imageMapPath As String = String.Format("{0}Connect\Projects\{1}\{2}\", PortalSettings.Current.HomeDirectoryMapPath, moduleId, projectId)
   Dim album As New ImageCollection(imageMapPath, "")

   y = 45
   Dim boxWidth As Single = 110.0F
   Dim boxHeight As Single = 110.0F
   Dim boxAr As Double = boxWidth / boxHeight
   Dim columnWidth As Single = 297.0F / 2.0F
   For Each img As Common.Image In album.Images
    Dim im As New PdfBitmap(imageMapPath & img.File & "_zoom" & img.Extension)
    Dim ar As Double = im.Width / im.Height
    If ar > boxAr Then
     Dim zoom As Double = boxWidth / im.Width
     Dim newHeight As Single = CSng(im.Height * zoom)
     If y + newHeight > 410 Then Exit For
     PageGraphics.DrawImage(im,
                            (columnWidth + (columnWidth - boxWidth) / 2).MillimetersToPoints, y.MillimetersToPoints,
                            boxWidth.MillimetersToPoints, newHeight.MillimetersToPoints)
     y += newHeight
    Else
     Dim zoom As Double = boxHeight / im.Height
     Dim newWidth As Single = CSng(im.Width * zoom)
     If y + boxHeight > 410 Then Exit For
     PageGraphics.DrawImage(im,
                            (columnWidth + (columnWidth - newWidth) / 2).MillimetersToPoints, y.MillimetersToPoints,
                            newWidth.MillimetersToPoints, boxHeight.MillimetersToPoints)
     y += boxHeight
    End If
    y += 2
    If img.Remarks <> "" Then
     DrawTextBlock(img.Remarks, GetHelvetica(10, True), Color.Black, y, columnWidth, columnWidth, PdfTextAlignment.Center)
    End If
    y += 5
   Next

  End Sub

  Private Sub DrawTextBlock(text As String, font As PdfFont, fontColor As Color, ByRef top As Single, left As Single, width As Single)
   DrawTextBlock(text, font, fontColor, top, left, width, PdfTextAlignment.Left)
  End Sub

  Private Sub DrawTextBlock(text As String, font As PdfFont, fontColor As Color, ByRef top As Single, left As Single, width As Single, align As PdfTextAlignment)

   Dim te As New PdfTextElement(text)
   te.StringFormat = New PdfStringFormat(align)
   te.Font = font
   te.Brush = New PdfSolidBrush(fontColor)
   Dim res As PdfLayoutResult = te.Draw(Page, New RectangleF(left.MillimetersToPoints(), top.MillimetersToPoints(), width.MillimetersToPoints(), (420.0F - top).MillimetersToPoints()))
   top = top + (Pt * res.Bounds.Height)

  End Sub

  Public Sub InitPage()

   Page = Pages.Add()
   PageGraphics = Page.Graphics

   PageGraphics.DrawRectangle(BrownBrush, 0.0F, 410.0F.MillimetersToPoints(), 297.0F.MillimetersToPoints(), 10.0F.MillimetersToPoints())
   Dim logo As New PdfBitmap(ModuleMapPath & "images\logo_large.png")
   PageGraphics.DrawImage(logo, 2.0F.MillimetersToPoints, 410.8F.MillimetersToPoints, 7.0F.MillimetersToPoints, 7.0F.MillimetersToPoints)
   DrawTextBlock("DNNCONNECT", GetAgency(12), Color.White, 412, 11, 200)
   DrawTextBlock("DNN Connect Association, " & Now.Year.ToString(), GetHelvetica(10, False), Color.White, 412.5, 0, 297, PdfTextAlignment.Center)

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

  Public Function GetFontAwesome(fontSize As Single) As PdfFont
   Return New PdfTrueTypeFont(ModuleMapPath & "fonts\fontawesome.ttf", fontSize)
  End Function

  Public Function GetAgency(fontSize As Single) As PdfFont
   Return New PdfTrueTypeFont(ModuleMapPath & "fonts\AGENCYB.TTF", fontSize)
  End Function

 End Class
End Namespace
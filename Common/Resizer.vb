Imports System.Drawing
Imports System.Drawing.Imaging

Namespace Common
 Public Class Resizer

  Private Property Settings As ModuleSettings

  Public Sub New(settings As ModuleSettings)
   Me.Settings = settings
  End Sub

  Public Sub Process(originalFile As String)

   ' Load Image
   Using thisImage As New Bitmap(originalFile)

    Dim imgFormat As ImageFormat = thisImage.RawFormat
    Dim originalWidth As Integer = thisImage.Width
    Dim originalHeight As Integer = thisImage.Height
    Dim imgRatio As Single = Convert.ToSingle(originalHeight / originalWidth)

    Dim ext As String = IO.Path.GetExtension(originalFile)
    Dim saveFilename As String = Left(originalFile, originalFile.Length - ext.Length)

    ' Resize Image
    ResizeImage(thisImage, imgFormat, originalWidth, originalHeight, imgRatio, Settings.TnWidth, Settings.TnHeight, Settings.TnFit, saveFilename & "_tn" & ext)
    ResizeImage(thisImage, imgFormat, originalWidth, originalHeight, imgRatio, Settings.MedWidth, Settings.MedHeight, Settings.MedFit, saveFilename & "_med" & ext)
    ResizeImage(thisImage, imgFormat, originalWidth, originalHeight, imgRatio, Settings.ZoomWidth, Settings.ZoomHeight, Settings.ZoomFit, saveFilename & "_zoom" & ext)

   End Using

  End Sub

  Private Sub ResizeImage(thisImage As Bitmap, imgFormat As ImageFormat, originalWidth As Integer, originalHeight As Integer, imgRatio As Single, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer, FitType As String, SaveAs As String)

   Dim newHeight As Integer = MaxHeight
   Dim newWidth As Integer = MaxWidth
   Dim scaleW As Double = MaxWidth / originalWidth
   Dim scaleH As Double = MaxHeight / originalHeight
   Dim scaleX As Double = scaleW
   Dim scaleY As Double = scaleH
   Dim newX As Integer = 0
   Dim newY As Integer = 0

   Select Case FitType
    Case "Shrink"
     scaleX = Math.Min(scaleW, scaleH)
     scaleY = scaleX
     newHeight = Convert.ToInt32(originalHeight * scaleX)
     newWidth = Convert.ToInt32(originalWidth * scaleY)
    Case "Crop"
     scaleX = Math.Max(scaleW, scaleH)
     scaleY = scaleX
     If scaleW > scaleH Then
      newY = -1 * Convert.ToInt32(((scaleX * originalHeight) - MaxHeight) / 2)
     Else
      newX = -1 * Convert.ToInt32(((scaleX * originalWidth) - MaxWidth) / 2)
     End If
    Case Else
     ' Stretch
   End Select

   Using backBuffer As Bitmap = New Bitmap(newWidth, newHeight, Drawing.Imaging.PixelFormat.Format24bppRgb)
    Using backBufferGraphics As Graphics = Graphics.FromImage(backBuffer)
     backBufferGraphics.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
     backBufferGraphics.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
     backBufferGraphics.DrawImage(thisImage, newX - 1, newY - 1, Convert.ToInt32(originalWidth * scaleX) + 2, Convert.ToInt32(originalHeight * scaleY) + 2)
     backBuffer.Save(SaveAs, imgFormat)
    End Using
   End Using

  End Sub

 End Class
End Namespace
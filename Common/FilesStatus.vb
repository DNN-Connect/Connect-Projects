Namespace Common
 Public Class FilesStatus
  Public Property name As String
  Public Property ext As String
  Public Property size As Integer
  Public Property progress As String
  Public Property url As String
  Public Property thumbnail_url As String
  Public Property [error] As String

  Public Sub New(imagesPath As String, fileKey As String, extension As String, fileLength As Integer)
   name = fileKey
   ext = extension
   size = fileLength
   progress = "1.0"
   url = imagesPath & fileKey & extension
   thumbnail_url = imagesPath & fileKey & "_tn" & extension
  End Sub
 End Class
End Namespace
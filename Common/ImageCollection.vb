Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Xml.Serialization

Namespace Common

 <Serializable()>
 <DataContract>
 Public Class ImageCollection

  <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), XmlElement("image")>
  <DataMember()>
  Public Property Images As New List(Of Image)

  <DataMember()>
  Public Property ImagePath As String = ""

  Private Property AlbumFile As String = ""
  Private _imagesMapPath As String = ""

  Public Sub New(imagesMapPath As String, imagesPath As String)
   MyBase.New()
   ImagePath = imagesPath
   AlbumFile = imagesMapPath & "album.xml"
   _imagesMapPath = imagesMapPath
   Dim x As New System.Xml.Serialization.XmlSerializer(GetType(ImageCollection))
   If IO.File.Exists(AlbumFile) Then
    Using rdr As New IO.StreamReader(AlbumFile)
     Dim a As ImageCollection = CType(x.Deserialize(rdr), ImageCollection)
     Me.Images = a.Images
    End Using
   End If
  End Sub
  Public Sub New()
  End Sub

  Public Sub Save()
   Save(AlbumFile)
  End Sub

  Public Sub Save(filePath As String)
   Dim x As New System.Xml.Serialization.XmlSerializer(GetType(ImageCollection))
   Using w As New IO.StreamWriter(filePath, False, System.Text.Encoding.UTF8)
    x.Serialize(w, Me)
   End Using
  End Sub

  Public Sub WriteOrder()
   Dim i As Integer = 0
   For Each img As Image In Images
    img.Order = i
    i += 1
   Next
  End Sub

  Public Sub Sort()
   Images.Sort(Function(x, y)
                Return x.Order.CompareTo(y.Order)
               End Function)
  End Sub

  Public Sub Delete(file As String)
   Try
    Dim i As Image = (From x In Images Select x Where x.File = file)(0)
    If IO.File.Exists(String.Format("{0}{1}{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}{2}", _imagesMapPath, file, i.Extension))
    If IO.File.Exists(String.Format("{0}{1}_tn{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}_tn{2}", _imagesMapPath, file, i.Extension))
    If IO.File.Exists(String.Format("{0}{1}_zoom{2}", _imagesMapPath, file, i.Extension)) Then IO.File.Delete(String.Format("{0}{1}_zoom{2}", _imagesMapPath, file, i.Extension))
    Images.Remove(i)
   Catch ex As Exception
   End Try
  End Sub

  Public Sub Recheck()
   Dim hasChanges As Boolean = False
   Dim registered As New List(Of String)
   Dim disappeared As New List(Of Image)
   For Each i As Image In Images
    If Not IO.File.Exists(_imagesMapPath & i.File & i.Extension) Then
     disappeared.Add(i)
    Else
     registered.Add(i.File)
    End If
   Next
   ' remove disappeared images from album
   For Each i As Image In disappeared
    hasChanges = True
    Images.Remove(i)
   Next
   ' pick up new images
   For Each f As String In IO.Directory.GetFiles(_imagesMapPath, "*.*")
    Dim m As Match = Regex.Match(f, "(?i)(\d{8}-\d{6,})\.(?-i)")
    If m.Success Then
     Dim fname As String = m.Groups(1).Value
     If Not registered.Contains(fname) Then
      Dim i As New Image With {.Extension = IO.Path.GetExtension(f), .File = fname, .Title = fname, .Order = Images.Count + 1}
      Images.Add(i)
      registered.Add(fname)
      hasChanges = True
     End If
    End If
   Next
   ' remove orphaned thumbnails/zooms
   For Each f As String In IO.Directory.GetFiles(_imagesMapPath, "*.*")
    Dim m As Match = Regex.Match(f, "(?i)([^_\\\.]+)(_tn|_zoom)\.(?-i)")
    If m.Success Then
     Dim fname As String = m.Groups(1).Value
     If Not registered.Contains(fname) Then
      Try
       IO.File.Delete(f)
      Catch ex As Exception
      End Try
     End If
    End If
   Next
   If hasChanges Then Save()
  End Sub

  Public Sub UpdateTitle(file As String, title As String)
   Try
    Dim i As Image = (From x In Images Select x Where x.File = file)(0)
    i.Title = title
   Catch ex As Exception
   End Try
  End Sub

  Public Sub UpdateRemarks(file As String, remarks As String)
   Try
    Dim i As Image = (From x In Images Select x Where x.File = file)(0)
    i.Remarks = remarks
   Catch ex As Exception
   End Try
  End Sub

 End Class
End Namespace
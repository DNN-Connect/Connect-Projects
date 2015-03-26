Imports System.Runtime.CompilerServices

Namespace Common
 Public Module Globals
  Public Const SharedResourceFileName As String = "~/DesktopModules/Connect/Projects/App_LocalResources/SharedResources.resx"

  <Extension()>
  Public Sub Add(Of T)(ByRef arr As T(), item As T)
   Array.Resize(arr, arr.Length + 1)
   arr(arr.Length - 1) = item
  End Sub

  <Extension()>
  Public Function MillimetersToPoints(mm As Single) As Single
   Return CSng(mm * 2.840626)
  End Function

  Public Function GetUploadedFileName(folder As String, originalFilename As String) As String
   For Each f As String In IO.Directory.GetFiles(folder, "*.resources")
    If ReadFile(f) = originalFilename Then
     Return IO.Path.GetFileNameWithoutExtension(f)
    End If
   Next
   Return ""
  End Function

  Public Function ReadFile(ByVal fileName As String) As String
   Return ReadFile(fileName, 10)
  End Function
  Public Function ReadFile(ByVal fileName As String, retries As Integer) As String
   If Not IO.File.Exists(fileName) Then Return ""
   If retries = 0 Then Return ""
   Try
    Using sr As New IO.StreamReader(fileName)
     Return sr.ReadToEnd
    End Using
   Catch ioex As IO.IOException
    Threading.Thread.Sleep(200)
    Return ReadFile(fileName, retries - 1)
   Catch ex As Exception
    Return ""
   End Try
  End Function
  Public Sub WriteTextToFile(filePath As String, textToWrite As String)
   WriteTextToFile(filePath, textToWrite, 10)
  End Sub
  Public Sub WriteTextToFile(filePath As String, textToWrite As String, retries As Integer)
   If retries = 0 Then Exit Sub
   Try
    Using sw As New IO.StreamWriter(filePath)
     sw.Write(textToWrite)
     sw.Flush()
    End Using
   Catch ioex As IO.IOException
    Threading.Thread.Sleep(200)
    WriteTextToFile(filePath, textToWrite, retries - 1)
   Catch ex As Exception
   End Try
  End Sub

 End Module
End Namespace
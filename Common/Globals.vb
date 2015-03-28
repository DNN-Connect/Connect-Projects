Imports System.Drawing
Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace Common
 Public Module Globals
  Public Const SharedResourceFileName As String = "~/DesktopModules/Connect/Projects/App_LocalResources/SharedResources.resx"
  Public Const Pt As Single = 0.352777778
  Public Const ModulePath As String = "~/DesktopModules/Connect/Projects/"

  <Extension()>
  Public Sub Add(Of T)(ByRef arr As T(), item As T)
   Array.Resize(arr, arr.Length + 1)
   arr(arr.Length - 1) = item
  End Sub

  <Extension()>
  Public Function MillimetersToPoints(mm As Single) As Single
   Return CSng(mm * 2.840626)
  End Function

  <Extension()>
  Public Function ToColor(input As String) As Color
   input = input.Trim("#"c)
   Try
    Return Color.FromArgb(CInt(Val("&H" & Mid(input, 1, 2))), CInt(Val("&H" & Mid(input, 3, 2))), CInt(Val("&H" & Mid(input, 5, 2))))
   Catch ex As Exception
    Throw New Exception(String.Format("{0} is not a valid hexadecimal color string", input))
   End Try
  End Function

  <Extension()>
  Public Function CanUseBlack(input As Color) As Boolean
   Dim R As Double = input.R / 256
   Dim G As Double = input.G / 256
   Dim B As Double = input.B / 256
   Dim lum As Double = R * 0.2126 + G * 0.7152 + B * 0.0722
   Dim sat As Double = (Max(R, G, B) - Min(R, G, B)) / Max(R, G, B)
   If sat < 0.4 Then
    If lum > 0.5 Then Return True
   End If
   Return False
  End Function

  Public Function Max(input As Double, ParamArray values() As Double) As Double
   input = values.Aggregate(input, Function(current, value) Math.Max(current, value))
   Return input
  End Function

  Public Function Min(input As Double, ParamArray values() As Double) As Double
   input = values.Aggregate(input, Function(current, value) Math.Min(current, value))
   Return input
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

#Region " Read Value Extensions "
  <Extension()>
  Public Sub ReadValue(ByRef valueTable As NameValueCollection, valueName As String, ByRef variable As Integer)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     variable = CType(valueTable.Item(valueName), Integer)
    Catch ex As Exception
    End Try
   End If
  End Sub

  <Extension()>
  Public Sub ReadValue(ByRef valueTable As NameValueCollection, valueName As String, ByRef variable As String)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     variable = valueTable.Item(valueName)
    Catch ex As Exception
    End Try
   End If
  End Sub

  <Extension()>
  Public Sub ReadValue(ByRef valueTable As NameValueCollection, valueName As String, ByRef variable As Boolean)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     Select Case valueTable.Item(valueName).ToLower()
      Case "true", "on", "1", "yes"
       variable = True
      Case Else
       variable = False
     End Select
    Catch ex As Exception
    End Try
   End If
  End Sub

  <Extension()>
  Public Sub ReadValue(ByRef valueTable As Hashtable, valueName As String, ByRef variable As Integer)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     variable = CType(valueTable.Item(valueName), Integer)
    Catch ex As Exception
    End Try
   End If
  End Sub

  <Extension()>
  Public Sub ReadValue(ByRef valueTable As Hashtable, valueName As String, ByRef variable As String)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     variable = CStr(valueTable.Item(valueName))
    Catch ex As Exception
    End Try
   End If
  End Sub

  <Extension()>
  Public Sub ReadValue(ByRef valueTable As Hashtable, valueName As String, ByRef variable As Boolean)
   If Not valueTable.Item(valueName) Is Nothing Then
    Try
     Select Case CStr(valueTable.Item(valueName)).ToLower()
      Case "true", "on", "1", "yes"
       variable = True
      Case Else
       variable = False
     End Select
    Catch ex As Exception
    End Try
   End If
  End Sub
#End Region

 End Module
End Namespace
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Script.Serialization
Imports Connect.DNN.Modules.Projects.Common
Imports DotNetNuke.Web.Api

Namespace Controllers
 Public Class ModuleController
  Inherits ModuleApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function Template(view As String) As HttpResponseMessage
   Dim ctl As New RazorControl(ActiveModule, String.Format("~/DesktopModules/Connect/Projects/Views/Partials/{0}.vbhtml", view), Globals.SharedResourceFileName)
   Dim content As New StringContent(ctl.RenderObject(), Encoding.UTF8, "text/html")
   Dim res As New HttpResponseMessage(HttpStatusCode.OK)
   res.Content = content
   Return res
  End Function
#End Region

#Region " Upload Handler "
  Private ReadOnly js As New JavaScriptSerializer()

  <HttpPost()>
  <DnnModuleAuthorize(PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function UploadFile(id As Integer) As HttpResponseMessage
   Dim res As New HttpResponseMessage(HttpStatusCode.OK)
   Dim statuses As New List(Of FilesStatus)
   HandleUploadFile(System.Web.HttpContext.Current, id, statuses)
   System.Web.HttpContext.Current.Response.ContentType = "text/plain"
   res.Content = New StringContent(WriteJsonIframeSafe(System.Web.HttpContext.Current, statuses))
   Return res
  End Function

  <HttpGet()>
  <DnnModuleAuthorize(PermissionKey:="SUBMITTER")>
  <ValidateAntiForgeryToken()>
  Public Function CommitFile(id As Integer, fileName As String) As HttpResponseMessage
   Dim localFile As String = GetImageMapPath(id) & fileName
   If IO.File.Exists(localFile) Then
    Dim r As New Resizer(Settings)
    r.Process(localFile)
   End If
   Dim res As New ImageCollection(GetImageMapPath(id), GetImagePath(id))
   res.Recheck()
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function

  Private Function WriteJsonIframeSafe(context As HttpContext, statuses As List(Of FilesStatus)) As String
   context.Response.AddHeader("Vary", "Accept")
   Try
    If context.Request("HTTP_ACCEPT").Contains("application/json") Then
     context.Response.ContentType = "application/json"
    Else
     context.Response.ContentType = "text/plain"
    End If
   Catch
    context.Response.ContentType = "text/plain"
   End Try
   Return js.Serialize(statuses.ToArray())
  End Function

  ' Upload file to the server
  Private Sub HandleUploadFile(context As HttpContext, projectId As Integer, ByRef statuses As List(Of FilesStatus))
   Dim headers As NameValueCollection = context.Request.Headers
   If String.IsNullOrEmpty(headers("X-File-Name")) Then
    UploadWholeFiles(projectId, context, statuses)
   Else
    UploadPartialFile(projectId, headers("X-File-Name"), context, statuses)
   End If
  End Sub

  ' Upload partial file
  Private Sub UploadPartialFile(projectId As Integer, fileName As String, context As HttpContext, ByRef statuses As List(Of FilesStatus))
   fileName = Path.GetFileName(fileName)
   Dim extension As String = Path.GetExtension(fileName)
   Dim fileToWriteTo As String = ""
   Dim localFile As String = GetUploadedFileName(GetImageMapPath(projectId), fileName)
   If localFile = "" Then
    fileToWriteTo = GetNewFilekey(projectId, extension)
    WriteTextToFile(String.Format("{0}{1}.resources", GetImageMapPath(projectId), fileToWriteTo), fileName)
   Else
    fileToWriteTo = localFile
   End If
   Dim fullName As String = String.Format("{0}{1}{2}", GetImageMapPath(projectId), fileToWriteTo, Path.GetExtension(fileName))
   Using inputStream As Stream = context.Request.Files(0).InputStream
    Using fs As New FileStream(fullName, FileMode.Append, FileAccess.Write)
     Dim buffer(1023) As Byte
     Dim l As Integer = inputStream.Read(buffer, 0, 1024)
     While l > 0
      fs.Write(buffer, 0, l)
      l = inputStream.Read(buffer, 0, 1024)
     End While
     fs.Flush()
     fs.Close()
    End Using
   End Using
   Dim f As New FileInfo(fullName)
   statuses.Add(New FilesStatus(GetImagePath(projectId), fileToWriteTo, extension, CInt(f.Length)))
  End Sub

  Private Sub UploadWholeFiles(projectId As Integer, context As HttpContext, ByRef statuses As List(Of FilesStatus))
   For i As Integer = 0 To context.Request.Files.Count - 1
    UploadWholeFile(projectId, context.Request.Files(i), statuses, 10)
   Next
  End Sub

  ' Upload entire file
  Private Sub UploadWholeFile(projectId As Integer, file As HttpPostedFile, ByRef statuses As List(Of FilesStatus), retries As Integer)
   If retries = 0 Then Exit Sub
   Dim extension As String = Path.GetExtension(file.FileName)
   Dim newFile As String = GetNewFilekey(projectId, extension)
   Dim fileName As String = Path.GetFileName(file.FileName)
   Dim fullName As String = GetImageMapPath(projectId) & newFile & extension
   Try
    file.SaveAs(fullName)
    WriteTextToFile(GetImageMapPath(projectId) & newFile & ".resources", file.FileName)
   Catch ioex As IOException
    Threading.Thread.Sleep(500)
    UploadWholeFile(projectId, file, statuses, retries - 1)
   Catch ex As Exception
    '
   End Try
   statuses.Add(New FilesStatus(GetImagePath(projectId), newFile, extension, file.ContentLength))
  End Sub

  Private Function GetNewFilekey(projectId As Integer, extension As String) As String
   Dim res As String = String.Format("{0:yyyyMMdd}-{0:HHmmss}", Now)
   If IO.File.Exists(GetImageMapPath(projectid) & res & extension) Then
    Dim i As Integer = 0
    Do While IO.File.Exists(GetImageMapPath(projectid) & res & i.ToString & extension)
     i += 1
    Loop
    res &= i.ToString
   End If
   Return res
  End Function
#End Region


 End Class
End Namespace

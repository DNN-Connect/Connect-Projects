Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Connect.DNN.Modules.Projects.Common
Imports DotNetNuke.Web.Api

Namespace Controllers
 Public Class ModuleController
  Inherits DnnApiController

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

 End Class
End Namespace

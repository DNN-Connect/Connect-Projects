Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Connect.DNN.Modules.Projects.Controllers.PTypes
Imports DotNetNuke.Security

Imports DotNetNuke.Web.Api

Namespace Controllers.ProjectTypes

 Partial Public Class ProjectTypesController
  Inherits DnnApiController

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=SecurityAccessLevel.View)>
  Public Function [Project](id As Integer) As HttpResponseMessage
   Return Request.CreateResponse(HttpStatusCode.OK, PTypesController.GetSelectionList(id))
  End Function
#End Region

 End Class
End Namespace

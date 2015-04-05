Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Imports DotNetNuke.Web.Api

Namespace Controllers.Urls

 Partial Public Class UrlsController
  Inherits DnnApiController
  Implements IServiceRouteMapper

#Region " IServiceRouteMapper "
  Public Sub RegisterRoutes(mapRouteManager As DotNetNuke.Web.Api.IMapRoute) Implements DotNetNuke.Web.Api.IServiceRouteMapper.RegisterRoutes
   mapRouteManager.MapHttpRoute("Connect/Projects", "Default", "{controller}/{action}/{id}", New With {.Controller = "Urls", .Action = "MyMethod"}, New With {.id = "\d*"}, New String() {"Connect.DNN.Modules.Projects.Data.Controllers.Urls"})
  End Sub
#End Region

#Region " Service Methods "
  <HttpGet()>
  <DnnModuleAuthorize(AccessLevel:=DotNetNuke.Security.SecurityAccessLevel.View)>
  Public Function MyMethod(id As Integer) As HttpResponseMessage
   Dim res As Boolean = True
   Return Request.CreateResponse(HttpStatusCode.OK, res)
  End Function
#End Region

 End Class
End Namespace

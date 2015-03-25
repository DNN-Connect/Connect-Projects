Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Web.Api

Namespace Common
 Public Class ModuleApiController
  Inherits DnnApiController

  Private _security As ContextSecurity
  Public ReadOnly Property Security As ContextSecurity
   Get
    If _security Is Nothing Then
     _security = New ContextSecurity(ActiveModule, UserController.GetCurrentUserInfo())
    End If
    Return _security
   End Get
  End Property

 End Class
End Namespace
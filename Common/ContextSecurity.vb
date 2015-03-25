Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Entities.Modules

Namespace Common
 Public Class ContextSecurity

  Public Property CanEdit As Boolean = False
  Public Property Moderator As Boolean = False
  Public Property Submitter As Boolean = False

#Region " Constructor "
  Public Sub New(objModule As ModuleInfo, user As UserInfo)
   CanEdit = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "EDIT")
   Moderator = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "MODERATOR")
   Submitter = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "SUBMITTER")
  End Sub
#End Region

 End Class
End Namespace
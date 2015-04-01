Imports System.Linq
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Localization.Localization
Imports DotNetNuke.Services.Social.Notifications

Namespace Integration

 Public Class NotificationController

#Region " Integration Methods "
  Public Shared Sub ProjectPendingApproval(portalId As Integer, moduleId As Integer, projectId As Integer)

   Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(NotificationPublishingTypeName)
   Dim project As Project = ProjectsController.GetProject(moduleId, projectId)

   Dim notificationKey As New NotificationKey(ContentTypeName, project.ModuleId, project.ProjectId)
   Dim objNotification As New Notification

   objNotification.NotificationTypeID = notificationType.NotificationTypeId
   objNotification.Subject = GetString("ProjectAdded.Title", SharedResourceFileName)
   objNotification.Body = String.Format(GetString("ProjectAdded.Summary", SharedResourceFileName), project.CreatedByUser, project.ProjectName)
   objNotification.IncludeDismissAction = False
   objNotification.SenderUserID = project.CreatedByUserID
   objNotification.Context = notificationKey.ToString

   Dim roles As New List(Of RoleInfo)
   Dim users As New List(Of UserInfo)
   Dim permissions As ModulePermissionCollection = (New PermissionProvider).GetModulePermissions(project.ModuleId, -1)
   For Each perm As ModulePermissionInfo In permissions.Where(Function(p) p.PermissionKey = "MODERATOR")
    If Not String.IsNullOrEmpty(perm.RoleName) Then
     roles.Add((New RoleController).GetRole(perm.RoleID, portalId))
    Else
     users.Add(UserController.GetUserById(portalId, perm.UserID))
    End If
   Next

   NotificationsController.Instance.SendNotification(objNotification, portalId, roles, users)

  End Sub

  Public Shared Sub RemoveProjectPendingNotification(moduleId As Integer, projectId As Integer)
   Dim notificationType As NotificationType = NotificationsController.Instance.GetNotificationType(NotificationPublishingTypeName)
   Dim notificationKey As New NotificationKey(ContentTypeName, moduleId, projectId)
   Dim objNotify As Notification = NotificationsController.Instance.GetNotificationByContext(notificationType.NotificationTypeId, notificationKey.ToString).SingleOrDefault
   If objNotify IsNot Nothing Then
    NotificationsController.Instance.DeleteAllNotificationRecipients(objNotify.NotificationID)
   End If
  End Sub
#End Region

#Region " Install Methods "
  ''' <summary>
  ''' This will create a notification type associated w/ the module and also handle the actions that must be associated with it.
  ''' </summary>
  ''' <remarks>This should only ever run once</remarks>
  Friend Shared Sub AddNotificationTypes()
   Dim actions As List(Of NotificationTypeAction) = New List(Of NotificationTypeAction)
   Dim deskModuleId As Integer = DesktopModuleController.GetDesktopModuleByFriendlyName("Connect Projects").DesktopModuleID

   Dim objNotificationType As NotificationType = New NotificationType
   objNotificationType.Name = NotificationPublishingTypeName
   objNotificationType.Description = "Project Approval."
   objNotificationType.DesktopModuleId = deskModuleId

   If NotificationsController.Instance.GetNotificationType(objNotificationType.Name) Is Nothing Then
    Dim objAction As New NotificationTypeAction
    objAction.NameResourceKey = "ApproveProject"
    objAction.DescriptionResourceKey = "ApproveProject_Desc"
    objAction.APICall = "DesktopModules/Connect/Projects/API/Projects/Approve"
    objAction.Order = 1
    actions.Add(objAction)

    objAction = New NotificationTypeAction
    objAction.NameResourceKey = "DeleteProject"
    objAction.DescriptionResourceKey = "DeleteProject_Desc"
    objAction.APICall = "DesktopModules/Connect/Projects/API/Projects/Delete"
    objAction.ConfirmResourceKey = "DeleteItem"
    objAction.Order = 3
    actions.Add(objAction)

    NotificationsController.Instance.CreateNotificationType(objNotificationType)
    NotificationsController.Instance.SetNotificationTypeActions(actions, objNotificationType.NotificationTypeId)
   End If

  End Sub

#End Region

 End Class

End Namespace
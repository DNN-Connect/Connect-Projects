Imports System.Linq
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.Localization.Localization
Imports DotNetNuke.Services.Social.Notifications

Namespace Integration

 Public Class NotificationController

#Region " Integration Methods "
  Public Shared Sub ProjectPendingApproval(portalId As Integer, tabId As Integer, moduleId As Integer, projectId As Integer)

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
   Dim permissions As ModulePermissionCollection = (New PermissionProvider).GetModulePermissions(project.ModuleId, tabId)
   For Each perm As ModulePermissionInfo In permissions.Where(Function(p) p.PermissionKey = "MODERATOR")
    If Not String.IsNullOrEmpty(perm.RoleName) Then
     roles.Add((New RoleController).GetRole(perm.RoleID, portalId))
    Else
     users.Add(UserController.GetUserById(portalId, perm.UserID))
    End If
   Next

   Try
    NotificationsController.Instance.SendNotification(objNotification, portalId, roles, users)
   Catch ex As Exception
   End Try

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

 End Class

End Namespace
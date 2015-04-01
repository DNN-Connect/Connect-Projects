Imports DotNetNuke.Services.Journal
Imports System.Linq
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects

Namespace Integration

 Public Class JournalController

#Region " Public Methods "
  Public Shared Sub AddProjectToJournal(portalId As Integer, tabId As Integer, moduleId As Integer, projectId As Integer)
   Dim project As Project = ProjectsController.GetProject(moduleId, projectId)
   Dim objectKey As String = ContentTypeName + "_" + String.Format("{0}", project.ProjectId)
   Dim ji As JournalItem = DotNetNuke.Services.Journal.JournalController.Instance.GetJournalItemByKey(portalId, objectKey)

   If Not ji Is Nothing Then
    DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
   End If

   ji = New JournalItem

   ji.PortalId = portalId
   ji.ProfileId = project.CreatedByUserID
   ji.UserId = project.CreatedByUserID
   ji.ContentItemId = -1
   ji.Title = project.ProjectName
   ji.ItemData = New ItemData()
   ji.ItemData.Url = DotNetNuke.Common.NavigateURL(tabId) & "#/Project/" & project.ProjectId.ToString()
   ji.Summary = project.Description
   ji.Body = Nothing
   ji.JournalTypeId = GetProjectCreatedTypeId(portalId)
   ji.ObjectKey = objectKey
   ji.SecuritySet = "E,"

   DotNetNuke.Services.Journal.JournalController.Instance.SaveJournalItem(ji, tabId)
  End Sub

  Public Shared Sub RemoveBlogPostFromJournal(projectId As Integer, portalId As Integer)
   Dim objectKey As String = ContentTypeName + "_" + String.Format("{0}", projectId)
   DotNetNuke.Services.Journal.JournalController.Instance.DeleteJournalItemByKey(portalId, objectKey)
  End Sub
#End Region

#Region " Private Methods "
  Private Shared Function GetProjectCreatedTypeId(portalId As Integer) As Integer
   Dim colJournalTypes As IEnumerable(Of JournalTypeInfo)
   colJournalTypes = (From t In DotNetNuke.Services.Journal.JournalController.Instance.GetJournalTypes(portalId) Where t.JournalType = "projectcreated")
   Dim journalTypeId As Integer
   If colJournalTypes.Count() > 0 Then
    Dim journalType As JournalTypeInfo = colJournalTypes.[Single]()
    journalTypeId = journalType.JournalTypeId
   Else
    journalTypeId = 23
   End If
   Return journalTypeId
  End Function
#End Region

 End Class

End Namespace
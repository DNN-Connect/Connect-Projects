Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Search.Entities

Namespace Integration
 Public Class ModuleController
  Inherits ModuleSearchBase
  Implements IUpgradeable
  
  Public Overrides Function GetModifiedSearchDocuments(moduleInfo As ModuleInfo, beginDate As Date) As IList(Of DotNetNuke.Services.Search.Entities.SearchDocument)

   Dim res As New List(Of SearchDocument)
   For Each p As Project In ProjectsController.GetProjects(moduleInfo.ModuleID)
    If p.LastModifiedOnDate >= beginDate Then
     Dim sd As New ProjectSearchDocument(moduleInfo, p)
     res.Add(sd.Document)
    End If
   Next
   Return res

  End Function

  Public Function UpgradeModule(version As String) As String Implements IUpgradeable.UpgradeModule

   Dim message As String = ""
   Select Case version
    Case "01.01.00"
     NotificationController.AddNotificationTypes()
     message &= "Added notification types to DNN" & vbCrLf
   End Select
   Return message

  End Function

 End Class
End Namespace
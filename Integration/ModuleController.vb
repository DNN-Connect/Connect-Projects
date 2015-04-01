Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Search.Entities

Namespace Integration
 Public Class ModuleController
  Inherits ModuleSearchBase

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

 End Class
End Namespace
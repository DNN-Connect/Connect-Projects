Imports System.Linq
Imports Connect.DNN.Modules.Projects.Controllers.ProjectTypes
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Search.Entities

Namespace Integration
 Public Class ProjectSearchDocument

  Public Property Document As New SearchDocument

  Public Sub New(moduleInfo As ModuleInfo, p As Project)
   With Document
    .AuthorUserId = p.LastModifiedByUserID
    .Title = p.ProjectName
    .Description = p.Description
    .ModifiedTimeUtc = p.LastModifiedOnDate
    .PortalId = moduleInfo.PortalID
    .UniqueKey = String.Format("CP-{0}-{1}", moduleInfo, p.ProjectId)
    .QueryString = String.Format("#/Project/{0}", p.ProjectId)
   End With
   AddDataLine("Project Types", String.Join(",", ProjectTypesController.GetProjectTypes(p.ProjectId).Select(Function(pt) pt.TypeDescription)))
   AddDataLine("License Type", p.LicenseType)
   AddDataLine("Owners", p.Owners)
   AddDataLine("People", p.People)
   AddDataLine("Status", p.Status)
   'AddDataLine("Url 1", p.Url1)
   'AddDataLine("Url 2", p.Url2)
   AddDataLine("Aims", p.Aims)
   AddDataLine("Dependencies", p.Dependencies)
  End Sub

  Private Sub AddDataLine(propName As String, propValue As String)
   If Not String.IsNullOrEmpty(Document.Body) Then
    Document.Body &= vbCrLf
   End If
   Document.Body &= String.Format("{0}: {1}", propName, propValue)
  End Sub

 End Class
End Namespace
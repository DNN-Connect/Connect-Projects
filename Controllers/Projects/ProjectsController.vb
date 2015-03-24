Imports System
Imports Connect.DNN.Modules.Projects.Repositories
Imports Connect.DNN.Modules.Projects.Models.Projects

Namespace Controllers.Projects

 Partial Public Class ProjectsController

  Public Shared Function GetProjects() As IEnumerable(Of Project)

   Dim repo As New ProjectRepository
   Return repo.Get

  End Function

 Public Shared Function GetProject(projectId As Int32) As Project
	
   Dim repo As New ProjectRepository
   Return repo.GetById(projectId)

 End Function

 Public Shared Function AddProject(ByRef project As ProjectBase, createdByUser As Integer) As Integer

   project.CreatedByUserID = createdByUser
   project.CreatedOnDate = Now
   project.LastModifiedByUserID = createdByUser
   project.LastModifiedOnDate = Now
   Dim repo As New ProjectBaseRepository
   repo.Insert(project)
   Return project.ProjectId

 End Function

 Public Shared Sub UpdateProject(project As ProjectBase, updatedByUser As Integer)
	
   project.LastModifiedByUserID = updatedByUser
   project.LastModifiedOnDate = Now
   Dim repo As New ProjectBaseRepository
   repo.Update(project)
	
 End Sub

 Public Shared Sub DeleteProject(project As ProjectBase)
	
   Dim repo As New ProjectBaseRepository
   repo.Delete(project)

 End Sub

 End Class
End Namespace


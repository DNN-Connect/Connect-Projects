Namespace Integration
 Public Class NotificationKey

  Public Id As String = ""
  Public ModuleId As Integer = -1
  Public ProjectId As Integer = -1

  Public Sub New(key As String)
   Dim keyParts() As String = key.Split(":"c)
   If keyParts.Length < 3 Then Exit Sub
   Id = keyParts(0)
   ModuleId = Integer.Parse(keyParts(1))
   ProjectId = Integer.Parse(keyParts(2))
  End Sub

  Public Sub New(id As String, moduleId As Integer, projectId As Integer)
   Me.Id = id
   Me.ModuleId = moduleId
   Me.ProjectId = projectId
  End Sub

  Public Shadows Function ToString() As String
   Return String.Format("{0}:{1}:{2}", Id, ModuleId, ProjectId)
  End Function

 End Class
End Namespace

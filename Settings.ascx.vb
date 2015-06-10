Imports System.IO
Imports Connect.DNN.Modules.Projects.Controllers.Projects
Imports Connect.DNN.Modules.Projects.Models.Projects
Imports DotNetNuke.Entities.Modules

Public Class Settings
 Inherits ModuleSettingsBase

 Private _settings As ModuleSettings
 Public Shadows Property Settings() As ModuleSettings
  Get
   If _settings Is Nothing Then
    _settings = Common.ModuleSettings.GetSettings(Me.ModuleContext.ModuleId)
   End If
   Return _settings
  End Get
  Set(ByVal Value As ModuleSettings)
   _settings = Value
  End Set
 End Property

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

 End Sub

 Public Overrides Sub LoadSettings()

  With Settings
   txtTnWidth.Text = .TnWidth.ToString
   txtTnHeight.Text = .TnHeight.ToString
   txtMedWidth.Text = .MedWidth.ToString
   txtMedHeight.Text = .MedHeight.ToString
   txtZoomWidth.Text = .ZoomWidth.ToString
   txtZoomHeight.Text = .ZoomHeight.ToString
   Try
    ddTnFit.Items.FindByValue(.TnFit).Selected = True
   Catch ex As Exception
   End Try
   Try
    ddMedFit.Items.FindByValue(.MedFit).Selected = True
   Catch ex As Exception
   End Try
   Try
    ddZoomFit.Items.FindByValue(.ZoomFit).Selected = True
   Catch ex As Exception
   End Try
  End With

 End Sub

 Public Overrides Sub UpdateSettings()

  With Settings
   .TnWidth = Integer.Parse(txtTnWidth.Text)
   .TnHeight = Integer.Parse(txtTnHeight.Text)
   .MedWidth = Integer.Parse(txtMedWidth.Text)
   .MedHeight = Integer.Parse(txtMedHeight.Text)
   .ZoomWidth = Integer.Parse(txtZoomWidth.Text)
   .ZoomHeight = Integer.Parse(txtZoomHeight.Text)
   .TnFit = ddTnFit.SelectedValue
   .MedFit = ddMedFit.SelectedValue
   .ZoomFit = ddZoomFit.SelectedValue
   .SaveSettings()
  End With

  ' Resize all pictures again
  Dim imageBasePath As String = String.Format("{0}Connect\Projects\{1}\", PortalSettings.HomeDirectoryMapPath, ModuleId)
  For Each p As Project In ProjectsController.GetProjects(ModuleId)
   Dim imagesPath As String = imageBasePath & p.ProjectId.ToString() & "\"
   Dim killList As New List(Of String)
   For Each f As String In Directory.GetFiles(imagesPath, "*_tn.*")
    killList.Add(f)
   Next
   For Each f As String In Directory.GetFiles(imagesPath, "*_med.*")
    killList.Add(f)
   Next
   For Each f As String In Directory.GetFiles(imagesPath, "*_zoom.*")
    killList.Add(f)
   Next
   For Each f As String In killList
    Try
     File.Delete(f)
    Catch ex As Exception
    End Try
   Next
   Dim r As New Resizer(Settings)
   For Each f As String In Directory.GetFiles(imagesPath, "*.*")
    Try
     If Not f.EndsWith(".xml") And Not f.EndsWith(".resources") Then
      r.Process(f)
     End If
    Catch ex As Exception
    End Try
   Next
  Next

 End Sub


End Class
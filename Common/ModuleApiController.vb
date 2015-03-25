Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Web.Api

Namespace Common
 Public Class ModuleApiController
  Inherits DnnApiController

  Private _security As ContextSecurity
  Public ReadOnly Property Security As ContextSecurity
   Get
    If _security Is Nothing Then
     _security = New ContextSecurity(ActiveModule, UserController.GetCurrentUserInfo())
    End If
    Return _security
   End Get
  End Property

  Private _settings As ModuleSettings
  Public Shadows Property Settings() As ModuleSettings
   Get
    If _settings Is Nothing Then
     _settings = ModuleSettings.GetSettings(ActiveModule.ModuleID)
    End If
    Return _settings
   End Get
   Set(ByVal Value As ModuleSettings)
    _settings = Value
   End Set
  End Property

  Private _imagePath As String
  Private ReadOnly Property ImagePath As String
   Get
    If String.IsNullOrEmpty(_imagePath) Then
     _imagePath = String.Format("{0}Connect/Projects/{1}/", PortalSettings.HomeDirectory, ActiveModule.ModuleID)
    End If
    Return _imagePath
   End Get
  End Property

  Private _imageMapPath As String
  Private ReadOnly Property ImageMapPath As String
   Get
    If String.IsNullOrEmpty(_imageMapPath) Then
     _imageMapPath = String.Format("{0}Connect\Projects\{1}\", PortalSettings.HomeDirectoryMapPath, ActiveModule.ModuleID)
    End If
    Return _imageMapPath
   End Get
  End Property

  Public Function GetImagePath(projectId As Integer) As String
   Return String.Format("{0}{1}/", ImagePath, projectId)
  End Function

  Public Function GetImageMapPath(projectId As Integer) As String
   Dim res As String = String.Format("{0}{1}\", ImageMapPath, projectId)
   If Not IO.Directory.Exists(res) Then IO.Directory.CreateDirectory(res)
   Return res
  End Function
 End Class
End Namespace
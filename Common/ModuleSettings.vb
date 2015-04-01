Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Utilities.DictionaryExtensions
Imports ModuleController = Connect.DNN.Modules.Projects.Integration.ModuleController

Namespace Common
 Public Class ModuleSettings

#Region " Properties "
  Private Property ModuleId As Integer = -1
  Private Property Settings As Hashtable
  Public Property TnWidth As Integer = 100
  Public Property TnHeight As Integer = 100
  Public Property MedWidth As Integer = 480
  Public Property MedHeight As Integer = 234
  Public Property ZoomWidth As Integer = 1920
  Public Property ZoomHeight As Integer = 1080
  Public Property TnFit As String = "Crop"
  Public Property MedFit As String = "Shrink"
  Public Property ZoomFit As String = "Shrink"
#End Region

#Region " Constructors "
  Public Sub New(moduleId As Integer)

   Me.ModuleId = moduleId
   Settings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(moduleId)
   Settings.ReadValue("TnWidth", TnWidth)
   Settings.ReadValue("TnHeight", TnHeight)
   Settings.ReadValue("MedWidth", MedWidth)
   Settings.ReadValue("MedHeight", MedHeight)
   Settings.ReadValue("ZoomWidth", ZoomWidth)
   Settings.ReadValue("ZoomHeight", ZoomHeight)
   Settings.ReadValue("TnFit", TnFit)
   Settings.ReadValue("MedFit", MedFit)
   Settings.ReadValue("ZoomFit", ZoomFit)

  End Sub
#End Region

#Region " Public Members "
  Public Sub SaveSettings()

   Dim objModules As New ModuleController
   objModules.UpdateModuleSetting(ModuleId, "TnWidth", Me.TnWidth.ToString)
   objModules.UpdateModuleSetting(ModuleId, "TnHeight", Me.TnHeight.ToString)
   objModules.UpdateModuleSetting(ModuleId, "MedWidth", Me.MedWidth.ToString)
   objModules.UpdateModuleSetting(ModuleId, "MedHeight", Me.MedHeight.ToString)
   objModules.UpdateModuleSetting(ModuleId, "ZoomWidth", Me.ZoomWidth.ToString)
   objModules.UpdateModuleSetting(ModuleId, "ZoomHeight", Me.ZoomHeight.ToString)
   objModules.UpdateModuleSetting(ModuleId, "TnFit", Me.TnFit)
   objModules.UpdateModuleSetting(ModuleId, "MedFit", Me.MedFit)
   objModules.UpdateModuleSetting(ModuleId, "ZoomFit", Me.ZoomFit)
   DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(ModuleId), Me)

  End Sub

  Public Shared Function GetSettings(moduleId As Integer) As ModuleSettings

   Dim res As ModuleSettings = Nothing
   Try
    res = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(CacheKey(moduleId)), ModuleSettings)
   Catch ex As Exception
   End Try
   If res Is Nothing Then
    res = New ModuleSettings(moduleId)
    DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(moduleId), res)
   End If
   Return res

  End Function

  Public Shared Function CacheKey(moduleId As Integer) As String
   Return String.Format("SettingsModule{0}", moduleId)
  End Function
#End Region

 End Class
End Namespace
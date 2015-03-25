Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Utilities.DictionaryExtensions

Namespace Common
 Public Class ModuleSettings

#Region " Properties "
  Private Property ModuleId As Integer = -1
  Private Property Settings As Hashtable
  Public Property Width As Integer = 80
  Public Property Height As Integer = 80
  Public Property ZoomWidth As Integer = 400
  Public Property ZoomHeight As Integer = 300
  Public Property FitType As String = "Crop"
  Public Property ZoomFitType As String = "Shrink"
#End Region

#Region " Constructors "
  Public Sub New(moduleId As Integer)

   Me.ModuleId = moduleId
   Settings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(moduleId)
   Width = Settings.GetValue(Of Integer)("Width", Width)
   Height = Settings.GetValue(Of Integer)("Height", Height)
   ZoomWidth = Settings.GetValue(Of Integer)("ZoomWidth", ZoomWidth)
   ZoomHeight = Settings.GetValue(Of Integer)("ZoomHeight", ZoomHeight)
   FitType = Settings.GetValue(Of String)("FitType", FitType)
   ZoomFitType = Settings.GetValue(Of String)("ZoomFitType", ZoomFitType)

  End Sub
#End Region

#Region " Public Members "
  Public Sub SaveSettings()

   Dim objModules As New ModuleController
   objModules.UpdateModuleSetting(ModuleId, "Width", Me.Width.ToString)
   objModules.UpdateModuleSetting(ModuleId, "Height", Me.Height.ToString)
   objModules.UpdateModuleSetting(ModuleId, "ZoomWidth", Me.ZoomWidth.ToString)
   objModules.UpdateModuleSetting(ModuleId, "ZoomHeight", Me.ZoomHeight.ToString)
   objModules.UpdateModuleSetting(ModuleId, "FitType", Me.FitType)
   objModules.UpdateModuleSetting(ModuleId, "ZoomFitType", Me.ZoomFitType)
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
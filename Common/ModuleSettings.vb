Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Common.Utilities.DictionaryExtensions
Imports ModuleController = Connect.DNN.Modules.Projects.Controllers.ModuleController

Namespace Common
 Public Class ModuleSettings

#Region " Properties "
  Private Property ModuleId As Integer = -1
  Private Property Settings As Hashtable
#End Region

#Region " Constructors "
  Public Sub New(moduleId As Integer)

   _ModuleId = moduleId
   _Settings = (New DotNetNuke.Entities.Modules.ModuleController).GetModuleSettings(moduleId)
   ' Property = _Settings.GetValue(Of Integer)("Property", Property)

  End Sub
#End Region

#Region " Public Members "
  Public Sub SaveSettings(portalHomeDirMapPath As String, moduleId As Integer)

   Dim objModules As New ModuleController
   ' objModules.UpdateModuleSetting(ModuleId, "Property", Me.Property.ToString)
   DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey(moduleId), Me)

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
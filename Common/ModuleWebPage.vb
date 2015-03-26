Imports DotNetNuke.Web.Razor
Imports System.Web.WebPages
Imports System.Linq
Imports DotNetNuke.Entities.Users

Namespace Common
 Public MustInherit Class ModuleWebPage
  Inherits DotNetNukeWebPage

  Public Function GetModuleUrl(relativeUrl As String) As String
   Return DotNetNuke.Common.ResolveUrl("~/DesktopModules/Connect/Projects/" & relativeUrl)
  End Function

  Public Function EditUrl(controlKey As String, ParamArray additionParameters() As String) As String
   additionParameters.Add("mid=" & Dnn.Module.ModuleID.ToString())
   Return DotNetNuke.Common.NavigateURL(Dnn.Tab.TabID, controlKey, additionParameters)
  End Function

  Protected Overrides Sub ConfigurePage(ByVal parentPage As WebPageBase)
   MyBase.ConfigurePage(parentPage)
   Context = parentPage.Context
  End Sub

  Private _security As ContextSecurity
  Public ReadOnly Property Security As ContextSecurity
   Get
    If _security Is Nothing Then
     _security = New ContextSecurity(Dnn.Module, UserController.GetCurrentUserInfo())
    End If
    Return _security
   End Get
  End Property

  Private _settings As ModuleSettings
  Public Property Settings() As ModuleSettings
   Get
    If _settings Is Nothing Then
     _settings = ModuleSettings.GetSettings(Dnn.Module.ModuleID)
    End If
    Return _settings
   End Get
   Set(ByVal Value As ModuleSettings)
    _settings = Value
   End Set
  End Property
 End Class

 Public MustInherit Class ModuleWebPage(Of T)
  Inherits DotNetNukeWebPage(Of T)

  Public Function GetModuleUrl(relativeUrl As String) As String
   Return DotNetNuke.Common.ResolveUrl("~/DesktopModules/Connect/Projects/" & relativeUrl)
  End Function

  Public Function EditUrl(controlKey As String, ParamArray additionParameters() As String) As String
   additionParameters.Add("mid=" & Dnn.Module.ModuleID.ToString())
   Return DotNetNuke.Common.NavigateURL(Dnn.Tab.TabID, controlKey, additionParameters)
  End Function

  Private _security As ContextSecurity
  Public ReadOnly Property Security As ContextSecurity
   Get
    If _security Is Nothing Then
     _security = New ContextSecurity(Dnn.Module, UserController.GetCurrentUserInfo())
    End If
    Return _security
   End Get
  End Property

  Private _settings As ModuleSettings
  Public Property Settings() As ModuleSettings
   Get
    If _settings Is Nothing Then
     _settings = ModuleSettings.GetSettings(Dnn.Module.ModuleID)
    End If
    Return _settings
   End Get
   Set(ByVal Value As ModuleSettings)
    _settings = Value
   End Set
  End Property
 End Class

End Namespace
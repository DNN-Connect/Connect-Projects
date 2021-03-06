Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Framework
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.UI.Utilities
Imports DotNetNuke.Web.Client
Imports DotNetNuke.Web.Client.ClientResourceManagement
Imports DotNetNuke.Web.Razor

Namespace Common
 Public Class ModuleBase
  Inherits RazorModuleBase

#Region " Properties "
  Private _security As ContextSecurity
  Public ReadOnly Property Security As ContextSecurity
   Get
    If _security Is Nothing Then
     _security = New ContextSecurity(ModuleContext.Configuration, UserController.GetCurrentUserInfo())
    End If
    Return _security
   End Get
  End Property

  Private _settings As ModuleSettings
  Public Shadows Property Settings() As ModuleSettings
   Get
    If _settings Is Nothing Then
     _settings = ModuleSettings.GetSettings(Me.ModuleContext.ModuleId)
    End If
    Return _settings
   End Get
   Set(ByVal Value As ModuleSettings)
    _settings = Value
   End Set
  End Property
#End Region

#Region " Event Handlers "
  Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
  End Sub
#End Region

#Region " Public Methods "
  Public Sub AddService()

   If Context.Items("ServiceAdded") Is Nothing Then

    ' Announce at DNN
    jQuery.RequestRegistration()
    jQuery.RequestDnnPluginsRegistration()
    jQuery.RequestUIRegistration()
    ServicesFramework.Instance.RequestAjaxScriptSupport()
    ServicesFramework.Instance.RequestAjaxAntiForgerySupport()

    ' Scripts
    AddJavascriptFile("angular.min.js", 50)
    AddJavascriptFile("angular-route.min.js", 51)
    AddJavascriptFile("connect.projects.js", 70)
    AddJavascriptFile("stupidtable.min.js", 70)
    AddJavascriptFile("es5-shim.min.js", 70)
    AddJavascriptFile("es5-sham.min.js", 70)
    AddJavascriptFile("angular-file-upload.min.js", 70)
    AddJavascriptFile("jquery.colorbox.js", 70)
    AddJavascriptFile("ng-tags-input.min.js", 70)

    ' Css
    AddCssFile("colorbox.css")
    AddCssFile("ng-tags-input.css")

    Context.Items("ServiceAdded") = True
   End If

  End Sub

  Public Sub AddJavascriptFile(jsFilename As String, priority As Integer)
   ClientResourceManager.RegisterScript(Page, ResolveUrl("~/DesktopModules/Connect/Projects/js/" & jsFilename), priority)
  End Sub

  Public Sub AddCssFile(cssFilename As String)
   ClientResourceManager.RegisterStyleSheet(Page, ResolveUrl("~/DesktopModules/Connect/Projects/css/" & cssFilename), FileOrder.Css.ModuleCss)
  End Sub

  Public Function LocalizeJSString(resourceKey As String) As String
   Return ClientAPI.GetSafeJSString(LocalizeString(resourceKey))
  End Function

  Public Function LocalizeJSString(resourceKey As String, resourceFile As String) As String
   Return ClientAPI.GetSafeJSString(Localization.GetString(resourceKey, resourceFile))
  End Function
#End Region

 End Class
End Namespace
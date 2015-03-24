Imports Connect.DNN.Modules.Projects.Common
Imports Connect.DNN.Modules.Projects.Common.Globals

Public Class ModuleHome
 Inherits ModuleBase

 Public Property View As String = "Home"
 Public Property ModuleControl As String = ""

 Protected Overrides ReadOnly Property RazorScriptFile As String
  Get
   If ModuleControl = "" Then
    Return String.Format("~/DesktopModules/Connect/Projects/Views/{0}.vbhtml", View)
   Else
    Return String.Format("~/DesktopModules/Connect/Projects/Views/{0}/{1}.vbhtml", ModuleControl, View)
   End If
  End Get
 End Property

 Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
  AddService()
  LocalResourceFile = SharedResourceFileName
 End Sub

 Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

  If Not Me.IsPostBack Then

  End If

 End Sub

End Class

Imports System.IO
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.UI.Modules
Imports DotNetNuke.Web.Razor

Namespace Common
 Public Class RazorControl

  Public Property RazorFile As String = ""
  Public Property DnnModuleContext As New ModuleInstanceContext
  Public Property LocalResourceFile As String = ""

  Public Sub New(context As ModuleInfo, razorFile As String, localResourceFile As String)
   MyBase.New()
   Me.DnnModuleContext.Configuration = context
   Me.RazorFile = razorFile
   Me.LocalResourceFile = localResourceFile
  End Sub

  Private _engine As RazorEngine
  Public ReadOnly Property Engine As RazorEngine
   Get
    If _engine Is Nothing Then
     _engine = New RazorEngine(RazorFile, DnnModuleContext, LocalResourceFile)
    End If
    Return _engine
   End Get
  End Property

  Public Function RenderObject() As String

   Using tw As New StringWriter()
    Engine.Render(tw)
    Return tw.ToString
   End Using

  End Function

  Public Function RenderObject(Of T)(model As T) As String

   Using tw As New StringWriter()
    Engine.Render(Of T)(tw, model)
    Return tw.ToString
   End Using

  End Function

 End Class
End Namespace
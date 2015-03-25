Imports System.Runtime.CompilerServices

Namespace Common
 Public Module Globals
  Public Const SharedResourceFileName As String = "~/DesktopModules/Connect/Projects/App_LocalResources/SharedResources.resx"

  <Extension()>
  Public Sub Add(Of T)(ByRef arr As T(), item As T)
   Array.Resize(arr, arr.Length + 1)
   arr(arr.Length - 1) = item
  End Sub

 End Module
End Namespace
Imports System.Globalization
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens

Namespace Common

 <Serializable()>
 <DataContract>
 Public Class Image
  Implements IPropertyAccess

  <XmlElement("order")>
  <DataMember()>
  Public Property Order As Integer = 0

  <XmlElement("file")>
  <DataMember()>
  Public Property File As String

  <XmlElement("extension")>
  <DataMember()>
  Public Property Extension As String

  <XmlElement("title")>
  <DataMember()>
  Public Property Title As String

  <XmlElement("remarks")>
  <DataMember()>
  Public Property Remarks As String

  Public Sub New()
  End Sub
  Public Sub New(filepath As String, title As String, remarks As String)
   Me.File = Path.GetFileNameWithoutExtension(filepath)
   Me.Extension = Path.GetExtension(filepath)
   Me.Title = title
   Me.Remarks = remarks
  End Sub

#Region " IPropertyAccess "
  Public ReadOnly Property Cacheability As CacheLevel Implements IPropertyAccess.Cacheability
   Get
    Return CacheLevel.fullyCacheable
   End Get
  End Property

  Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As CultureInfo, AccessingUser As UserInfo, AccessLevel As Scope, ByRef PropertyNotFound As Boolean) As String Implements IPropertyAccess.GetProperty
   Dim OutputFormat As String = String.Empty
   If strFormat = String.Empty Then
    OutputFormat = "D"
   Else
    OutputFormat = strFormat
   End If
   Select Case strPropertyName.ToLower
    Case "order"
     Return (Me.Order.ToString(OutputFormat, formatProvider))
    Case "file"
     Return PropertyAccess.FormatString(Me.File, strFormat)
    Case "extension"
     Return PropertyAccess.FormatString(Me.Extension, strFormat)
    Case "title"
     Return PropertyAccess.FormatString(Me.Title, strFormat)
    Case "remarks"
     Return PropertyAccess.FormatString(Me.Remarks, strFormat)
    Case Else
     Return ""
   End Select
  End Function
#End Region

 End Class
End Namespace
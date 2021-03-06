Imports System
Imports System.Data
Imports System.Globalization

Imports DotNetNuke.ComponentModel.DataAnnotations
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens

Namespace Models.ProjectTypes
 Partial Public Class ProjectTypeBase
  Implements IHydratable
  Implements IPropertyAccess

#Region " IHydratable Methods "
  Public Overridable Sub Fill(dr As IDataReader) Implements IHydratable.Fill


   ProjectId = Convert.ToInt32(Null.SetNull(dr.Item("ProjectId"), ProjectId))
   TypeId = Convert.ToInt32(Null.SetNull(dr.Item("TypeId"), TypeId))

  End Sub

  <IgnoreColumn()>
  Public Property KeyID() As Integer Implements IHydratable.KeyID
   Get
    Return Nothing
   End Get
   Set(value As Integer)
   End Set
  End Property
#End Region

#Region " IPropertyAccess Methods "
  Public Overridable Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As CultureInfo, accessingUser As UserInfo, accessLevel As Scope, ByRef propertyNotFound As Boolean) As String Implements IPropertyAccess.GetProperty
   Dim outputFormat As String = strFormat
   If strFormat = String.Empty Then
    outputFormat = "D"
   End If
   Select Case strPropertyName.ToLower
    Case "projectid"
     Return ProjectId.ToString(outputFormat, formatProvider)
    Case "typeid"
     Return TypeId.ToString(outputFormat, formatProvider)
    Case Else
     propertyNotFound = True
   End Select

   Return Null.NullString
  End Function

  <IgnoreColumn()>
  Public ReadOnly Property Cacheability() As CacheLevel Implements IPropertyAccess.Cacheability
   Get
    Return CacheLevel.fullyCacheable
   End Get
  End Property
#End Region

 End Class
End Namespace

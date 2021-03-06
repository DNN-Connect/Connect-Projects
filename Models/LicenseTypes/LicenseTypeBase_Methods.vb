Imports System
Imports System.Data
Imports System.Globalization

Imports DotNetNuke.ComponentModel.DataAnnotations
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens

Namespace Models.LicenseTypes
 Partial Public Class LicenseTypeBase
  Implements IHydratable
  Implements IPropertyAccess

#Region " IHydratable Methods "
  Public Overridable Sub Fill(dr As IDataReader) Implements IHydratable.Fill


   LicenseTypeId = Convert.ToInt32(Null.SetNull(dr.Item("LicenseTypeId"), LicenseTypeId))
   ProjectColor = Convert.ToString(Null.SetNull(dr.Item("ProjectColor"), ProjectColor))
   TypeDescription = Convert.ToString(Null.SetNull(dr.Item("TypeDescription"), TypeDescription))

  End Sub

  <IgnoreColumn()>
  Public Property KeyID() As Integer Implements IHydratable.KeyID
   Get
    Return LicenseTypeId
   End Get
   Set(value As Integer)
    LicenseTypeId = value
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
    Case "licensetypeid"
     Return LicenseTypeId.ToString(outputFormat, formatProvider)
    Case "projectcolor"
     Return PropertyAccess.FormatString(ProjectColor, strFormat)
    Case "typedescription"
     Return PropertyAccess.FormatString(TypeDescription, strFormat)
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

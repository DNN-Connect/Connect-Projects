Imports System
Imports System.Data
Imports System.Globalization
Imports System.Xml.Serialization

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens

Namespace Models.ProjectTags

 <Serializable(), XmlRoot("ProjectTag")>
 Partial Public Class ProjectTag

#Region " IHydratable Implementation "
  Public Overrides Sub Fill(dr As IDataReader)
   MyBase.Fill(dr)
   VocabularyID = Convert.ToInt32(Null.SetNull(dr.Item("VocabularyID"), VocabularyID))
   ParentTermID = Convert.ToInt32(Null.SetNull(dr.Item("ParentTermID"), ParentTermID))
   Name = Convert.ToString(Null.SetNull(dr.Item("Name"), Name))
   Description = Convert.ToString(Null.SetNull(dr.Item("Description"), Description))
   Weight = Convert.ToInt32(Null.SetNull(dr.Item("Weight"), Weight))
   TermLeft = Convert.ToInt32(Null.SetNull(dr.Item("TermLeft"), TermLeft))
   TermRight = Convert.ToInt32(Null.SetNull(dr.Item("TermRight"), TermRight))
   CreatedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("CreatedByUserID"), CreatedByUserID))
   CreatedOnDate = CDate(Null.SetNull(dr.Item("CreatedOnDate"), CreatedOnDate))
   LastModifiedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("LastModifiedByUserID"), LastModifiedByUserID))
   LastModifiedOnDate = CDate(Null.SetNull(dr.Item("LastModifiedOnDate"), LastModifiedOnDate))
  End Sub
#End Region

#Region " IPropertyAccess Implementation "
  Public Overrides Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As CultureInfo, accessingUser As UserInfo, accessLevel As Scope, ByRef propertyNotFound As Boolean) As String
   Dim outputFormat As String = strFormat
   If strFormat = String.Empty Then
    outputFormat = "D"
   End If
   Select Case strPropertyName.ToLower
    Case "vocabularyid"
     Return (VocabularyID.ToString(outputFormat, formatProvider))
    Case "parenttermid"
     Return (ParentTermID.ToString(outputFormat, formatProvider))
    Case "name"
     Return PropertyAccess.FormatString(Name, strFormat)
    Case "description"
     Return PropertyAccess.FormatString(Description, strFormat)
    Case "weight"
     Return (Weight.ToString(outputFormat, formatProvider))
    Case "termleft"
     Return (TermLeft.ToString(outputFormat, formatProvider))
    Case "termright"
     Return (TermRight.ToString(outputFormat, formatProvider))
    Case "createdbyuserid"
     If CreatedByUserID Is Nothing Then Return ""
     Return CType(CreatedByUserID, Int32).ToString(outputFormat, formatProvider)
    Case "createdondate"
     Return (CreatedOnDate.ToString(outputFormat, formatProvider))
    Case "lastmodifiedbyuserid"
     If LastModifiedByUserID Is Nothing Then Return ""
     Return CType(LastModifiedByUserID, Int32).ToString(outputFormat, formatProvider)
    Case "lastmodifiedondate"
     Return (LastModifiedOnDate.ToString(outputFormat, formatProvider))
    Case Else
     Return MyBase.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, propertyNotFound)
   End Select

  End Function
#End Region

 End Class
End Namespace



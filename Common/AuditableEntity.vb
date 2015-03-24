Imports System.Runtime.Serialization
Imports DotNetNuke.Common.Utilities

Namespace Common

 Public MustInherit Class AuditableEntity

  Public Sub FillAuditFields(dr As IDataReader)
   CreatedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("CreatedByUserID"), CreatedByUserID))
   CreatedOnDate = CDate(Null.SetNull(dr.Item("CreatedOnDate"), CreatedOnDate))
   LastModifiedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("LastModifiedByUserID"), LastModifiedByUserID))
   LastModifiedOnDate = CDate(Null.SetNull(dr.Item("LastModifiedOnDate"), LastModifiedOnDate))
  End Sub

#Region " Public Properties "
  <DataMember()>
  Public Property CreatedByUserID As Int32 = -1
  <DataMember()>
  Public Property CreatedOnDate As Date
  <DataMember()>
  Public Property LastModifiedByUserID As Int32 = -1
  <DataMember()>
  Public Property LastModifiedOnDate As Date
#End Region


 End Class
End Namespace
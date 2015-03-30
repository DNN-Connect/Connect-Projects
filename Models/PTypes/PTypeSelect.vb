Imports System.Runtime.Serialization
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.ComponentModel.DataAnnotations

Namespace Models.PTypes

 <DataContract()>
 Public Class PTypeSelect
  Inherits PTypeBase

  Public Property Selected As Integer = 0

  <DataMember()>
  <IgnoreColumn()>
  Public Property IsSelected As Boolean
   Get
    Return CBool(Selected > 0)
   End Get
   Set(value As Boolean)
    Selected = 1
   End Set
  End Property

  Public Overrides Sub Fill(dr As IDataReader)
   MyBase.Fill(dr)
   Selected = Convert.ToInt32(Null.SetNull(dr.Item("Selected"), Selected))
  End Sub

 End Class
End Namespace
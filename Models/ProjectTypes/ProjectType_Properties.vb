
Imports System.Runtime.Serialization
Imports DotNetNuke.ComponentModel.DataAnnotations

Namespace Models.ProjectTypes

  <TableName("vw_Connect_Projects_ProjectTypes")>  <DataContract()>
  Partial Public Class ProjectType
  Inherits ProjectTypeBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
   MyBase.New()
  End Sub
#End Region

#Region " Public Properties "
  <DataMember()>
  Public Property ProjectName As String = ""
  <DataMember()>
  Public Property Visible As Boolean = False
  <DataMember()>
  Public Property TypeDescription As String = ""
  <DataMember()>
  Public Property TypeIcon As String 
#End Region

#Region " Public Methods "
  Public Function GetProjectTypeBase() As ProjectTypeBase
   Dim base As New ProjectTypeBase
   base.ProjectId = ProjectId
   base.TypeId = TypeId
   Return base
  End Function
#End Region

 End Class
End Namespace



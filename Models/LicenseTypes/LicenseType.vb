Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace Models.LicenseTypes

  <TableName("Connect_Projects_LicenseTypes")>
  <PrimaryKey("LicenseTypeId", AutoIncrement:=True)>
  <DataContract()>
  Partial Public Class LicenseType
  Inherits LicenseTypeBase

#Region " Private Members "
#End Region
	
#Region " Constructors "
  Public Sub New()
   MyBase.New()
  End Sub
#End Region

#Region " Public Methods "
  Public Function GetLicenseTypeBase() As LicenseTypeBase
   Dim base As New LicenseTypeBase
   base.LicenseTypeId = LicenseTypeId
   base.ProjectColor = ProjectColor
   base.TypeDescription = TypeDescription
   Return base
  End Function
#End Region

 End Class
End Namespace


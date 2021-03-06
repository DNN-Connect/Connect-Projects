Imports System
Imports Connect.DNN.Modules.Projects.Repositories
Imports Connect.DNN.Modules.Projects.Models.LicenseTypes

Namespace Controllers.LicenseTypes

 Partial Public Class LicenseTypesController

  Public Shared Function GetLicenseTypes() As IEnumerable(Of LicenseType)

   Dim repo As New LicenseTypeRepository
   Return repo.Get()

  End Function

  Public Shared Function GetLicenseType(licenseTypeId As Int32) As LicenseType

   Dim repo As New LicenseTypeRepository
   Return repo.GetById(licenseTypeId)

  End Function

  Public Shared Function AddLicenseType(ByRef licensetype As LicenseTypeBase) As Integer

   Dim repo As New LicenseTypeBaseRepository
   repo.Insert(licensetype)
   Return licensetype.LicenseTypeId

  End Function

  Public Shared Sub UpdateLicenseType(licensetype As LicenseTypeBase)

   Dim repo As New LicenseTypeBaseRepository
   repo.Update(licensetype)

  End Sub

  Public Shared Sub DeleteLicenseType(licensetype As LicenseTypeBase)

   Dim repo As New LicenseTypeBaseRepository
   repo.Delete(licensetype)

  End Sub

 End Class
End Namespace


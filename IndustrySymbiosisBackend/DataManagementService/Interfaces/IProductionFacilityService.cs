using System;
namespace DataManagementService.Interfaces
{
    public interface IProductionFacilityService
    {
        string Get(int enterpriseId);
        string Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city);
        int Update(int? enterpriseId, int productionFacilityId, int? postAddressId, string? facilityName, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city);
    }
}

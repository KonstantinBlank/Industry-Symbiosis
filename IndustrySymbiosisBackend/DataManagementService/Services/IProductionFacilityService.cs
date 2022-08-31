using System;
namespace DataManagementService.Services
{
    public interface IProductionFacilityService
    {
        string GetAsJSONStringByEnterpriseId(int enterpriseId);
        string Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city);
        string Update(int productionFacilityId, int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city);
    }
}

namespace DataManagementService.Interfaces
{
    public interface IProductionFacilityService
    {
        string Get(int enterpriseId);
        string Create(int enterpriseId, string name, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city);
        int Update(int id, int postAddressId, string? name, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city);
    }
}

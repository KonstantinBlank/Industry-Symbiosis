using System;
namespace EnterpriseManagement.Interfaces
{
    public interface IEnterpriseService
    {
        public string GetAll();
        public string GetById(int enterpriseId);
        public string Create(string name, string addressRecord1, string addressRecord2, string street, string house_number, string postcode, string city);
        public int Update(int enterpriseId, int addressId, string? name, string? addressRecord1, string? addressRecord2, string? street, string? house_number, string? postcode, string? city);
    }
}

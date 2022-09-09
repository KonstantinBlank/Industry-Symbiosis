using System;
using System.Collections.Generic;
using DataManagementService.Services;
using EnterpriseManagement.Interfaces;
using EnterpriseManagementService.Data;
using Newtonsoft.Json;

namespace EnterpriseManagement.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        public EnterpriseService()
        {
        }

        public string Create(string name, string addressRecord1, string addressRecord2, string street, string houseNumber, string postcode, string city)
        {
            Enterprise enterprise = new Enterprise(name, new Address(addressRecord1, addressRecord2, street, houseNumber, postcode, city));
            Address address = enterprise.Address;

            string addressQuery = @"INSERT INTO post_address
			                            (address_record_1,
                                        address_record_2,
			                            street,
                                        house_number,
                                        postcode,
                                        city)
                                    VALUES
                                        (@addressRecord1,
                                        @addressRecord2,
		                                @street,
		                                @houseNumber,
		                                @postcode,
                                        @city);
                                    SELECT SCOPE_IDENTITY()";

            IDictionary<string, object> parameterPairsAddress = new Dictionary<string, object>();
            parameterPairsAddress.Add(new KeyValuePair<string, object>("addressRecord1", address.PostAddressRecord1));
            parameterPairsAddress.Add(new KeyValuePair<string, object>("addressRecord2", address.PostAddressRecord2));
            parameterPairsAddress.Add(new KeyValuePair<string, object>("street", address.Street));
            parameterPairsAddress.Add(new KeyValuePair<string, object>("houseNumber", address.HouseNumber));
            parameterPairsAddress.Add(new KeyValuePair<string, object>("postcode", address.Postcode));
            parameterPairsAddress.Add(new KeyValuePair<string, object>("city", address.City));
            
            int addressId = SqlConnectionHelper.CreateEntry(addressQuery, parameterPairsAddress);
            address.SetId(addressId);
            
            string enterpriseQuery = @"INSERT INTO enterprise
                                           (name,
                                           fk_address)
                                       VALUES
		                                   (@name,
		                                    @fkAddress);
                                       SELECT SCOPE_IDENTITY()";

            IDictionary<string, object> parameterPairsEnterprise = new Dictionary<string, object>();
            parameterPairsEnterprise.Add(new KeyValuePair<string, object>("name", enterprise.Name));
            parameterPairsEnterprise.Add(new KeyValuePair<string, object>("fkAddress", enterprise.Address.Id));
            int enterpriseId = SqlConnectionHelper.CreateEntry(enterpriseQuery, parameterPairsEnterprise);
            enterprise.SetId(enterpriseId);

            Console.WriteLine("address and enterprise were successfully created.");

            return JsonConvert.SerializeObject(enterprise);
        }

        public string GetAll()
        {
            string query = @"SELECT e.id as enterpriseId, e.name, p.id as addressId, p.address_record_1, p.address_record_2, p.street, p.house_number, p.postcode, p.city
                             FROM enterprise as e
                             LEFT JOIN post_address as p
                             ON e.fk_address = p.id;";
            string enterprises = SqlConnectionHelper.GetTable(query);

            return enterprises;
        }

        public string GetById(int enterpriseId)
        {
            string query = $@"SELECT e.id as enterpriseId, e.name, p.id as addressId, p.address_record_1, p.address_record_2, p.street, p.house_number, p.postcode, p.city
                                  FROM enterprise as e
                                  LEFT JOIN post_address as p
                                  ON e.fk_address = p.id
                                  WHERE e.id = {enterpriseId}";

            string enterprise = SqlConnectionHelper.GetTable(query);

            return enterprise;
        }

        public int Update(int enterpriseId, int addressId, string? name, string? addressRecord1, string? addressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            int updatedRows = 0;

            //update enterprise entry
            Enterprise enterprise = new Enterprise(enterpriseId, name, new Address(addressId, addressRecord1, addressRecord2, street, houseNumber, postcode, city));

            IDictionary<string, object?> parameterPairsEnterprise = new Dictionary<string, object?>();
            parameterPairsEnterprise.Add(new KeyValuePair<string, object?>("name", enterprise.Name));

            updatedRows += SqlConnectionHelper.UpdateEntry("enterprise", enterprise.Id.ToString(), parameterPairsEnterprise);

            //update address entry
            Address address = enterprise.Address;

            IDictionary<string, object?> parameterPairsAddress = new Dictionary<string, object?>();
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("address_record_1", address.PostAddressRecord1));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("address_record_2", address.PostAddressRecord2));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("street", address.Street));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("house_number", address.HouseNumber));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("postcode", address.Postcode));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("city", address.City));

            updatedRows += SqlConnectionHelper.UpdateEntry("post_address", enterprise.Address.Id.ToString(), parameterPairsAddress);

            return updatedRows;
        }
    }
}

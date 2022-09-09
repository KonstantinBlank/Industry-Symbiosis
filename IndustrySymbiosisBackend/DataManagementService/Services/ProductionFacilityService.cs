using System;
using Newtonsoft.Json;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using System.Collections.Generic;

namespace DataManagementService.Services
{
    public class ProductionFacilityService : IProductionFacilityService
    {
        public ProductionFacilityService()
        {
        }

        public string Get(int enterpriseId)
        {
            string query = @$"SELECT * 
                       FROM production_facility_view
                       WHERE enterprise_id = {enterpriseId};";
            string productionFacilities = SqlConnectionHelper.GetTable(query);

            return productionFacilities;
        }

        public string Create(int enterpriseId, string name, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            Address address = new Address(postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);
            ProductionFacility productionFacility = new ProductionFacility(name, address, enterpriseId);

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

            string enterpriseQuery = @"INSERT INTO production_facility
                                           (name,
                                           fk_address)
                                       VALUES
		                                   (@name,
		                                    @fk_enterprise,
                                            @fk_post_address);
                                       SELECT SCOPE_IDENTITY()";

            IDictionary<string, object> parameterPairsEnterprise = new Dictionary<string, object>();
            parameterPairsEnterprise.Add(new KeyValuePair<string, object>("name", productionFacility.Name));
            parameterPairsEnterprise.Add(new KeyValuePair<string, object>("fk_enterprise", productionFacility.EnterpriseId));
            parameterPairsEnterprise.Add(new KeyValuePair<string, object>("fk_post_address", productionFacility.Address.Id));
            int productionFacilityId = SqlConnectionHelper.CreateEntry(enterpriseQuery, parameterPairsEnterprise);
            productionFacility.Address.SetId(productionFacilityId);

            Console.WriteLine("address and production facility were successfully created.");

            return JsonConvert.SerializeObject(productionFacility);
        }

        public int Update(int id, int postAddressId, string? name, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            int updatedRows = 0;
            Address address = new Address(postAddressId,postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);
            ProductionFacility productionFacility = new ProductionFacility(id, name, address);


            //update production faciltiy entry
            IDictionary<string, object?> parameterPairsProductionFacility = new Dictionary<string, object?>();
            parameterPairsProductionFacility.Add(new KeyValuePair<string, object?>("name", productionFacility.Name));
            updatedRows += SqlConnectionHelper.UpdateEntry("production_facility", productionFacility.Id.ToString(), parameterPairsProductionFacility);

            //update address entry
            IDictionary<string, object?> parameterPairsAddress = new Dictionary<string, object?>();
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("address_record_1", address.PostAddressRecord1));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("address_record_2", address.PostAddressRecord2));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("street", address.Street));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("house_number", address.HouseNumber));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("postcode", address.Postcode));
            parameterPairsAddress.Add(new KeyValuePair<string, object?>("city", address.City));

            updatedRows += SqlConnectionHelper.UpdateEntry("post_address", address.Id.ToString(), parameterPairsAddress);

            return updatedRows;
        }

        
    }
}

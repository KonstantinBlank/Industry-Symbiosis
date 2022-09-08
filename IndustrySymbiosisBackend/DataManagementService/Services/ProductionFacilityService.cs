using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using DataManagementService.Services;

namespace DataManagementService.Services
{
    public class ProductionFacilityService : IProductionFacilityService
    {
        public ProductionFacilityService()
        {
        }

        public string Get(int enterpriseId)
        {
            string productionFacilities = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT * 
                                 FROM production_facility_view
                                 WHERE enterprise_id = {enterpriseId};";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        productionFacilities = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return productionFacilities;
        }

        public string Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            ProductionFacility productionFacility = new ProductionFacility(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                // address
                queryString = @"INSERT INTO post_address
			                            (address_record_1,
                                        address_record_2,
			                            street,
                                        house_number,
                                        postcode,
                                        city)
                                    VALUES
                                        (@postAddressRecord1,
                                        @postAddressRecord2,
		                                @street,
		                                @houseNumber,
		                                @postcode,
                                        @city);
                                    SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@postAddressRecord1", productionFacility.PostAddressRecord1);
                    command.Parameters.AddWithValue("@postAddressRecord2", productionFacility.PostAddressRecord2);
                    command.Parameters.AddWithValue("@street", productionFacility.Street);
                    command.Parameters.AddWithValue("@houseNumber", productionFacility.HouseNumber);
                    command.Parameters.AddWithValue("@postcode", productionFacility.Postcode);
                    command.Parameters.AddWithValue("@city", productionFacility.City);
                    command.Connection.Open();

                    int postAddressId = Convert.ToInt32(command.ExecuteScalar());
                    productionFacility.SetPostAddressId(postAddressId);
                }

                // production facility
                queryString = @"INSERT INTO production_facility
                                        (name,
                                        fk_enterprise,
                                        fk_post_address)
                                    VALUES
		                                (@name,
                                        @fk_enterprise,
		                                @fk_post_address);
                                    SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@name", productionFacility.FacilityName);
                    command.Parameters.AddWithValue("@fk_enterprise", productionFacility.EnterpriseId);
                    command.Parameters.AddWithValue("@fk_post_address", productionFacility.PostAddressId);
                    int productionFacilityId = Convert.ToInt32(command.ExecuteScalar());
                    productionFacility.SetProductionFacilityId(productionFacilityId);
                }

                Console.WriteLine("Adresse und Facility wurden erstellt");
            });

            return JsonConvert.SerializeObject(productionFacility);
        }

        public int Update(int enterpriseId, int productionFacilityId, int postAddressId, string? facilityName, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            int result = 0;
            ProductionFacility productionFacility = new ProductionFacility(productionFacilityId, postAddressId, enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryFacility = getQueryStringFacility(productionFacility);
                string queryPostAddress = getQueryPostAddress(productionFacility);

                using (SqlCommand command = new SqlCommand(queryFacility, connection))
                {
                    command.Connection.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTransaction = connection.BeginTransaction();

                    // Enlist a command in the current transaction.
                    command.Transaction = sqlTransaction;

                    try
                    {
                        result += updateProductionFacilityTable(productionFacility, command);
                        command.CommandText = queryPostAddress;
                        result += updatePostAddressTable(productionFacility, command);

                        // Commit the transaction.
                        sqlTransaction.Commit();
                    }
                    catch (SqlException error)
                    {
                        Console.Write(error.ToString());
                        sqlTransaction.Rollback();
                        throw error;
                    }
                }
            });
            return result;
        }

        private int updatePostAddressTable(ProductionFacility productionFacility, SqlCommand command)
        {
            if (!string.IsNullOrWhiteSpace(productionFacility.PostAddressRecord1))
            {
                command.Parameters.AddWithValue("@address_record_1", productionFacility.PostAddressRecord1);
            }

            if (!string.IsNullOrWhiteSpace(productionFacility.PostAddressRecord2))
            {
                command.Parameters.AddWithValue("@address_record_2", productionFacility.PostAddressRecord2);
            }

            if (!string.IsNullOrWhiteSpace(productionFacility.Street))
            {
                command.Parameters.AddWithValue("@street", productionFacility.Street);
            }

            if (!string.IsNullOrWhiteSpace(productionFacility.HouseNumber))
            {
                command.Parameters.AddWithValue("@house_number", productionFacility.HouseNumber);
            }

            if (!string.IsNullOrWhiteSpace(productionFacility.Postcode))
            {
                command.Parameters.AddWithValue("@postcode", productionFacility.Postcode);
            }

            if (!string.IsNullOrWhiteSpace(productionFacility.City))
            {
                command.Parameters.AddWithValue("@city", productionFacility.City);
            }

            int result = command.ExecuteNonQuery();

            return result;
        }

        private int updateProductionFacilityTable(ProductionFacility productionFacility, SqlCommand command)
        {
            // Insert parameters
            if (!string.IsNullOrWhiteSpace(productionFacility.FacilityName))
            {
                command.Parameters.AddWithValue("@production_facility_name", productionFacility.FacilityName);
            }

            int result = command.ExecuteNonQuery();

            return result;
        }

        private string getQueryPostAddress(ProductionFacility productionFacility)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacility.Id.ToString());

            if (!string.IsNullOrEmpty(productionFacility.PostAddressRecord1))
            {
                queryBuilder.AddQueryArg("address_record_1");
            }

            if (!string.IsNullOrEmpty(productionFacility.PostAddressRecord2))
            {
                queryBuilder.AddQueryArg("address_record_2");
            }

            if (!string.IsNullOrEmpty(productionFacility.City))
            {
                queryBuilder.AddQueryArg("city");
            }

            if (!string.IsNullOrEmpty(productionFacility.Street))
            {
                queryBuilder.AddQueryArg("street");
            }

            if (!string.IsNullOrEmpty(productionFacility.Postcode))
            {
                queryBuilder.AddQueryArg("postcode");
            }

            if (!string.IsNullOrEmpty(productionFacility.HouseNumber))
            {
                queryBuilder.AddQueryArg("house_number");
            }

            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }

        private string getQueryStringFacility(ProductionFacility productionFacility)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacility.Id.ToString());

            if (!string.IsNullOrEmpty(productionFacility.FacilityName))
            {
                queryBuilder.AddQueryArg("production_facility_name");
            }

            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }
    }
}

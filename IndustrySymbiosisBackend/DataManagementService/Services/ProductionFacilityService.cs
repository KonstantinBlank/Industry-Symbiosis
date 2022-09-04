using System;
using System.Data;
using System.Data.SqlClient;

using Newtonsoft.Json;

using DataManagementService.Data;
using DataManagementService.Interfaces;
using DataManagementService.Services;

namespace DataManagementService.Service
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
                    //result = command.ExecuteNonQuery();
                    int postAddressId = Convert.ToInt32(command.ExecuteScalar());
                    productionFacility.SetPostAddressId(postAddressId);
                }

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
                string queryStringFacilityAttributes = getQueryStringFacilityAttributes(productionFacilityId, productionFacility);
                string queryStringPostAddressAttributes = getQueryStringPostAddressAttributes(productionFacilityId, productionFacility);

                using (SqlCommand command = new SqlCommand(queryStringFacilityAttributes, connection))
                {
                    command.Connection.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTran = connection.BeginTransaction();

                    // Enlist a command in the current transaction.
                    //command = connection.CreateCommand();
                    command.Transaction = sqlTran;

                    try
                    {
                        result = updateProductionFacilityTable(result, productionFacility, queryStringPostAddressAttributes, command);
                        result = updatePostAddressTable(result, productionFacility, command);

                        // Commit the transaction.
                        sqlTran.Commit();
                    }
                    catch (SqlException error)
                    {
                        Console.Write(error.ToString());
                        sqlTran.Rollback();
                        throw error;
                    }
                }
            });

            return result;
        }

        private static int updatePostAddressTable(int result, ProductionFacility productionFacility, SqlCommand command)
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

            //command.Connection.Open();
            result += command.ExecuteNonQuery();
            return result;
        }

        private static int updateProductionFacilityTable(int result, ProductionFacility productionFacility, string queryStringPostAddressAttributes, SqlCommand command)
        {
            // Insert parameters
            if (!string.IsNullOrWhiteSpace(productionFacility.FacilityName))
            {
                command.Parameters.AddWithValue("@production_facility_name", productionFacility.FacilityName);
            }

            result += command.ExecuteNonQuery();

            command.CommandText = queryStringPostAddressAttributes;
            return result;
        }

        private static string getQueryStringPostAddressAttributes(int productionFacilityId, ProductionFacility productionFacility)
        {
            SqlQueryStringBuilder sqlQueryStringBuilderPostAddressAttributes = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacilityId.ToString());

            if (!string.IsNullOrEmpty(productionFacility.PostAddressRecord1))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("address_record_1");
            }

            if (!string.IsNullOrEmpty(productionFacility.PostAddressRecord2))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("address_record_2");
            }

            if (!string.IsNullOrEmpty(productionFacility.City))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("city");
            }

            if (!string.IsNullOrEmpty(productionFacility.Street))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("street");
            }

            if (!string.IsNullOrEmpty(productionFacility.Postcode))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("postcode");
            }

            if (!string.IsNullOrEmpty(productionFacility.HouseNumber))
            {
                sqlQueryStringBuilderPostAddressAttributes.AddQueryArg("house_number");
            }


            string queryStringPostAddressAttributes = sqlQueryStringBuilderPostAddressAttributes.GetSqlQueryString();
            Console.WriteLine(queryStringPostAddressAttributes);
            return queryStringPostAddressAttributes;
        }

        private static string getQueryStringFacilityAttributes(int productionFacilityId, ProductionFacility productionFacility)
        {
            SqlQueryStringBuilder sqlQueryStringBuilderFacilityAttributes = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacilityId.ToString());

            if (!string.IsNullOrEmpty(productionFacility.FacilityName))
            {
                sqlQueryStringBuilderFacilityAttributes.AddQueryArg("production_facility_name");
            }

            string queryStringFacilityAttributes = sqlQueryStringBuilderFacilityAttributes.GetSqlQueryString();
            Console.WriteLine(queryStringFacilityAttributes);
            return queryStringFacilityAttributes;
        }
    }
}

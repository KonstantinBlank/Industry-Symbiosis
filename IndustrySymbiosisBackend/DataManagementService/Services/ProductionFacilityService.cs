using DataManagementService.Data;
using DataManagementService.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataManagementService.Service
{
    public class ProductionFacilityService : IProductionFacilityService
    {
        private List<ProductionFacility> productionFacilities = new List<ProductionFacility>();

        public ProductionFacilityService()
        {
        }

        public string GetAsJSONStringByEnterpriseId(int enterpriseId)
        {
            string productionLinesOfEnterpriseasJSON = string.Empty;

            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {
                    string queryString;
                    queryString = @$"SELECT enterprise_user.first_name, enterprise_user.surname, enterprise_user.email
                                    FROM enterprise_user
                                    WHERE enterprise_user.fk_enterprise = {enterpriseId};";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dataReader);
                            productionLinesOfEnterpriseasJSON = JsonConvert.SerializeObject(dataTable);
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return productionLinesOfEnterpriseasJSON;
        }

        public string Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            ProductionFacility productionFacility = new ProductionFacility(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
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
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine("Adresse oder facility konnte nicht erstellt werden");
                Console.WriteLine(error.ToString());
            }

            return JsonConvert.SerializeObject(productionFacility);
        }

        public int Update(int enterpriseId, int productionFacilityId, int postAddressId, string? facilityName, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            int result = -1;
            ProductionFacility productionFacility = new ProductionFacility(productionFacilityId, postAddressId, enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);
            
            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {

                    SqlQueryStringBuilder sqlQueryStringBuilderFacilityAttributes = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacilityId.ToString()); 

                    if (facilityName != null)
                    {
                        sqlQueryStringBuilderFacilityAttributes.AddqueryArg("production_facility_name");
                    }

                    string queryStringFacilityAttributes = sqlQueryStringBuilderFacilityAttributes.GetSqlQueryString();
                    Console.WriteLine(queryStringFacilityAttributes);


                    SqlQueryStringBuilder sqlQueryStringBuilderPostAddressAttributes = new SqlQueryStringBuilder("production_facility_view", "production_facility_id", productionFacilityId.ToString());

                    if (postAddressRecord1 != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("address_record_1");
                    }

                    if (postAddressRecord2 != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("address_record_1");
                    }

                    if (city != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("city");
                    }

                    if (street != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("street");
                    }

                    if (postcode != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("postcode");
                    }

                    if (houseNumber != null)
                    {
                        sqlQueryStringBuilderPostAddressAttributes.AddqueryArg("house_number");
                    }


                    string queryStringPostAddressAttributes = sqlQueryStringBuilderPostAddressAttributes.GetSqlQueryString();
                    Console.WriteLine(queryStringPostAddressAttributes);

                    /*

                    CREATE VIEW production_facility_view
                    AS
                    SELECT production_facility.id as production_facility_id, production_facility.name as production_facility_name, fk_post_address, post_address.id as post_address_id, address_record_1, address_record_2, street, house_number, postcode, city 
                    FROM production_facility
                    LEFT JOIN post_address
                    ON production_facility.fk_post_address = post_address.id

                   */


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
                            // Insert parameters
                            if (!string.IsNullOrWhiteSpace(facilityName))
                            {
                                command.Parameters.AddWithValue("@production_facility_name", productionFacility.FacilityName);
                            }

                            result = command.ExecuteNonQuery();

                            command.CommandText = queryStringPostAddressAttributes;


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
                            result = command.ExecuteNonQuery();

                            // Commit the transaction.
                            sqlTran.Commit();
                        }
                        catch (SqlException error)
                        {
                            sqlTran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine("Adresse oder facility konnte nicht verändert werden");
                Console.WriteLine(error.ToString());
            }

            return result;
        }
    }
}


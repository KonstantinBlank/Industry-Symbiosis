using DataManagementService.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataManagementService.Service
{
    public class ProductionFacilityProviderService
    {
        private List<ProductionFacility> productionFacilities = new List<ProductionFacility>();

        public ProductionFacilityProviderService()
        {


        }

        public string GetProductionFacilitysByEnterpriseIDasJSONString(string enterpriseID)
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
                                    WHERE enterprise_user.fk_enterprise = {enterpriseID};";

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


        public string CreateProductionFacilitysByEnterpriseID(string facilityName, int enterpriseID, string postAddressRecord1, string postAdressRecord2, string street, string houseNumber, string postcode, string city)
        {
            int postAddressID;

            ProductionFacility productionFacility = new ProductionFacility(facilityName, enterpriseID, $"{postcode}, {city}, {street}, {houseNumber}, {postAddressRecord1} {postAdressRecord2}");
            
            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {
                    string queryString;

                    queryString = @"INSERT INTO post_address
			                            (address_record_1,
			                            street,
                                        house_number,
                                        postcode,
                                        city)
                                    VALUES
                                        (@postAddressRecord1,
		                                @street,
		                                @houseNumber,
		                                @postcode,
                                        @city);
                                    SELECT SCOPE_IDENTITY()";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {

                        // Insert parameters
                        command.Parameters.AddWithValue("@postAddressRecord1", postAddressRecord1);
                        command.Parameters.AddWithValue("@street", street);
                        command.Parameters.AddWithValue("@houseNumber", houseNumber);
                        command.Parameters.AddWithValue("@postcode", postcode);
                        command.Parameters.AddWithValue("@city", city);
                        command.Connection.Open();
                        //result = command.ExecuteNonQuery();
                        postAddressID = Convert.ToInt32(command.ExecuteScalar());
                    }

                    queryString = @"INSERT INTO production_facility
                                        (name,
                                        fk_enterprise,
                                        fk_post_address)
                                    VALUES
		                                (@name,
                                        @fk_enterprise,
		                                @fk_post_address);";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {

                        // Insert parameters
                        command.Parameters.AddWithValue("@name", facilityName);
                        command.Parameters.AddWithValue("@fk_enterprise", enterpriseID);
                        command.Parameters.AddWithValue("@fk_post_address", postAddressID);
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Adresse und Facility wurden erstellt");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Adresse oder facility konnte nicht erstellt werden");
                Console.WriteLine(e.ToString());
            }


            return JsonConvert.SerializeObject(productionFacility);
        }



    }
}

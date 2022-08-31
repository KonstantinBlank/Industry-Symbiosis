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

        //update

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

        public string Update(int productionFacilityId, int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            ProductionFacility productionFacility = new ProductionFacility(productionFacilityId, enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);
            try
            {
                string connectionString;
                connectionString = "Data Source=tcp:insym.database.windows.net,1433;Initial Catalog=Industrie-Symbiose_db;User Id=insym_adm@insym;Password=jio:90u?..Q++";

                using (SqlConnection connection = new SqlConnection(connectionString)) //auto close connection with using
                {

                    string queryString;
                    //       UPDATE _table_name_
                    //   SET _column1_ = _value1_, _column2_ = _value2_, ...  
                    //WHERE _condition_;
                    string result = "";
                    
                        result += updateName(facilityName);


                    //https://stackoverflow.com/questions/9890456/update-sql-statement-with-unknown-name-amount-of-params/9891276#9891276

                    queryString = @"UPDATE production_facility
                                    SET 
			                            (address_record_1,
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

                    

                }
            }
            catch (SqlException error)
            {
                Console.WriteLine("Adresse oder facility konnte nicht erstellt werden");
                Console.WriteLine(error.ToString());
            }


            return JsonConvert.SerializeObject(productionFacility);
        }

        private string updateName(string facilityName)
        {
            if (!string.IsNullOrWhiteSpace(facilityName))
            {

            }
            return "";
        }
    }
}


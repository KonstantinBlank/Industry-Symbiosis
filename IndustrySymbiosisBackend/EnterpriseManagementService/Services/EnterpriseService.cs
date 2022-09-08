using System;
using System.Data;
using System.Data.SqlClient;
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
                    command.Parameters.AddWithValue("@postAddressRecord1", enterprise.Address.PostAddressRecord1);
                    command.Parameters.AddWithValue("@postAddressRecord2", enterprise.Address.PostAddressRecord2);
                    command.Parameters.AddWithValue("@street", enterprise.Address.Street);
                    command.Parameters.AddWithValue("@houseNumber", enterprise.Address.HouseNumber);
                    command.Parameters.AddWithValue("@postcode", enterprise.Address.Postcode);
                    command.Parameters.AddWithValue("@city", enterprise.Address.City);
                    command.Connection.Open();

                    int postAddressId = Convert.ToInt32(command.ExecuteScalar());
                    enterprise.Address.SetId(postAddressId);
                }

                // production facility
                queryString = @"INSERT INTO enterprise
                                        (name,
                                         fk_address)
                                    VALUES
		                                (@name
		                                @fk_address);
                                    SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@name", enterprise.Name);
                    command.Parameters.AddWithValue("@fk_address", enterprise.Address.Id);
                    int enterpriseId = Convert.ToInt32(command.ExecuteScalar());
                    enterprise.SetId(enterpriseId);
                }

                Console.WriteLine("address and enterprise were successfully created.");
            });

            return JsonConvert.SerializeObject(enterprise);
        }

        public string GetAll()
        {
            string enterprises = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @"SELECT 
                                    enterprise.id, enterprise.name, post_address.id, post_address.address_record_1, post_address.address_record_2, post_address.street, post_address.house_number, post_address.postcode, post_address.city
                                    FROM enterprise
                                    LEFT JOIN post_address
                                    ON enterprise.fk_address = post_address.id;";
                enterprises = SqlConnectionHelper.GetTable(connection, queryString);
            });

            return enterprises;
        }

        public string GetById(int enterpriseId)
        {
            string enterprise = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = $@"SELECT 
                                    enterprise.id, enterprise.name, post_address.id, post_address.address_record_1, post_address.address_record_2, post_address.street, post_address.house_number, post_address.postcode, post_address.city
                                FROM enterprise
                                LEFT JOIN post_address
                                ON enterprise.fk_address = post_address.id
                                WHERE enterprise.id = {enterpriseId}";

                enterprise = SqlConnectionHelper.GetTable(connection, queryString);
            });

            return enterprise;
        }

        public int Update(int enterpriseId, int addressId, string? name, string? addressRecord1, string? addressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            int result = 0;
            Enterprise enterprise = new Enterprise(enterpriseId, name, new Address(addressId, addressRecord1, addressRecord2, street, houseNumber, postcode, city));

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryEnterprise = getQueryEnterprise(enterprise);
                string queryPostAddress = getQueryPostAddress(enterprise.Address);
                string query = !string.IsNullOrWhiteSpace(queryEnterprise) ? queryEnterprise : queryPostAddress;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTransaction = connection.BeginTransaction();

                    // Enlist a command in the current transaction.
                    command.Transaction = sqlTransaction;

                    try
                    {
                        if (!string.IsNullOrWhiteSpace(queryEnterprise))
                        {
                            result += updateEnterpriseTable(enterprise, command);
                        }
                        if (!string.IsNullOrWhiteSpace(queryPostAddress))
                        {
                            command.CommandText = queryPostAddress;
                            result += updatePostAddressTable(enterprise.Address, command);
                        }

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

        private int updatePostAddressTable(Address address, SqlCommand command)
        {
            if (!string.IsNullOrWhiteSpace(address.PostAddressRecord1))
            {
                command.Parameters.AddWithValue("@address_record_1", address.PostAddressRecord1);
            }

            if (!string.IsNullOrWhiteSpace(address.PostAddressRecord2))
            {
                command.Parameters.AddWithValue("@address_record_2", address.PostAddressRecord2);
            }

            if (!string.IsNullOrWhiteSpace(address.Street))
            {
                command.Parameters.AddWithValue("@street", address.Street);
            }

            if (!string.IsNullOrWhiteSpace(address.HouseNumber))
            {
                command.Parameters.AddWithValue("@house_number", address.HouseNumber);
            }

            if (!string.IsNullOrWhiteSpace(address.Postcode))
            {
                command.Parameters.AddWithValue("@postcode", address.Postcode);
            }

            if (!string.IsNullOrWhiteSpace(address.City))
            {
                command.Parameters.AddWithValue("@city", address.City);
            }

            int result = command.ExecuteNonQuery();

            return result;
        }

        private int updateEnterpriseTable(Enterprise enterprise, SqlCommand command)
        {
            // Insert parameters
            if (!string.IsNullOrWhiteSpace(enterprise.Name))
            {
                command.Parameters.AddWithValue("@name", enterprise.Name);
            }

            int result = command.ExecuteNonQuery();

            return result;
        }

        private string getQueryPostAddress(Address address)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("post_address", "id", address.Id.ToString());

            if (!string.IsNullOrEmpty(address.PostAddressRecord1))
            {
                queryBuilder.AddQueryArg("address_record_1");
            }

            if (!string.IsNullOrEmpty(address.PostAddressRecord2))
            {
                queryBuilder.AddQueryArg("address_record_2");
            }

            if (!string.IsNullOrEmpty(address.City))
            {
                queryBuilder.AddQueryArg("city");
            }

            if (!string.IsNullOrEmpty(address.Street))
            {
                queryBuilder.AddQueryArg("street");
            }

            if (!string.IsNullOrEmpty(address.Postcode))
            {
                queryBuilder.AddQueryArg("postcode");
            }

            if (!string.IsNullOrEmpty(address.HouseNumber))
            {
                queryBuilder.AddQueryArg("house_number");
            }
            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }

        private string getQueryEnterprise(Enterprise enterprise)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("enterprise_address_view", "enterprise_id", enterprise.Id.ToString());

            if (!string.IsNullOrEmpty(enterprise.Name))
            {
                queryBuilder.AddQueryArg("name");
            }

            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }
    }
}


using System;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Services;
using EnterpriseManagement.Interfaces;
using EnterpriseManagementService.Data;
using Newtonsoft.Json;

namespace EnterpriseManagement.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public string Create(int enterpriseId, string firstName, string surname, string email)
        {
            User user = new User(enterpriseId, firstName, surname, email);
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = @$"INSERT INTO enterprise_user (first_name, surname, email, fk_enterprise)
                                       VALUES (@firstname, @surname, @email, @fk_enterprise)
                                       SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@firstname", user.FirstName);
                    command.Parameters.AddWithValue("@surname", user.Surname);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@fk_enterprise", user.EnterpriseId);
                    command.Connection.Open();
                    int userId = Convert.ToInt32(command.ExecuteScalar());
                    user.SetId(userId);
                }
            });

            return JsonConvert.SerializeObject(user);
        }

        public string GetById(int userId)
        {
            string users = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT id, first_name, surname, email
                                    FROM enterprise_user
                                    WHERE id = {userId};";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        users = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return users;
        }

        public string GetAllFromEnterprise(int enterpriseId)
        {
            string users = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT id, first_name, surname, email
                                 FROM enterprise_user
                                 WHERE fk_enterprise = {enterpriseId};";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        users = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return users;
        }

        public int UpdateUser(int userId, string? firstName, string? surname, string? email)
        {
            int result = 0;
            // user cannot change the enterprise
            User user = new User(userId, -1, firstName, surname, email);

            SqlConnectionHelper.Connect((connection) =>
            {
                string query = getQuery(user);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection.Open();
                    SqlTransaction sqlTransaction = connection.BeginTransaction();
                    command.Transaction = sqlTransaction;

                    try
                    {
                        result += updateUserTable(command, user);
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

        private int updateUserTable(SqlCommand command, User user)
        {
            addParatemterWithValue(command, "first_name", user.FirstName);
            addParatemterWithValue(command, "surname", user.Surname);
            addParatemterWithValue(command, "email", user.Email);

            return command.ExecuteNonQuery();
        }

        private void addParatemterWithValue(SqlCommand command, string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                command.Parameters.AddWithValue(key, value);
            }
        }

        private string getQuery(User user)
        {
            SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("enterprise_user", "id", user.Id.ToString());

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                queryBuilder.AddQueryArg("first_name");
            }

            if (!string.IsNullOrEmpty(user.Surname))
            {
                queryBuilder.AddQueryArg("surname");
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                queryBuilder.AddQueryArg("email");
            }

            string query = queryBuilder.GetSqlQueryString();
            Console.WriteLine(query);
            return query;
        }
    }
}


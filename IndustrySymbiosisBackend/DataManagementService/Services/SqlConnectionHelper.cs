using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class SqlConnectionHelper
    {
        public static string GetTable(string query)
        {
            string json = "";

            connect((connection) =>
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        json = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return json;
        }

        public static int CreateEntry(string query, IDictionary<string, object> parameterPairs)
        {
            int id = -1;
            connect((connection) =>
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (KeyValuePair<string, object> parameterPair in parameterPairs)
                    {
                        string key = parameterPair.Key;
                        object value = parameterPair.Value;
                        command.Parameters.AddWithValue("@" + key, value);
                    }
                    command.Connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            });

            return id;
        }

        public static int UpdateEntry(string tableName, string id, IDictionary<string, object?> parameterPairs)
        {
            int updatedRows = 0;
            connect((connection) =>
            {
                //build query
                SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder(tableName, "id", id);
                foreach (KeyValuePair<string, object?> parameterPair in parameterPairs)
                {
                    queryBuilder.AddQueryArgument(parameterPair.Key, $"{parameterPair.Value}");
                }
                string query = queryBuilder.GetSqlQueryString();
                Console.WriteLine(query);
                if (!string.IsNullOrWhiteSpace(query))
                {
                    //open command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Connection.Open();
                        SqlTransaction sqlTransaction = connection.BeginTransaction();
                        command.Transaction = sqlTransaction;

                        try
                        {
                            //insert values
                            foreach (KeyValuePair<string, object?> parameterPair in parameterPairs)
                            {
                                string key = parameterPair.Key;
                                object? value = parameterPair.Value;

                                if (value != null)
                                {
                                    command.Parameters.AddWithValue("@" + key, value);
                                }
                            }

                            // execute and commit
                            updatedRows += command.ExecuteNonQuery();
                            sqlTransaction.Commit();
                        }
                        catch (SqlException error)
                        {
                            Console.Write(error.ToString());
                            sqlTransaction.Rollback();
                            throw error;
                        }
                    }
                }
            });
            return updatedRows;
        }

        private static void connect(Action<SqlConnection> action)
        {
            try
            {
                string connectionString = Constants.DB_CONNECTION_STRING;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    action(connection);
                }

            }
            catch (SqlException error)
            {
                Console.WriteLine(error.ToString());
            }
        }

    }
}

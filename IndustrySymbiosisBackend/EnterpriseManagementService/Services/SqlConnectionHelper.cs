using System;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class SqlConnectionHelper
    {
        public static void Connect(Action<SqlConnection> action)
        {
            try
            {
                string connectionString = Constants.DB_CONNECTION_STRING;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    action(connection);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string GetTable(SqlConnection connection, string queryString)
        {
            string json;
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.Connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    json = JsonConvert.SerializeObject(dataTable);
                }
            }

            return json;
        }
    }
}


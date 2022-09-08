using System;
using System.Data.SqlClient;
using DataManagementService.Data;

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

    }
}


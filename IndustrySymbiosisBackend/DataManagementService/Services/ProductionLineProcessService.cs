using System;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class ProductionLineProcessService : IProductionLineProcessService
    {
        public ProductionLineProcessService()
        {
        }

        public string Get(int productionLineId)
        {
            string productionLines = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT * 
                                 FROM production_line_process
                                 WHERE fk_production_line = {productionLineId};";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        productionLines = JsonConvert.SerializeObject(dataTable);
                    }
                }
            });

            return productionLines;
        }

        public string Create(int productionLineId, string name)
        {
            ProductionLineProcess productionLineProcess = new ProductionLineProcess(productionLineId, name);
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = @$"INSERT INTO production_line_process (fk_production_line,name)
                                       VALUES (@fk_production_line, @name)
                                       SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@fk_production_line", productionLineProcess.ProductionLineId);
                    command.Parameters.AddWithValue("@name", productionLineProcess.Name);
                    command.Connection.Open();
                    //result = command.ExecuteNonQuery();
                    int productionLineProcessId = Convert.ToInt32(command.ExecuteScalar());
                    productionLineProcess.SetProductionLineProcessId(productionLineProcessId);
                }
            });

            return JsonConvert.SerializeObject(productionLineProcess);
        }

        public int Update(int productionLineProcessId, string name)
        {
            int result = 0;
            ProductionLineProcess productionLineProcess = new ProductionLineProcess(productionLineProcessId, name);

            SqlConnectionHelper.Connect((connection) =>
            {
                SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("production_line_process", "id", productionLineProcess.Id.ToString());

                if (!string.IsNullOrEmpty(productionLineProcess.Name))
                {
                    queryBuilder.AddQueryArg("name");
                }

                string query = queryBuilder.GetSqlQueryString();
                Console.WriteLine(query);


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection.Open();

                    // Start a local transaction.
                    SqlTransaction sqlTran = connection.BeginTransaction();

                    // Enlist a command in the current transaction.
                    command.Transaction = sqlTran;

                    try
                    {
                        // Insert parameters
                        if (!string.IsNullOrWhiteSpace(productionLineProcess.Name))
                        {
                            command.Parameters.AddWithValue("@name", productionLineProcess.Name);
                        }

                        result += command.ExecuteNonQuery();

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
    }
}


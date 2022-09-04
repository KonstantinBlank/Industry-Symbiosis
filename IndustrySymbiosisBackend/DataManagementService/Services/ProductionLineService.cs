using System;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class ProductionLineService : IProductionLineService
    {
        public ProductionLineService()
        {
        }

        public string Get(int productionFacilityId)
        {
            string productionLines = string.Empty;

            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString;
                queryString = @$"SELECT * 
                                 FROM production_line
                                 WHERE fk_production_facility = {productionFacilityId};";

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

        public string Create(int productionFacilityId, string name)
        {
            ProductionLine productionLine = new ProductionLine(productionFacilityId, name);
            SqlConnectionHelper.Connect((connection) =>
            {
                string queryString = @$"INSERT INTO production_line (fk_production_facility,name)
                                       VALUES (@fk_production_facility, @name)
                                       SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    // Insert parameters
                    command.Parameters.AddWithValue("@fk_production_facility", productionLine.ProductionFacilityId);
                    command.Parameters.AddWithValue("@name", productionLine.Name);
                    command.Connection.Open();
                    //result = command.ExecuteNonQuery();
                    int productionLineId = Convert.ToInt32(command.ExecuteScalar());
                    productionLine.SetProductionLineId(productionLineId);
                }
            });

            return JsonConvert.SerializeObject(productionLine);
        }

        public int Update(int productionLineId, string name)
        {
            int result = 0;
            ProductionLine productionLine = new ProductionLine(productionLineId, name);

            SqlConnectionHelper.Connect((connection) =>
            {

                SqlQueryStringBuilder queryBuilder = new SqlQueryStringBuilder("production_line", "id", productionLineId.ToString());

                if (!string.IsNullOrEmpty(productionLine.Name))
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
                    //command = connection.CreateCommand();
                    command.Transaction = sqlTran;

                    try
                    {
                        // Insert parameters
                        if (!string.IsNullOrWhiteSpace(productionLine.Name))
                        {
                            command.Parameters.AddWithValue("@name", productionLine.Name);
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

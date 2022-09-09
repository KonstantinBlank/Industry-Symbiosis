using System;
using System.Collections.Generic;
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
            string query = @$"SELECT * 
                              FROM production_line_process
                              WHERE fk_production_line = {productionLineId};";

            string productionLines = SqlConnectionHelper.GetTable(query);

            return productionLines;
        }

        public string Create(int productionLineId, string name)
        {
            string query = @$"INSERT INTO production_line_process (fk_production_line,name)
                              VALUES (@fk_production_line, @name)
                              SELECT SCOPE_IDENTITY()";

            ProductionLineProcess productionLineProcess = new ProductionLineProcess(productionLineId, name);

            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();
            parameterPairs.Add("fk_production_line", productionLineProcess.ProductionLineId);
            parameterPairs.Add("name", productionLineProcess.Name);
            
            int productionLineProcessId = SqlConnectionHelper.CreateEntry(query, parameterPairs);
            productionLineProcess.SetId(productionLineProcessId);

            Console.WriteLine("production line process entry was successfully created!");

            return JsonConvert.SerializeObject(productionLineProcess);
        }

        public int Update(int id, int? productionLineId, string? name)
        {
            ProductionLineProcess productionLineProcess = new ProductionLineProcess(id, productionLineId, name);

            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add(new KeyValuePair<string, object?>("productionLineId", productionLineProcess.ProductionLineId));
            parameterPairs.Add(new KeyValuePair<string, object?>("name", productionLineProcess.Name));

            int result = SqlConnectionHelper.UpdateEntry("production_line_process", productionLineProcess.Id.ToString(), parameterPairs);

            return result;
        }
    }
}


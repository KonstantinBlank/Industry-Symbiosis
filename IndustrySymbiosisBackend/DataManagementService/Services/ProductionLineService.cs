using System;
using System.Collections.Generic;
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
            string query = @$"SELECT * 
                              FROM production_line
                              WHERE fk_production_facility = {productionFacilityId};";

            string productionLines = SqlConnectionHelper.GetTable(query);

            return productionLines;
        }

        public string Create(int productionFacilityId, string name)
        {
            string query = @$"INSERT INTO production_line (fk_production_facility,name)
                                  VALUES (@fk_production_facility, @name)
                                  SELECT SCOPE_IDENTITY()";

            ProductionLine productionLine = new ProductionLine(productionFacilityId, name);

            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();
            parameterPairs.Add("fk_production_facility", productionLine.ProductionFacilityId);
            parameterPairs.Add("name", productionLine.Name);

            int productionLineId = SqlConnectionHelper.CreateEntry(query, parameterPairs);
            productionLine.SetId(productionLineId);

            Console.WriteLine("production line process entry was successfully created!");

            return JsonConvert.SerializeObject(productionLine);
        }

        public int Update(int id, int? productionFacilityId, string? name)
        {
            ProductionLine productionLine = new ProductionLine(id, productionFacilityId, name);

            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add(new KeyValuePair<string, object?>("productionFacilityId", productionLine.ProductionFacilityId));
            parameterPairs.Add(new KeyValuePair<string, object?>("name", productionLine.Name));

            int result = SqlConnectionHelper.UpdateEntry("production_line", productionLine.Id.ToString(), parameterPairs);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class StreamService : IStreamService
    {
        public StreamService()
        {
        }

        public string Get(int productionLineProcessId)
        {
            string query = @$"SELECT * 
                              FROM stream
                              WHERE fk_production_line_process = {productionLineProcessId};";

            string streams = SqlConnectionHelper.GetTable(query);

            return streams;
        }

        public string Create(int productionLineProcessId, bool isInput, int? materialId, int? energyId, int amount, int interval)
        {
            Stream stream = new Stream(productionLineProcessId, isInput, materialId, energyId, amount, interval);

            string query;
            string column = (stream.MaterialId != null) ? "fk_material" : "fk_energy_source";
            query = @$"INSERT INTO stream
                            (fk_production_line_process,
                            is_input,
                            {column},
                            amount,
                            interval)
                       VALUES
                            (@fk_production_line_process,
                            @is_input,
                            @{column},
                            @amount,
                            @interval)
                           SELECT SCOPE_IDENTITY()";


            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();
            parameterPairs.Add("fk_production_line_process", stream.ProductionLineProcessId);
            parameterPairs.Add("is_input", stream.IsInput);
            parameterPairs.Add("fk_material", stream.MaterialId);
            parameterPairs.Add("fk_energy_source", stream.EnergyId);
            parameterPairs.Add("amount", stream.Amount);
            parameterPairs.Add("interval", stream.Interval);


            int streamId = SqlConnectionHelper.CreateEntry(query, parameterPairs);
            stream.SetId(streamId);

            Console.WriteLine("production line process entry was successfully created!");

            return JsonConvert.SerializeObject(stream);
        }

        public int Update(int id, int? productionLineProcessId, bool? isInput, int? materialId, int? energyId, int? amount, int? interval)
        {
            Stream stream = new Stream(id, productionLineProcessId, isInput, materialId, energyId, amount, interval);

            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("fk_production_line_process", stream.ProductionLineProcessId);
            parameterPairs.Add("is_input", stream.IsInput);
            parameterPairs.Add("fk_material", stream.MaterialId);
            parameterPairs.Add("fk_energy_source", stream.EnergyId);
            parameterPairs.Add("amount", stream.Amount);
            parameterPairs.Add("interval", stream.Interval);

            int result = SqlConnectionHelper.UpdateEntry("stream", stream.Id.ToString(), parameterPairs);

            return result;
        }

    }
}


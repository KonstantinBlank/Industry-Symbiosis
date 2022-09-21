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

        public string Get(int enterpriseId)
        {
            string query = @$"SELECT stream_enterprise_id.enterprise_id, stream_view.id AS id, stream_view.is_emission, stream_view.renewable_share, stream_view.is_intern AS is_internal_energy, production_facility.name AS production_facility, production_line.name AS production_line, production_line_process.name AS production_line_process, stream_view.amount, stream_view.unit, stream_view.interval, stream_view.is_input, stream_view.is_private FROM stream_unmatched
                                 LEFT JOIN stream_enterprise_id
                                 ON stream_unmatched.id = stream_enterprise_id.stream_id
	                                LEFT JOIN stream_view
	                                ON stream_unmatched.id = stream_view.id
										LEFT JOIN production_facility
										ON stream_enterprise_id.production_facility_id = production_facility.id
											LEFT JOIN production_line
											ON stream_enterprise_id.production_line_id = production_line.id
												LEFT JOIN production_line_process
												ON stream_enterprise_id.production_line_process_id = production_line_process.id
                                 WHERE stream_view.is_input = 1 AND stream_enterprise_id.enterprise_id = {enterpriseId};";

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
            if (stream.MaterialId != null)
            {
                parameterPairs.Add("fk_material", stream.MaterialId);
            }
            if (stream.EnergyId != null)
            {
                parameterPairs.Add("fk_energy_source", stream.EnergyId);
            }
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


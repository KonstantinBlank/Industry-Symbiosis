using System;
using System.Collections.Generic;
using DataManagementService.Data;
using DataManagementService.Interfaces;
using Newtonsoft.Json;

namespace DataManagementService.Services
{
    public class EnergySourceService : IEnergySourceService
    {
        public EnergySourceService()
        {
        }

        public string Create(string name, float renewableShare, bool isIntern, int enterpriseId)
        {
            EnergySource energySource = new EnergySource(name, renewableShare, isIntern, enterpriseId);

            string query = @$"INSERT INTO energy_source (name, renewable_share, is_intern, fk_enterprise)
                              VALUES (@name, @renewable_share, @is_intern, @enterpriseId)
                              SELECT SCOPE_IDENTITY()";

            IDictionary<string, object> parameterPairs = new Dictionary<string, object>();
            parameterPairs.Add("name", energySource.Name);
            parameterPairs.Add("renewable_share", energySource.RenewableShare);
            parameterPairs.Add("is_intern", energySource.IsIntern);
            parameterPairs.Add("enterpriseId", energySource.EnterpriseId);

            int energySourceId = SqlConnectionHelper.CreateEntry(query, parameterPairs);
            energySource.SetId(energySourceId);

            Console.WriteLine("user entry was successfully created!");

            return JsonConvert.SerializeObject(energySource);
        }

        public string Get(int enterpriseId)
        {
            string query = $@"SELECT *
                             FROM energy_source
                             WHERE fk_enterprise = {enterpriseId};";

            string energySources = SqlConnectionHelper.GetTable(query);

            return energySources;
        }

        public int Update(int id, string? name = null, float? renewableShare = null, bool? isIntern = null)
        {
            EnergySource energySource = new EnergySource(id, name, renewableShare, isIntern);

            IDictionary<string, object?> parameterPairs = new Dictionary<string, object?>();
            parameterPairs.Add("name", energySource.Name);
            parameterPairs.Add("renewable_share", energySource.RenewableShare);
            parameterPairs.Add("is_intern", energySource.IsIntern);

            int result = SqlConnectionHelper.UpdateEntry("energy_source", energySource.Id.ToString(), parameterPairs);

            return result;
        }
    }
}


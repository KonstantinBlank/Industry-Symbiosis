using System;
namespace DataManagementService.Interfaces
{
    public interface IStreamService
    {
        string Get(int productionLineProcessId);
        string Create(int productionLineProcessId, bool isInput, int? materialId, int? energyId, int amount, int interval);
        int Update(int id, int? productionLineProcessId, bool? isInput, int? materialId, int? energyId, int? amount, int? interval);
    }
}

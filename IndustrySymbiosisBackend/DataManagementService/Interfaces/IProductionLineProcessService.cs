using System;
namespace DataManagementService.Interfaces
{
    public interface IProductionLineProcessService
    {
        string Get(int productionProcessId);
        string Create(int productionFacilityId, string name);
        int Update(int productionLineId, string name);
    }
}


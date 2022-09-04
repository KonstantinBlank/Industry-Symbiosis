using System;
namespace DataManagementService.Interfaces
{
    public interface IProductionLineService
    {
        string Get(int productionFacilityId);
        string Create(int productionFacilityId, string name);
        int Update(int productionLineId, string name);
    }
}

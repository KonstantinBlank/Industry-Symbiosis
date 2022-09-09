namespace DataManagementService.Interfaces
{
    public interface IProductionLineProcessService
    {
        string Get(int productionLineId);
        string Create(int productionLineId, string name);
        int Update(int id, int? productionLineId, string? name);
    }
}


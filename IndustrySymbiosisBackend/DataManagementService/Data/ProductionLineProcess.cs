using System;
namespace DataManagementService.Data
{
    public class ProductionLineProcess : IQueryObject
    {
        public int Id { get; private set; }
        public int? ProductionLineId { get; private set; }
        public string? Name { get; private set; }

        public ProductionLineProcess(int id, int? productionLineId, string? name)
        {
            Id = id;
            ProductionLineId = productionLineId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public ProductionLineProcess(int productionLineId, string name)
        {
            Id = -1;
            ProductionLineId = productionLineId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The Id for the production line process was already set. It's {Id}.");
            }
        }
    }
}


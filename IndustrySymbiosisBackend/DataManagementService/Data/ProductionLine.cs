using System;
namespace DataManagementService.Data
{
    public class ProductionLine
    {
        public int Id { get; private set; }
        public int ProductionFacilityId { get; private set; }
        public string Name { get; private set; }

        public ProductionLine(int id, int productionFacilityId, string name)
        {
            Id = id;
            ProductionFacilityId = productionFacilityId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public ProductionLine(int productionFacilityId, string name)
        {
            Id = -1;
            ProductionFacilityId = productionFacilityId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void SetProductionLineId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The Id for the production line was already set. It's {Id}.");
            }
        }
    }
}


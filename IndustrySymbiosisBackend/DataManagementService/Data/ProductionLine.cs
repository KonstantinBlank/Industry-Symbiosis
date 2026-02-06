using System;
namespace DataManagementService.Data
{
    public class ProductionLine : IQueryObject
    {
        public int Id { get; private set; }
        public int? ProductionFacilityId { get; private set; }
        public string? Name { get; private set; }

        /// <summary>
        /// constructor for updating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productionFacilityId"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProductionLine(int id, int? productionFacilityId, string? name)
        {
            Id = id;
            ProductionFacilityId = productionFacilityId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }


        /// <summary>
        /// constructor for creating
        /// </summary>
        /// <param name="productionFacilityId"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProductionLine(int productionFacilityId, string name)
        {
            Id = -1;
            ProductionFacilityId = productionFacilityId;
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
                throw new InvalidOperationException($"The Id for the production line was already set. It's {Id}.");
            }
        }
    }
}


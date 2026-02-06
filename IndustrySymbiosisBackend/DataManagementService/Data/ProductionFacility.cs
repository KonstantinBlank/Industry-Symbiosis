using System;
using System.Text.Json.Serialization;

namespace DataManagementService.Data
{
    public class ProductionFacility : IQueryObject
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public int? EnterpriseId { get; private set; }
        public Address Address { get; private set; }

        /// <summary>
        /// constructor for creation
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="enterpriseId"></param>
        public ProductionFacility(string name, Address address, int enterpriseId)
        {
            Id = -1;
            Name = name;
            Address = address;
            EnterpriseId = enterpriseId;
        }

        /// <summary>
        /// constructor for updating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public ProductionFacility(int id, string? name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
            // the enterprise can not be changed
            EnterpriseId = -1;
        }

        public void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The production facility id was already set. It's {Id}.");
            }
        }
    }
}

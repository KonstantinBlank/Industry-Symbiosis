using System;
namespace EnterpriseManagementService.Data
{
    public class Enterprise
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public Address Address { get; private set; }


        /// <summary>
        /// Constructor for creating an enterprise.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public Enterprise(string name, Address address)
        {
            Id = -1;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(name));
        }


        /// <summary>
        /// contructor for updating an enterprise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public Enterprise(int id, string? name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        internal void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The Id for the enterprise was already set. It's {Id}.");
            }
        }
    }
}


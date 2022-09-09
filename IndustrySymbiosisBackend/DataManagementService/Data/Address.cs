using System;
namespace DataManagementService.Data
{
    public class Address : IQueryObject
    {
        public int Id { get; private set; }
        public string? FacilityName { get; private set; }
        public string? PostAddressRecord1 { get; private set; }
        public string? PostAddressRecord2 { get; private set; }
        public string? Postcode { get; private set; }
        public string? City { get; private set; }
        public string? Street { get; private set; }
        public string? HouseNumber { get; private set; }


        /// <summary>
        /// Constructor for creating the address.
        /// </summary>
        /// <param name="postAddressRecord1"></param>
        /// <param name="postAddressRecord2"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="postcode"></param>
        /// <param name="city"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Address(string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            Id = -1;
            PostAddressRecord1 = postAddressRecord1;
            PostAddressRecord2 = postAddressRecord2;
            Street = street ?? throw new ArgumentNullException(nameof(street));
            HouseNumber = houseNumber ?? throw new ArgumentNullException(nameof(houseNumber));
            Postcode = postcode ?? throw new ArgumentNullException(nameof(postcode));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        /// <summary>
        /// Constructor for updating the addresss.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postAddressRecord1"></param>
        /// <param name="postAddressRecord2"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="postcode"></param>
        /// <param name="city"></param>
        public Address(int id, string? postAddressRecord1, string? postAddressRecord2, string? street, string? houseNumber, string? postcode, string? city)
        {
            Id = id;
            PostAddressRecord1 = postAddressRecord1;
            PostAddressRecord2 = postAddressRecord2;
            Street = street;
            HouseNumber = houseNumber;
            Postcode = postcode;
            City = city;
        }

        public void SetId(int id)
        {
            if (Id == -1)
            {
                Id = id;
            }
            else
            {
                throw new InvalidOperationException($"The Id for the address was already set. It's {Id}.");
            }
        }
    }
}


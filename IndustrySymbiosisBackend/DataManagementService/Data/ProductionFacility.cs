using System;
using System.Text.Json.Serialization;

namespace DataManagementService.Data
{
    public class ProductionFacility
    {
        [JsonPropertyName("facilityName")]
        public int Id { get; private set; }
        public int? PostAddressId { get; private set; }
        public int? EnterpriseId { get; private set; }
        public string? FacilityName { get; private set; }
        public string? PostAddressRecord1 { get; private set; }
        public string? PostAddressRecord2 { get; private set; }
        public string? Postcode { get; private set; }
        public string? City { get; private set; }
        public string? Street { get; private set; }
        public string? HouseNumber { get; private set; }

        public ProductionFacility(int productionFacilityId, int? postAddressId, int? enterpriseId, string? facilityName, string? postAddressRecord1, string? postAddressRecord2,  string? street, string? houseNumber, string? postcode, string? city)
        {
            Id = productionFacilityId;
            PostAddressId = postAddressId;
            EnterpriseId = enterpriseId;
            FacilityName = facilityName;
            PostAddressRecord1 = postAddressRecord1;
            PostAddressRecord2 = postAddressRecord2;
            Street = street;
            HouseNumber = houseNumber;
            Postcode = postcode;
            City = city;
        }

        public ProductionFacility(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            Id = -1;
            EnterpriseId = enterpriseId;
            FacilityName = facilityName ?? throw new ArgumentNullException(nameof(facilityName));
            PostAddressRecord1 = postAddressRecord1;
            PostAddressRecord2 = postAddressRecord2;
            Street = street ?? throw new ArgumentNullException(nameof(street));
            HouseNumber = houseNumber ?? throw new ArgumentNullException(nameof(houseNumber));
            Postcode = postcode ?? throw new ArgumentNullException(nameof(postcode));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        public void SetPostAddressId(int postAddressId)
        {
            PostAddressId = postAddressId;
        }

        public void SetProductionFacilityId(int productionFacilityId)
        {
            if (Id == -1)
            {
                Id = productionFacilityId;
            }
            else
            {
                throw new InvalidOperationException($"The production facility id was already set. It's {Id}.");
            }
        }
    }
}

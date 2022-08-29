using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace DataManagementService.Data
{
    public class ProductionFacility
    {
        [JsonPropertyName("facilityName")]
        public string facilityName { get; private set; }
        public int enterpriseID { get; private set; }
        public string facilityAddress { get; private set; }

        public ProductionFacility(string facilityName, int enterpriseID, string facilityAddress)
        {
            this.facilityName = facilityName;
            this.enterpriseID = enterpriseID;
            this.facilityAddress = facilityAddress;
        }

        /// <summary>
        /// Returns string as JSON
        /// </summary>
        /// <returns></returns>
        // public override string ToString() => JsonSerializer.Serialize<>(this);
    }
}

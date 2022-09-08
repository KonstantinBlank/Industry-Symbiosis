using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductionFacilityController : ControllerBase
    {
        private ProductionFacilityService _productionFacilityService;

        public ProductionFacilityController()
        {
            _productionFacilityService = new ProductionFacilityService();
        }

        /// <summary>
        /// get all production facilities by enterpriseId
        /// </summary>
        /// <returns>
        /// json with all production facilities from the specified enterprise
        /// </returns>
        [HttpGet("production_facilities/get/{enterpriseId}")]
        public ActionResult Get(int enterpriseId)
        {
            string productionLinesOfEnterpriseasJSON = _productionFacilityService.Get(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLinesOfEnterpriseasJSON);
        }

        /// <summary>
        /// create production facility
        /// </summary>
        /// <returns></returns>
        [HttpPost("production_facilities/create/")]
        public IActionResult Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string CreatedProductionFacilityasJSON = _productionFacilityService.Create(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionFacilityasJSON);
        }

        [HttpPost("production_facilities/update/")]
        public IActionResult Update(int productionFacilityId, int? enterpriseId = null, int? postAddressId = null, string? facilityName = null, string? postAddressRecord1 = null, string? postAddressRecord2 = null, string? street = null, string? houseNumber = null, string? postcode = null, string? city = null)
        {
            int result = _productionFacilityService.Update(enterpriseId, productionFacilityId, postAddressId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(result);
        }
    }
}


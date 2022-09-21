using System;
using DataManagementService.Interfaces;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/production_facilities/")]
    public class ProductionFacilityController : ControllerBase
    {
        private IProductionFacilityService _productionFacilityService;

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
        [HttpGet("get/{enterpriseId}")]
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
        [HttpPost("create/")]
        public IActionResult Create(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string CreatedProductionFacilityasJSON = _productionFacilityService.Create(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionFacilityasJSON);
        }

        [HttpPost("update/")]
        public IActionResult Update(int id, int postAddressId, string? name = null, string? postAddressRecord1 = null, string? postAddressRecord2 = null, string? street = null, string? houseNumber = null, string? postcode = null, string? city = null)
        {
            int result = _productionFacilityService.Update(id, postAddressId, name, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(result);
        }
    }
}


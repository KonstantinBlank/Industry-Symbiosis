using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Data;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductionFacilityController : ControllerBase
    {
        private ProductionFacilityService _productionFacilityService { get; }

        public ProductionFacilityController(ProductionFacilityService productionFacilityService)
        {
            _productionFacilityService = productionFacilityService;
        }

        /// <summary>
        /// get all production facilities by enterpriseId
        /// </summary>
        /// <returns>
        /// json with all production facilities from the specified enterprise
        /// </returns>
        [HttpGet("production_facilities/get/{enterpriseId}")]
        public ActionResult GetProductionFacilities(int enterpriseId)
        {
            string productionLinesOfEnterpriseasJSON = _productionFacilityService.Get(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLinesOfEnterpriseasJSON);
        }

        /// <summary>
        /// create production facility
        /// </summary>
        /// <returns></returns>
        [HttpPost("production_facilities/create/")] // zu testzecken entfernt: {enterpriseID}/
        public IActionResult CreateProductionFacility(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string CreatedProductionFacilityasJSON = _productionFacilityService.Create(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionFacilityasJSON);
        }

       

        [HttpPost("production_facilities/update/")]
        public IActionResult UpdateProductionFacility(int enterpriseId, int productionFacilityId, int postAddressId, string? facilityName = null, string? postAddressRecord1 = null, string? postAddressRecord2 = null, string? street = null, string? houseNumber = null, string? postcode = null, string? city = null)
        {
            int result = _productionFacilityService.Update(enterpriseId, productionFacilityId, postAddressId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(result);
        }


    }
}


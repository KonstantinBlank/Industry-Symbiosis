using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Data;
using DataManagementService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductionFacilityController : ControllerBase
    {
        public ProductionFacilityService productionFacilityService { get; }

        public ProductionFacilityController(ProductionFacilityService productionFacilityService)
        {
            this.productionFacilityService = productionFacilityService;
        }

        /// <summary>
        /// get all Production Lines by enterpriseID
        /// </summary>
        /// <returns>
        /// json with all Production Lines with Production Line attributes from an Enterprise
        /// </returns>
        [HttpGet("productionfacilities/get/{enterpriseID}")]
        public ActionResult GetProductionFacilities(int enterpriseID)
        {
            string productionLinesOfEnterpriseasJSON = productionFacilityService.GetAsJSONStringByEnterpriseId(enterpriseID);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLinesOfEnterpriseasJSON);
        }

        /// <summary>
        /// set production facility by enterpriseID
        /// </summary>
        /// <returns></returns>
        [HttpPost("productionfacility/create/")] // zu testzecken entfernt: {enterpriseID}/
        public IActionResult CreateProductionFacility(int enterpriseId, string facilityName, string postAddressRecord1, string postAddressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string CreatedProductionFacilityasJSON = productionFacilityService.Create(enterpriseId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionFacilityasJSON);
        }

       

        [HttpPost("productionfacility/update/")]
        public IActionResult UpdateProductionFacility(int enterpriseId, int productionFacilityId, int postAddressId, string? facilityName = null, string? postAddressRecord1 = null, string? postAddressRecord2 = null, string? street = null, string? houseNumber = null, string? postcode = null, string? city = null)
        {
            int result = productionFacilityService.Update(enterpriseId, productionFacilityId, postAddressId, facilityName, postAddressRecord1, postAddressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(result);
        }


    }
}


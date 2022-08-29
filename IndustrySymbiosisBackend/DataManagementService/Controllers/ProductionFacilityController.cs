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
        public ProductionFacilityProviderService productionFacilityProviderService { get;}
      
        public ProductionFacilityController(ProductionFacilityProviderService productionFacilityProviderService)
        {
            this.productionFacilityProviderService = productionFacilityProviderService;
        }

        /// <summary>
        /// get all Production Lines by enterpriseID
        /// </summary>
        /// <returns>
        /// json with all Production Lines with Production Line attributes from an Enterprise
        /// </returns>
        [HttpGet("productionfacilitys/{enterpriseID}")]
        public ActionResult GetProductionFacilitysByEnterpriseID(string enterpriseID)
        {
            string productionLinesOfEnterpriseasJSON = productionFacilityProviderService.GetProductionFacilitysByEnterpriseIDasJSONString(enterpriseID);
            
            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLinesOfEnterpriseasJSON);
        }
  
        /// <summary>
        /// set production facility by enterpriseID
        /// </summary>
        /// <returns></returns>
        [HttpPost("productionfacility/")] // zu testzecken entfernt: {enterpriseID}/
        public IActionResult CreateProductionFacilitysByEnterpriseID(string facilityName, int enterpriseID, string postAddressRecord1, string postAdressRecord2, string street, string houseNumber, string postcode, string city)
        {
            string CreatedProductionFacilityasJSON = productionFacilityProviderService.CreateProductionFacilitysByEnterpriseID(facilityName, enterpriseID, postAddressRecord1, postAdressRecord2, street, houseNumber, postcode, city);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionFacilityasJSON);
        }

        // EditProductionFacilityByFacilityID


    }
}


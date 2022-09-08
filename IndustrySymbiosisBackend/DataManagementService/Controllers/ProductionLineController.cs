using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/production_lines")]
    public class ProductionLineController : ControllerBase
    {
        private ProductionLineService _productionLineService;

        public ProductionLineController()
        {
            _productionLineService = new ProductionLineService();
        }

        /// <summary>
        /// get all production lines from a production facility
        /// </summary>
        /// <returns>
        /// json with all production lines from the specified production facility
        /// </returns>
        [HttpGet("productionfacility/{productionFacilityId}")]
        public ActionResult GetProductionLines(int productionFacilityId)
        {
            string productionLinesJSON = _productionLineService.Get(productionFacilityId);
                
            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLinesJSON);
        }

        /// <summary>
        /// create production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("/create/")]
        public IActionResult CreateProductionLine(int productionFacilityId, string name)
        {
            string CreatedProductionLinesAsJSON = _productionLineService.Create(productionFacilityId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionLinesAsJSON);
        }

        /// <summary>
        /// update a production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("/update/")]
        public IActionResult UpdateProductionLine(int productionLineId, string name)
        {
            int CreatedProductionLines = _productionLineService.Update(productionLineId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionLines);
        }

    }
}


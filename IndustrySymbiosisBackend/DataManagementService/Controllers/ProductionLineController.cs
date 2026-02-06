using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/production_lines/")]
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
        [HttpGet("get/{productionFacilityId}")]
        public ActionResult Get(int productionFacilityId)
        {
            string productionLines = _productionLineService.Get(productionFacilityId);
                
            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLines);
        }

        /// <summary>
        /// create production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("create/")]
        public IActionResult Create(int productionFacilityId, string name)
        {
            string productionLine = _productionLineService.Create(productionFacilityId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(productionLine);
        }

        /// <summary>
        /// update a production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("update/")]
        public IActionResult Update(int id, int? productionFacilityId, string? name)
        {
            int updatedRows = _productionLineService.Update(id, productionFacilityId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(updatedRows);
        }
    }
}


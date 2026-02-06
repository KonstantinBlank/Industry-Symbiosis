using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/production_line_processes/")]
    public class ProductionLineProcessController : ControllerBase
    {
        private ProductionLineProcessService _productionLineProcessService;

        public ProductionLineProcessController()
        {
            _productionLineProcessService = new ProductionLineProcessService();
        }

        /// <summary>
        /// get all production lines processes from a production line
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{productionLineId}")]
        public ActionResult Get(int productionLineId)
        {
            string productionLineProcessesJSON = _productionLineProcessService.Get(productionLineId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLineProcessesJSON);
        }

        /// <summary>
        /// create production line process
        /// </summary>
        /// <returns></returns>
        [HttpPost("create/")]
        public IActionResult Create(int productionLineId, string name)
        {
            string CreatedProductionLineProcessJSON = _productionLineProcessService.Create(productionLineId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionLineProcessJSON);
        }

        /// <summary>
        /// update a production line process
        /// </summary>
        /// <returns></returns>
        [HttpPost("update/")]
        public IActionResult Update(int id, int? productionLineId, string? name)
        {
            int UpdatedProductionLineProcesses = _productionLineProcessService.Update(id, productionLineId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(UpdatedProductionLineProcesses);
        }
    }
}

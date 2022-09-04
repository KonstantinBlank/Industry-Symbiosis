using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductionLineProcessController : ControllerBase
    {
        private ProductionLineProcessService _productionLineProcessService;

        public ProductionLineProcessController()
        {
            _productionLineProcessService = new ProductionLineProcessService();
        }

        /// <summary>
        /// get all production lines from a production facility
        /// </summary>
        /// <returns>
        /// json with all production lines from the specified production facility
        /// </returns>
        [HttpGet("production_line_processes/{productionLineId}")]
        public ActionResult GetProductionLineProcesses(int productionLineId)
        {
            string productionLineProcessesJSON = _productionLineProcessService.Get(productionLineId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(productionLineProcessesJSON);
        }

        /// <summary>
        /// create production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("production_line_processes/create/")]
        public IActionResult CreateProductionLineProcess(int productionLineId, string name)
        {
            string CreatedProductionLineProcessJSON = _productionLineProcessService.Create(productionLineId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(CreatedProductionLineProcessJSON);
        }

        /// <summary>
        /// update a production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("production_line_processes/update/")]
        public IActionResult UpdateProductionLineProcess(int productionLineId, string name)
        {
            int UpdatedProductionLineProcesses = _productionLineProcessService.Update(productionLineId, name);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(UpdatedProductionLineProcesses);
        }
    }
}


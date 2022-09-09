using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [Route("api/energy_source/")]
    [ApiController]
    public class EnergySourceController : ControllerBase
    {
        private EnergySourceService _energySourceService = new EnergySourceService();
        
        /// <summary>
        /// get all production lines from a production facility
        /// </summary>
        /// <returns>
        /// json with all production lines from the specified production facility
        /// </returns>
        [HttpGet("get/{enterpriseId}")]
        public ActionResult Get(int enterpriseId)
        {
            string energySources = _energySourceService.Get(enterpriseId);
                
            Console.WriteLine("API abfrage erfolgreich");

            return Ok(energySources);
        }

        /// <summary>
        /// create production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("create/")]
        public IActionResult Create(string name, float renewableShare, bool isIntern, int enterpriseId)
        {
            string energySource = _energySourceService.Create(name, renewableShare, isIntern, enterpriseId);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(energySource);
        }

        /// <summary>
        /// update a production line
        /// </summary>
        /// <returns></returns>
        [HttpPost("update/")]
        public IActionResult Update(int id, string? name = null, float? renewableShare = null, bool? isIntern = null)
        {
            int updatedRows = _energySourceService.Update(id, name, renewableShare, isIntern);

            Console.WriteLine("API Abfrage durchgeführt");

            return Ok(updatedRows);
        }

    }
}


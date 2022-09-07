using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreamController : ControllerBase
    {
        private StreamService _streamService;

        public StreamController()
        {
            _streamService = new StreamService();
        }

        [HttpGet("streams/{processLineId}")]
        public ActionResult Get(int processLineId)
        {
            string streamsJSON = _streamService.Get(processLineId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpPost("streams/create/")]
        public ActionResult Create(int productionLineProcessId, bool isInput, string name, int materialId, int energyId, int amount, int interval)
        {
            _streamService.Create(productionLineProcessId, isInput, name, materialId, energyId, amount, interval);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok();
        }

        [HttpPost("streams/update/")]
        public ActionResult Update(int id, int productionLineProcessId, bool isInput, int materialId, int energyId, int amount, int interval)
        {
            _streamService.Update(id, productionLineProcessId, isInput, materialId, energyId, amount, interval);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok();
        }


    }
}


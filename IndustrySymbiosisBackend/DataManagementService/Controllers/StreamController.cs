using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/stream")]
    public class StreamController : ControllerBase
    {
        private StreamService _streamService;

        public StreamController()
        {
            _streamService = new StreamService();
        }

        [HttpGet("get/{processLineId}")]
        public ActionResult Get(int processLineId)
        {
            string streamsJSON = _streamService.Get(processLineId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpPost("create/")]
        public ActionResult Create(int productionLineProcessId, bool isInput, int amount, int interval, int? materialId = null, int? energyId = null)
        {
            if ((materialId == null && energyId == null) || (materialId != null && energyId != null))
            {
                Console.WriteLine("You need to pass either a materialId or a energyId.");
            }
            else
            {
                _streamService.Create(productionLineProcessId, isInput, materialId, energyId, amount, interval);
                Console.WriteLine("API abfrage erfolgreich");
            }

            return Ok();
        }

        [HttpPost("update/")]
        public ActionResult Update(int id, int? productionLineProcessId = null, bool? isInput = null, int? materialId = null, int? energyId = null, int? amount = null, int? interval = null)
        {
            _streamService.Update(id, productionLineProcessId, isInput, materialId, energyId, amount, interval);
            Console.WriteLine("API abfrage erfolgreich");
            return Ok();
        }
    }
}

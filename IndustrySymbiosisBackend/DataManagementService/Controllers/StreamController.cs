using System;
using DataManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers
{
    [ApiController]
    [Route("api/streams/")]
    public class StreamController : ControllerBase
    {
        private StreamService _streamService;

        public StreamController()
        {
            _streamService = new StreamService();
        }

        [HttpGet("get/{processId}")]
        public ActionResult Get(int processId)
        {
            string streamsJSON = _streamService.Get(processId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpPost("create/")]
        public ActionResult Create(int productionLineProcessId, bool isInput, int amount, int interval, int? materialId = null, int? energyId = null)
        {
            string message = "";
            if ((materialId == null && energyId == null) || (materialId != null && energyId != null))
            {
                message = "You need to pass either a materialId or a energyId.";
                Console.WriteLine(message);
            }
            else
            {
                message = _streamService.Create(productionLineProcessId, isInput, materialId, energyId, amount, interval);
                Console.WriteLine("API abfrage erfolgreich");
            }

            return Ok(message);
        }

        [HttpPost("update/")]
        public ActionResult Update(int id, int? productionLineProcessId = null, bool? isInput = null, int? materialId = null, int? energyId = null, int? amount = null, int? interval = null)
        {
            int updatedRows = _streamService.Update(id, productionLineProcessId, isInput, materialId, energyId, amount, interval);
            Console.WriteLine("API abfrage erfolgreich");
            return Ok(updatedRows);
        }
    }
}

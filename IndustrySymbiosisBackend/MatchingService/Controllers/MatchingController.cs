using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using MatchingService.Services;

namespace EnterpriseManagementService.Controllers
{
    [Route("api/matches")] // api/matching/
    [ApiController]
    public class MatchingController : ControllerBase
    {


        private StreamMatchingService _streamMatchingService { get; }

        public MatchingController(StreamMatchingService matchingService)
        {
            _streamMatchingService = matchingService;
        }


        [HttpGet("/inputstreams/enterprise/{enterpriseID}")] // api/matching/inputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableInputStreams(string enterpriseID)
        {
            string streamsJSON = _streamMatchingService.GetAvailableInputStreams(enterpriseID);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);

        }

        [HttpGet("/outputstreams/enterprise/{enterpriseID}")] // api/matching/outputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableOutputStreams(string enterpriseID)
        {
            string streamsJSON = _streamMatchingService.GetAvailableOutputStreams(enterpriseID);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }


        [HttpGet("inputstreams/allmatching/{outputStreamId}")] //  api/matching/inputstreams/allmatching/{outputStreamId}
        public ActionResult GetMatchingOuputStreams(int outputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingInputStreams(outputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpGet("outputstreams/allmatching/{inputStreamId}")] // api/matching/outputstreams/allmatching/{outputStreamId}
        public ActionResult GetMatchingInputStreams(int inputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingInputStreams(inputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }


        [HttpGet("streams/allmatches/{enterpriseId}")] // api/matching/outputstreams/all/
        public ActionResult create(string enterpriseId)
        {

            return Ok();
        }


        [HttpGet("streams/proposed/outgoing/{enterpriseId}")] // api/matching/outputstreams/all/
        public ActionResult create(string enterpriseId)
        {

            return Ok();
        }

        [HttpGet("streams/proposed/incoming/{enterpriseId}")] // api/matching/outputstreams/all/
        public ActionResult create(string enterpriseId)
        {

            return Ok();
        }

        [HttpPost("propose/")] // api/matching/outputstreams/all/
        public ActionResult create(bool selectedIsInput, string selectedStreamId, string requestedStreamId, float amount, int invervall, float priceProposal, string comment)
        {

            return Ok();
        }

        [HttpPost("cancel/")] // api/matching/outputstreams/all/
        public ActionResult create(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("decline/")] // api/matching/outputstreams/all/
        public ActionResult create(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("accept/")] // api/matching/outputstreams/all/
        public ActionResult create(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }



    }
}

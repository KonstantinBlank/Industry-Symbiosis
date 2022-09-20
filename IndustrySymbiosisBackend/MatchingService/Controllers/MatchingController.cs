using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using MatchingService.Services;

namespace EnterpriseManagement.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchingController : ControllerBase
    {
        private StreamMatchingService _streamMatchingService { get; }

        public MatchingController(StreamMatchingService matchingService)
        {
            _streamMatchingService = matchingService;
        }

        /*
        [HttpGet("/inputstreams/enterprise/{enterpriseID}")] // api/matching/inputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableInputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAvailableInputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);

        }

        [HttpGet("/outputstreams/enterprise/{enterpriseID}")] // api/matching/outputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableOutputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAvailableOutputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        */
        [HttpGet("inputstreams/allmatching/{outputStreamId}")] //  api/matches/inputstreams/allmatching/{outputStreamId}
        public ActionResult GetMatchingOuputStreams(int outputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingInputStreams(outputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }
        /*

        [HttpGet("outputstreams/allmatching/{inputStreamId}")] // api/matching/outputstreams/allmatching/{intputStreamId}
        public ActionResult GetMatchingInputStreams(int inputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingOutputStreams(inputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        */

        [HttpGet("streams/proposed/outgoing/{enterpriseId}")] // api/matching/outputstreams/all/
        public ActionResult proposeOutgoing(string enterpriseId)
        {

            return Ok();
        }

        [HttpGet("streams/proposed/incoming/{enterpriseId}")] // api/matching/outputstreams/all/
        public ActionResult proposeIncoming(string enterpriseId)
        {

            return Ok();
        }

        [HttpPost("propose/")] // api/matching/outputstreams/all/
        public ActionResult propose(int enterpriseId, bool selectedIsInput, int selectedStreamId, int requestedStreamId, float amount, int invervall, float priceProposal, string comment)
        {
            int result = _streamMatchingService.propose(enterpriseId, selectedIsInput, selectedStreamId, requestedStreamId, amount, priceProposal, comment);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(result);
        }

        [HttpPost("cancel/")] // api/matching/outputstreams/all/
        public ActionResult cancel(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("decline/")] // api/matching/outputstreams/all/
        public ActionResult decline(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("accept/")] // api/matching/outputstreams/all/
        public ActionResult accept(string matchId)
        {

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using MatchingService.Services;
using MatchingService.Interfaces;

namespace EnterpriseManagement.Controllers
{
    [Route("api/matches/")]
    [ApiController]
    public class MatchingController : ControllerBase
    {
        private StreamMatchingService _streamMatchingService { get; }

        public MatchingController(StreamMatchingService matchingService)
        {
            _streamMatchingService = matchingService;
        }


        [HttpGet("outputstreams/get/enterprise/{enterpriseId}")] 
        public ActionResult GetAllOutputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAllOutputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpGet("inputstreams/get/enterprise/{enterpriseId}")] 
        public ActionResult GetAllInputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAllInputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpGet("allstreams/get/enterprise/{enterpriseId}")]
        public ActionResult GetAllStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAllStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }


        //----------------------------


        [HttpGet("inputstreams/enterprise/{enterpriseID}")] // api/matches/inputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableInputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAvailableInputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        

        [HttpGet("outputstreams/enterprise/{enterpriseID}")] // api/matches/outputstreams/enterprise/{enterpriseID}
        public ActionResult GetAvailableOutputStreams(int enterpriseId)
        {
            string streamsJSON = _streamMatchingService.GetAllInputStreams(enterpriseId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        
        [HttpGet("outputstreams/allmatching/{inputStreamId}")] //  api/matches/outputstreams/allmatching/{inputStreamId}
        public ActionResult GetMatchingOuputStreams(int inputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingOutputStreams(inputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }
        
        [HttpGet("inputstreams/allmatching/{outputStreamId}")] // api/matches/inputstreams/allmatching/{outputStreamId}
        public ActionResult GetMatchingInputStreams(int outputStreamId)
        {
            string streamsJSON = _streamMatchingService.GetMatchingInputStreams(outputStreamId);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(streamsJSON);
        }

        [HttpGet("streams/proposed/outgoing/{enterpriseId}")] // api/matches/outputstreams/all/
        public ActionResult proposeOutgoing(int enterpriseId, bool selectedIsInput, int selectedStreamId, int requestedStreamId, float amount, float? priceProposal = null, string comment = null)
        {
            _streamMatchingService.propose(enterpriseId, selectedIsInput, selectedStreamId, requestedStreamId, amount, priceProposal, comment);
            return Ok();
        }

        [HttpGet("streams/proposed/incoming/{enterpriseId}")] // api/matches/outputstreams/all/
        public ActionResult proposeIncoming(string enterpriseId)
        {

            return Ok();
        }

        [HttpPost("propose/")] // propose
        public ActionResult propose(int enterpriseId, bool selectedIsInput, int selectedStreamId, int requestedStreamId, float amount, float priceProposal = 0, string comment = "default")
        {
            int result = _streamMatchingService.propose(enterpriseId, selectedIsInput, selectedStreamId, requestedStreamId, amount, priceProposal, comment);

            Console.WriteLine("API abfrage erfolgreich");

            return Ok(result);
        }

        [HttpPost("cancel/")] // api/matches/outputstreams/all/
        public ActionResult cancel(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("decline/")] // api/matches/outputstreams/all/
        public ActionResult decline(string selectedStreamId, string requestedStreamId)
        {

            return Ok();
        }

        [HttpPost("accept/")] // api/matches/outputstreams/all/
        public ActionResult accept(string matchId)
        {

            return Ok();
        }
    }
}

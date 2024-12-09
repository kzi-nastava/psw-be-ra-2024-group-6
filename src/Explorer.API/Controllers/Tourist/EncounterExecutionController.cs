using System.Diagnostics;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/encounterExecutions")]
    public class EncounterExecutionController : BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;

        public EncounterExecutionController(IEncounterExecutionService encounterExecutionService)
        {
            _encounterExecutionService = encounterExecutionService;
        }

        [HttpPost("startMisc/{encounterId:long}")]
        public ActionResult<EncounterExecutionDto> StartMiscExecution(long encounterId)
        {
            int userId = User.UserId();
            var result = _encounterExecutionService.StartMiscExecution(encounterId, userId);
            return CreateResponse(result);
        }
        [HttpPost("completeMisc/{executionId:long}")]
        public ActionResult<EncounterExecutionDto> CompleteMiscExecution(long executionId)
        {
            Debug.WriteLine(executionId);
            var result = _encounterExecutionService.FinishMiscExecution(executionId, User.UserId());
            return CreateResponse(result);
        }

        [HttpPost("startHidden/{encounterId:long}")]
        public ActionResult<HiddenEncounterExecutionDto> StartHiddenExecution(long encounterId)
        {
            var result = _encounterExecutionService.StartHiddenExecution(encounterId, User.UserId());
            return CreateResponse(result);
        }

        // salje se na svakih 2 sekunde
        [HttpPost("processHidden/{executionId:long}")]
        public ActionResult<HiddenEncounterExecutionDto> ProcessHiddenExecution(long executionId, LocationDto currentPosition)
        {
            var result = _encounterExecutionService.ProcessHiddenExecution(executionId, User.UserId(), currentPosition);
            return CreateResponse(result);
        }

        [HttpPost("start-social/{encounterId:long}")]
        public ActionResult<SocialEncounterReadDto> StartSocialEncounter(long encounterId)
        {
            var result = _encounterExecutionService.StartSocialEncounterExecution(encounterId, User.UserId());
            return CreateResponse(result);
        }

        //[HttpPost("update-location/{encounterExecutionId:long}")]
        //public ActionResult<SocialEncounterReadDto> UpdateLocation(long encounterExecutionId,
        //    [FromBody] LocationDto location)
        //{
        //    var result = _encounterExecutionService.UpdateSocialExecutionLocation(encounterExecutionId, location, User.UserId());
        //    return CreateResponse(result);
        //}

    }
}

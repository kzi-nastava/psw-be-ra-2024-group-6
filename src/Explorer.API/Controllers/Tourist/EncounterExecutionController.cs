using System.Diagnostics;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/encounterexecutions")]
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
            var result = _encounterExecutionService.FinishMiscExecution(executionId);
            return CreateResponse(result);
        }

    }
}

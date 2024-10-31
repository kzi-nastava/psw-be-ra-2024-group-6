using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;
        private readonly ICheckpointService _checkpointService;

        public TourExecutionController(ITourExecutionService tourExecutionService,ICheckpointService checkpointService)
        {
            _tourExecutionService = tourExecutionService;
            _checkpointService = checkpointService;
        }

        [HttpPost]
        public ActionResult<TourExecutionDto> CreateTourExecution([FromBody] TourExecutionDto tourExecution)
        {
            tourExecution.TouristId = User.UserId();
            var result = _tourExecutionService.Create(tourExecution);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<TourExecutionDto> FinalizeTourExecution([FromQuery] int tourExecutionId, [FromQuery] string status)
        {
            var result = _tourExecutionService.FinalizeTourExecution(tourExecutionId, status, User.UserId());
            return CreateResponse(result);
        }

        [HttpGet("checkpoints/{tourId:long}")]
        public ActionResult<List<CheckpointReadDto>> GetByTourId(long tourId)
        {
            var result = _checkpointService.GetByTourId(tourId);
            return CreateResponse(result);
        }
    }
}

using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-execution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService tourExecutionService)
        {
            _tourExecutionService = tourExecutionService;
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
            var result = _tourExecutionService.FinalizeTourExecution(tourExecutionId, status, -1);
            return CreateResponse(result);
        }
    }
}

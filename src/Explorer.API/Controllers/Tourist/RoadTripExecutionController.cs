using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/road-trip-execution")]
public class RoadTripExecutionController : BaseApiController
{
    private readonly IRoadTripExecutionService _roadTripExecutionService;

    public RoadTripExecutionController(IRoadTripExecutionService roadTripExecutionService)
    {
        _roadTripExecutionService = roadTripExecutionService;
    }

    [HttpPost]
    public ActionResult<RoadTripExecutionDto> CreateRoadTripExecution([FromBody]  RoadTripExecutionCreateDto roadTripExecutionCreate)
    {
        var result = _roadTripExecutionService.Create(roadTripExecutionCreate, User.UserId());

        return CreateResponse(result);
    }
}

using Explorer.BuildingBlocks.Core.UseCases;
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

    [HttpGet]
    public ActionResult<RoadTripExecutionDto> GetRoadTripExecution([FromQuery] int roadTripExecutionId)
    {
        var result = _roadTripExecutionService.GetById(roadTripExecutionId, User.UserId());

        return CreateResponse(result);
    }

    [HttpGet("get-by-tourist")]
    public ActionResult<PagedResult<RoadTripExecutionDto>> GetAll()
    {
        var touristId = User.UserId();
        var result = _roadTripExecutionService.GetByTouristId(touristId);
        return CreateResponse(result);
    }

    [HttpPut("finalize")]
    public ActionResult<RoadTripExecutionDto> FinalizeRoadTripExecution([FromQuery] int roadTripExecutionId, [FromQuery] string status)
    {
        var result = _roadTripExecutionService.FinalizeRoadTripExecution(roadTripExecutionId, User.UserId(), status);

        return CreateResponse(result);
    }

    [HttpPut("update")]
    public ActionResult<RoadTripExecutionDto> UpdateTouristLocation([FromBody] RoadTripExecutionDto roadTripExecution)
    {
        var result = _roadTripExecutionService.UpdateLocation(roadTripExecution);
        return CreateResponse(result);
    }

    [HttpPut("update-completion")]
    public ActionResult<RoadTripExecutionDto> UpdateCompletion([FromBody] int roadTripExecutionId)
    {
        var result = _roadTripExecutionService.UpdateCompletion(roadTripExecutionId);
        return CreateResponse(result);
    }
}
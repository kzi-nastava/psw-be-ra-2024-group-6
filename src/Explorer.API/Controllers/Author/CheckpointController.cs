using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/checkpoint")]
    public class CheckpointController : BaseApiController
    {
        private readonly ICheckpointService _checkpointService;

        public CheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
        }

        [HttpGet("{tourId:long}")]
        public ActionResult<PagedResult<CheckpointReadDto>> GetByTourId(long tourId)
        {
            var result = _checkpointService.GetByTourId(tourId);
            return CreateResponse(result);
        }
        [HttpGet]
        public ActionResult<PagedResult<CheckpointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _checkpointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<CheckpointDto> Create([FromBody] CheckpointCreateDto checkpointDto)
        {
            var result = _checkpointService.Create(checkpointDto);
            return CreateResponse(result);
        }

        [HttpPost("public")]
        public ActionResult<CheckpointReadDto> CreatePublicCheckpoint([FromBody] CheckpointDto checkpointDto)

        {
            var userId = User.UserId();
            var result = _checkpointService.CreatePublicCheckpoint(checkpointDto, userId);
            return CreateResponse(result);
        }

        [HttpGet("nearby")]
        public ActionResult<List<CheckpointDto>> GetNearbyPublicCheckpoints([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _checkpointService.GetNearbyPublicCheckpoints(latitude, longitude, radius);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CheckpointDto> Update([FromBody] CheckpointDto checkpointDto)
        {
            var result = _checkpointService.Update(checkpointDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _checkpointService.Delete(id);
            return CreateResponse(result);
        }
    }
}

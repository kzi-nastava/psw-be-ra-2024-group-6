using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/checkpoint")]
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
    }
}

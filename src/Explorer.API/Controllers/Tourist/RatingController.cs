using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/ratings")]
    public class RatingController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public ActionResult<PagedResult<RatingDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _ratingService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<RatingDto> Create([FromBody] RatingDto ratingDto)
        {
            var createdRating = _ratingService.Create(ratingDto);
            return CreateResponse(createdRating);
        }

        [HttpPut("{id:int}")]
        public ActionResult<RatingDto> Update([FromBody] RatingDto ratingDto)
        {
            var updateResult = _ratingService.Update(ratingDto);
            return CreateResponse(updateResult);
        }
    }
}
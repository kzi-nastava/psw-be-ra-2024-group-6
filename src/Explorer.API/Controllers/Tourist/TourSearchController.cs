using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-search")]
    public class TourSearchController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourSearchController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public ActionResult<List<TourCardDto>> SearchToursNearby([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourService.GetSearchedToursNearby(latitude, longitude, radius);
            return CreateResponse(result);
        }
        [HttpGet("author-leaderboard")]
        public ActionResult<List<AuthorLeaderboardDto>> GetAuthorLeaderboard([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourService.GetAuthorLeaderboard(latitude, longitude, radius);
            return CreateResponse(result);
        }
        [HttpGet("sort")]
        public ActionResult<List<TourCardDto>> SortTours([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius,[FromQuery]string criteria)
        {
            var result = _tourService.GetSortedTours(latitude,longitude,radius,criteria);
            return CreateResponse(result);
        }
        [HttpPost("filter")]
        public ActionResult<List<TourCardDto>> FilterTours([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius, [FromBody] TourFilterDto tourFiltersDto)
        {
            var result = _tourService.GetFilteredTours(latitude, longitude, radius, tourFiltersDto);
            return CreateResponse(result);
        }
    }
}
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.LocationDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/tour-search")]
    public class TourSearchController : BaseApiController
    { 
        private readonly ITourSearchService _tourSearchService;

        public TourSearchController(ITourSearchService tourSearchService)
        {
            _tourSearchService = tourSearchService;
        }

        [HttpGet]
        public ActionResult<List<TourCardDto>> SearchToursNearby([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourSearchService.GetSearchedToursNearby(latitude, longitude, radius);
            return CreateResponse(result);
        }
        [HttpPost("road")]
        public ActionResult<RoadTripHoverMapDto> GetRoadTripSuggestions([FromBody] RoadTripDto roadTripDto)
        {
            var result = _tourSearchService.GetRoadTripSuggestions(roadTripDto);
            return CreateResponse(result);
        }
        [HttpGet("author-leaderboard")]
        public ActionResult<List<AuthorLeaderboardDto>> GetAuthorLeaderboard([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourSearchService.GetAuthorLeaderboard(latitude, longitude, radius);
            return CreateResponse(result);
        }
        [HttpGet("sort")]
        public ActionResult<List<TourCardDto>> SortTours([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius,[FromQuery]string criteria)
        {
            var result = _tourSearchService.GetSortedTours(latitude,longitude,radius,criteria);
            return CreateResponse(result);
        }
        [HttpPost("filter")]
        public ActionResult<List<TourCardDto>> FilterTours([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius, [FromBody] TourFilterDto tourFiltersDto)
        {
            var result = _tourSearchService.GetFilteredTours(latitude, longitude, radius, tourFiltersDto);
            return CreateResponse(result);
        }
    }
}
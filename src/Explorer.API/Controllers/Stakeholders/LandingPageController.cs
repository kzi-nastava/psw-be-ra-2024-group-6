using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Stakeholders
{
    [Route("api/landingPage")]
    public class LandingPageController : BaseApiController
    {
        private readonly ITourService _tourService;

        public LandingPageController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("allTours")]
        public ActionResult<List<TourCardDto>> GetLandingPageTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllTourCards(page, pageSize);
            return CreateResponse(result);
        }
        [HttpGet("map-hover")]
        public ActionResult<List<TourHoverMapDto>> GetToursOnMapNearby([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourService.FindToursOnMapNearby(latitude, longitude, radius);
            return CreateResponse(result);
        }
        [HttpGet("map-preview")]
        public ActionResult<List<TourHoverMapDto>> GetTourPreviewsOnMap([FromQuery] double longitude, [FromQuery]  double latitude )
        {
            var result = _tourService.GetTourPreviewsOnMap(latitude, longitude);
            return CreateResponse(result);
        }

    }
}

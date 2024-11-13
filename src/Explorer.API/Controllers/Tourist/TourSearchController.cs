using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourSearch")]
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
            var result = _tourService.FindToursNearby(latitude, longitude, radius);
            return CreateResponse(result);
        }
    }
}
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
    }
}

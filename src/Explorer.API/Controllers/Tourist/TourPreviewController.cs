using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourPreview")]
    public class TourPreviewController : BaseApiController
    {
        private readonly ITourService _tourService;

        public TourPreviewController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("preview/{tourId:long}")]
        public ActionResult<TourPreviewDto> GetTourPreview(long tourId)
        {
            var result = _tourService.GetTourPreview(tourId);
            return CreateResponse(result);
        }
    }
}

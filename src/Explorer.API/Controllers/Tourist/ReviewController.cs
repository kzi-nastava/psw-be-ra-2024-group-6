using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/reviews")]
    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public ActionResult<ReviewDto> Create([FromBody] ReviewDto reviewDto)
        {

            var result = _reviewService.CreateWithDateParser(reviewDto);
            return CreateResponse(result);

        }

        [HttpGet("all-reviews")]
        public ActionResult<IEnumerable<ReviewDto>> GetAll()
        {
            var reviews = _reviewService.GetAllReviews();
            return Ok(reviews);
        }
    }
}

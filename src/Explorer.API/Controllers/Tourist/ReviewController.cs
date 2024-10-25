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

        [HttpPost("addReview")]
        public ActionResult<ReviewDto> Create([FromBody] ReviewDto reviewDto)
        {
            reviewDto.TourDate = DateTime.SpecifyKind(reviewDto.TourDate, DateTimeKind.Utc);
            reviewDto.ReviewDate = DateTime.SpecifyKind(reviewDto.ReviewDate, DateTimeKind.Utc);

            var result = _reviewService.Create(reviewDto);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("allReview")]
        public ActionResult<IEnumerable<ReviewDto>> GetAll()
        {
            var reviews = _reviewService.GetAllReviews();
            return Ok(reviews);
        }
    }
}

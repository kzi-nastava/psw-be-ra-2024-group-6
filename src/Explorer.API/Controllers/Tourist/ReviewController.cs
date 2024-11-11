using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
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
        [HttpGet("all-reviews/{userId:long}")]
        public ActionResult<IEnumerable<ReviewDto>> GetAllByUser(long userId)
        {
            var result = _reviewService.GetAllByUser(userId);
            return Ok(result);
        }

        [HttpGet("tour/{tourId}/reviews")]
        public ActionResult<IEnumerable<ReviewDto>> GetReviewsByTourId(long tourId)
        {
            var reviews = _reviewService.GetReviewsFromTourId(tourId);
            return Ok(reviews);
        }
        [HttpPut]
        public ActionResult<User> Update([FromBody] ReviewDto review)
        {
            if (string.IsNullOrWhiteSpace(review.Comment) || review.Completion < 35 || review.Completion > 100 || review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Bad fields.");
            }
            var result = _reviewService.Update(review);
            return CreateResponse(result);
        }


    }
}

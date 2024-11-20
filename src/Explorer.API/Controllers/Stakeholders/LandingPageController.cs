using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Stakeholders
{
    [Route("api/landingPage")]
    public class LandingPageController : BaseApiController
    {
        private readonly ITourService _tourService;
        private readonly IAuthorService _authorService;
        private readonly ICheckpointService _checkpointService;
        private readonly IRatingService _ratingService;
        private readonly IBlogService _blogService;

        public LandingPageController(ITourService tourService, IAuthorService authorService, ICheckpointService checkpointService, IRatingService ratingService, IBlogService blogService)
        {
            _tourService = tourService;
            _authorService = authorService;
            _checkpointService = checkpointService;
            _ratingService = ratingService;
            _blogService = blogService;
        }

        [HttpGet("allTours")]
        public ActionResult<List<TourCardDto>> GetLandingPageTours([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllTourCards(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("most-popular-tours")]
        public ActionResult<List<TourCardDto>> GetMostPopularTours(int count = 4)
        {
            var result = _tourService.GetMostPopularTours(count);
            return CreateResponse(result);
        }

        [HttpGet("most-popular-authors")]
        public ActionResult<List<PersonDto>> GetMostPopularAuthors()
        {
            var result = _authorService.GetMostPopularAuthors();
            return CreateResponse(result);
        }

        [HttpGet("most-popular-destinations")]
        public ActionResult<List<DestinationDto>> GetMostPopularDestinations()
        {
            var result = _checkpointService.GetMostPopularDestinations();
            return CreateResponse(result);
        }

        [HttpGet("best")]
        public ActionResult<PagedResult<RatingReadDto>> GetBestRatings([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _ratingService.GetBestPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("blogs")]
        public ActionResult<List<BlogHomeDto>> GetBlogs([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.GetHomePaged(page, pageSize);
            return CreateResponse(result);
        }
    }
}

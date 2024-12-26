using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.CheckpointsDtos;
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
        private readonly Explorer.Stakeholders.Core.UseCases.RatingService _ratingService;
        private readonly IBlogService _blogService;

        public LandingPageController(ITourService tourService, IAuthorService authorService, ICheckpointService checkpointService, RatingService ratingService, IBlogService blogService)
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
        [HttpGet("map-hover")]
        public ActionResult<List<TourHoverMapDto>> GetToursOnMapNearby([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _tourService.FindToursOnMapNearby(latitude, longitude, radius);
            return CreateResponse(result);
        }



        [HttpGet("nearby-checkpoints")]
        public ActionResult<List<CheckpointDto>> GetNearbyPublicCheckpoints([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radius)
        {
            var result = _checkpointService.GetNearbyPublicCheckpoints(latitude, longitude, radius);
            return CreateResponse(result);
        }

        [HttpGet("map-preview")]
        public ActionResult<List<TourHoverMapDto>> GetTourPreviewsOnMap([FromQuery] double longitude, [FromQuery]  double latitude )
        {
            var result = _tourService.GetTourPreviewsOnMap(latitude, longitude);
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

        [HttpGet("tours-for-destination")]
        public ActionResult<PagedResult<DestinationTourDto>> GetToursForDestination([FromQuery] string city, [FromQuery] string country, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetToursForDestination(city, country, page, pageSize);
            return CreateResponse(result);
        }
    }
}

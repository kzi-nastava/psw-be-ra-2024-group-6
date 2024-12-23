using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Dtos.TourDtos.StatisticDtos;

namespace Explorer.API.Controllers.Author
{
//  [Authorize(Policy = "authorPolicy")]
    [Route("api/tours")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;


        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }



        [HttpGet]
        public ActionResult<PagedResult<TourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("author")]
        public ActionResult<List<TourDto>> GetByUserId()
        {
            long userId = User.UserId();
            var result = _tourService.GetByUserId(userId);

            return CreateResponse(result);

        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{tourId:int}")]
         public ActionResult<TourReadDto> GetTourDetailsByTourId(long tourId)
         {
             int userId = User.UserId();
             var result = _tourService.GetTourDetailsByTourId(tourId,userId);
             return CreateResponse(result);

         }

        [Authorize(Policy = "authorPolicy")]
        [HttpPost]
         public ActionResult<TourCreateDto> Create([FromBody] TourCreateDto tour)
         {
             tour.TourInfo.AuthorId = User.UserId();
             var result = _tourService.Create(tour);
             return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPatch("archive/{tourId:long}")]
        public ActionResult<TourReadDto> ArchieveTour(long tourId)
        {
            int userId = User.UserId();
            var result = _tourService.Archive(tourId, userId);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPatch("publish/{tourId:long}")]
        public ActionResult<TourReadDto> PublishTour(long tourId)
        {
            int userId = User.UserId();
            var result = _tourService.Publish(tourId, userId);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("statistics")]
        public ActionResult<AllTourStatisticsDto> GetAllTourStatistics()
        {
            int userId = User.UserId();
            var result = _tourService.GetAllTourStatistics(userId);
            return CreateResponse(result);

        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("tourStatisticsPreview")]
        public ActionResult<List<TourStatisticsPreviewDto>> GetTourStatisticsPreviews()
        {
            int userId = User.UserId();
            var result = _tourService.GetTourStatisticsPreviews(userId);
            return CreateResponse(result);

        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("tourStatistics/{tourId:long}")]
        public ActionResult<List<TourStatisticsPreviewDto>> GetTourStatistics(long tourId)
        {
            int userId = User.UserId();
            var result = _tourService.GetTourStatistics(tourId, userId);
            return CreateResponse(result);

        }

    }
}

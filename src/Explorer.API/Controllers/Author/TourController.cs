using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
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

        [HttpGet("author")]
        public ActionResult<List<TourDto>> GetByUserId()
        {
            long userId = User.UserId();
            var result = _tourService.GetByUserId(userId);

            return CreateResponse(result);

        }

         [HttpGet("{tourId:int}")]
         public ActionResult<TourDetailsDto> GetTourDetailsByTourId(int tourId)
         {
             int userId = User.UserId();
             var result = _tourService.GetTourDetailsByTourId(tourId,userId);
             return CreateResponse(result);

         }

        [HttpPost("details")]
        public ActionResult<TourCreateDto> CreateTour([FromBody]TourCreateDto tour)
        {
            tour.TourInfo.AuthorId = User.UserId();
            var result = _tourService.CreateTour(tour);
            return CreateResponse(result);

        }




        [HttpPost]
        public ActionResult<TourDto> Create([FromBody] TourDto tour)
        {
            tour.AuthorId = User.UserId();
            var result = _tourService.Create(tour);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourDto> Update([FromBody] TourDto tour)
        {
            var result = _tourService.Update(tour);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }


    }
}

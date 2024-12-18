using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/road-trips")]
    public class RoadTripController : BaseApiController
    {
        private readonly IRoadTripService _roadTripService;

        public RoadTripController(IRoadTripService roadTripService)
        {
            _roadTripService = roadTripService;
        }

        [HttpPost()]
        public ActionResult<RoadTripReadDto> Create([FromBody] RoadTripCreateDto roadTripCreate)
        {
            var result = _roadTripService.CreateRoadTrip(roadTripCreate, User.UserId());
            return CreateResponse(result);
        }
    }
}

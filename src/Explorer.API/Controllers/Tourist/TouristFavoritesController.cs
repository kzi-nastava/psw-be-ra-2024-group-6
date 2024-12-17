using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/favorites")]
    public class TouristFavoritesController : BaseApiController
    {
        private readonly ITouristFavoritesService _touristFavoritesService;

        public TouristFavoritesController(ITouristFavoritesService touristFavoritesService)
        {
            _touristFavoritesService = touristFavoritesService;
        }

        [HttpGet]
        public ActionResult<TouristFavoritesDto> GetByTouristId()
        {
            var result = _touristFavoritesService.GetByTouristId(User.UserId());
            return CreateResponse(result);
        }
    }
}

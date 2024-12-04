using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Stakeholders
{
    [Route("api/userProfile")]
    public class UserProfileController : BaseApiController
    {
        private readonly ITourService _tourService;

        public UserProfileController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("{userId:long}")]
        public ActionResult<List<TourDto>> GetByUserId(long userId)
        {
            var result = _tourService.GetByUserId(userId);

            return CreateResponse(result);

        }

    }
}

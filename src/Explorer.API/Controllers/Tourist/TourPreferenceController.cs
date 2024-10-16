using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("/api/tourist/tour-preference")]
    public class TourPreferenceController : BaseApiController
    {
        private readonly ITourPreferenceService _tourPreferenceService;
        
        public TourPreferenceController(ITourPreferenceService tourPreferenceService)
        {
            _tourPreferenceService = tourPreferenceService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPreferenceDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPreferenceService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourPreferenceDto> Create([FromBody] TourPreferenceDto tourPreference)
        {
            var result = _tourPreferenceService.Create(tourPreference);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourPreferenceDto> Update([FromBody] TourPreferenceDto tourPreference)
        {
            var result = _tourPreferenceService.Update(tourPreference);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPreferenceService.Delete(id);
            return CreateResponse(result);
        }

    }
}

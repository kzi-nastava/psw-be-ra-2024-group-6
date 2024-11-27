using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Explorer.Stakeholders.Infrastructure.Authentication;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/encounters")]
    public class EncounterTouristController : BaseApiController
    {
        
        
        private readonly IEncounterService _encounterService;

        public EncounterTouristController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet("active")]
        public ActionResult<List<EncounterReadDto>> GetAllActiveEncounters()
        {
            var result = _encounterService.GetAllActiveEncounters();
            return CreateResponse(result);
        }

        [HttpPost()]
        public ActionResult<EncounterReadDto> Create([FromBody] EncounterByTouristCreateDto encounter)
        {
            var result = _encounterService.CreateByTourist(encounter, User.UserId());
            return CreateResponse(result);
        }


    }
}

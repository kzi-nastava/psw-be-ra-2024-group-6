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
        private readonly ITouristRankService _touristRankService;

        public EncounterTouristController(IEncounterService encounterService, ITouristRankService touristRankService)
        {
            _encounterService = encounterService;
            _touristRankService = touristRankService;
        }

        [HttpGet("active")]
        public ActionResult<List<EncounterReadDto>> GetAllActiveEncounters()
        {
            var result = _encounterService.GetAllActiveEncounters();
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterReadDto> Create([FromBody] EncounterCreateDto encounter)
        {
            var result = _encounterService.CreateByTourist(encounter, User.UserId());
            return CreateResponse(result);
        }

        [HttpPost("social")]
        public ActionResult<EncounterReadDto> CreateSocialEncounter([FromBody] SocialEncounterCreateDto socialEncounter)
        {
            var result = _encounterService.CreateSocialEncounterByTourist(socialEncounter, User.UserId());
            return CreateResponse(result);
        }

        [HttpPost("hidden")]
        public ActionResult<EncounterReadDto> CreateHiddenEncounter([FromBody] HiddenEncounterDto hiddenEncounter)
        {
            var result = _encounterService.CreateByTourist(hiddenEncounter, User.UserId());
            return CreateResponse(result);
        }

        [HttpGet("all-tourist-encounters")]
        public ActionResult<EncounterReadDto> GetAllActiveTouristsEncounters()
        {
            var result = _encounterService.GetAllActiveEncountersForTourist(User.UserId());
            return CreateResponse(result);
        }

        [HttpGet("can-create-encounter")]
        public ActionResult<bool> CanTouristCreateEncounter()
        {
            var result = _touristRankService.CanCreateEncounter(User.UserId());
            return CreateResponse(result);
        }
    }
}

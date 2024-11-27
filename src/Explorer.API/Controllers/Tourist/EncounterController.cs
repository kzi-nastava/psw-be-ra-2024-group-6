using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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


    }
}

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
    public class EncounterController : BaseApiController
    {

        private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }

        [HttpGet("active")]
        public ActionResult<List<EncounterReadDto>> GetAllActiveEncounters()
        {
            var result = _encounterService.GetAllActiveEncounters();
            return CreateResponse(result);
        }

        [HttpGet]
        public ActionResult<List<EncounterReadDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetPaged(page, pageSize);
            Debug.WriteLine("IMA NAS UKUPNO");
            Debug.WriteLine(result.Value.Count);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterCreateDto> Create([FromBody] EncounterCreateDto encounter)
        {
            var result = _encounterService.Create(encounter);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EncounterCreateDto> Update([FromBody] EncounterCreateDto encounter)
        {
            var result = _encounterService.Update(encounter);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterService.Delete(id);
            return CreateResponse(result);
        }

    }
}

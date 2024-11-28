using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/encounters")]
    public class EncounterController : BaseApiController
    {
        
         private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }
        
        [HttpGet]
        public ActionResult<List<EncounterReadDto>> GetAll()
        {
            var result = _encounterService.GetPaged();
            return CreateResponse(result);
        }

        
        
        [HttpPost]
        public ActionResult<EncounterCreateDto> Create([FromBody] EncounterCreateDto encounterDto)
        {
            var result = _encounterService.Create(encounterDto);
            return CreateResponse(result);
        }
        
        [HttpPut()]
        public ActionResult<EncounterCreateDto> Update([FromBody] EncounterCreateDto encounterDto)
        {
            var result = _encounterService.Update(encounterDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            var result = _encounterService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("{encounterId}/accept")]
        public ActionResult AcceptEncounter(int encounterId)
        {
            var result = _encounterService.AcceptEncounter(encounterId);
            return CreateResponse(result);
        }

    }
}

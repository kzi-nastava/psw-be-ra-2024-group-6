using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/encounters")]
    public class EncounterController : BaseApiController
    {
        
         private readonly IEncounterService _encounterService;

        public EncounterController(IEncounterService encounterService)
        {
            _encounterService = encounterService;
        }
        
        /*[HttpGet]
        public ActionResult<PagedResult<EncounterReadDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }*/
        
        /*[HttpPost]
        public ActionResult<EncounterCreateDto> Create([FromBody] EncounterCreateDto encounterDto)
        {
            var result = _encounterService.Create(encounterDto);
            return CreateResponse(result);
        }*/
        /*
        [HttpPut("{id:long}")]
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
        */
    }
}

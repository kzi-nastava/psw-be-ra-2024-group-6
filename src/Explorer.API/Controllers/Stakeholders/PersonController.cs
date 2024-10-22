using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Explorer.API.Controllers.Stakeholders
{
    
    [Route("api/person")]
    public class PersonController : BaseApiController
    {

        private readonly IPersonService _personService;


        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<Person> GetByUserId(int userId)
        {
            var result = _personService.GetByUserId(userId);

            return CreateResponse(result);

        }

        [HttpPut]
        public ActionResult<PersonDto> Update([FromBody] PersonDto person)
        {
            var result = _personService.Update(person);
            return CreateResponse(result);
        }

    }
}

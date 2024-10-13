using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;

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
        public ActionResult<Person> Get(int userId)
        {
            var result = _personService.Get(userId);

            return CreateResponse(result);

        }

    }
}

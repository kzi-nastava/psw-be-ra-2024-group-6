using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Infrastructure.Authentication;

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

        [HttpPost("add/{followerId:int}")]
        public ActionResult AddFollower(int followerId)
        {
            var userId = User.UserId();


            var result = _personService.AddFollower(followerId, userId);
            return CreateResponse(result);

        }
        [HttpGet("{userId:int}/followers")]
        public ActionResult<List<PersonDto>> GetFollowers(int userId)
        {
            var result = _personService.GetFollowers(userId);
            return CreateResponse(result);
        }
    }
}

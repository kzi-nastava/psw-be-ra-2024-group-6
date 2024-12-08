using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.API.Controllers.Stakeholders
{

    [Route("api/person")]
    public class PersonController : BaseApiController
    {

        private readonly IPersonService _personService;
        private readonly IUserService _userService;


        public PersonController(IPersonService personService,IUserService userService)
        {
            _personService = personService;
            _userService = userService;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<Person> GetByUserId(int userId)
        {
            var result = _personService.GetByUserId(userId);

            return CreateResponse(result);

        }

        [HttpGet("user/{userId:int}")]
        public ActionResult<User> GetUser(int userId) 
        {
            var result = _userService.Get(userId);
            return CreateResponse(result);
        }

        [HttpGet("userProfile/{userId:int}")]
        public ActionResult<User> GetUserProfile(int userId)
        {
            var result = _userService.GetWithoutPassword(userId);
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

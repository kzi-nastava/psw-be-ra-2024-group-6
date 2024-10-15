using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<User> GetPaged()
        {
            var result = _userService.GetPaged();
            return Ok(result);
        }
        [HttpPut]
        public ActionResult<User> Update([FromBody] UserDto user)
        {
            var result = _userService.Update(user);
            return CreateResponse(result);
        }
    }
}

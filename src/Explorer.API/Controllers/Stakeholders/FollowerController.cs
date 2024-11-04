using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Stakeholders
{
    [Route("api/followers")]
    public class FollowerController : BaseApiController
    {
        private readonly IPersonService _personService;
        private readonly INotificationService _notificationService;

        public FollowerController(IPersonService personService, INotificationService notificationService)
        {
            _personService = personService;
            _notificationService = notificationService;
        }

        [HttpPost("add/{followerId:int}")]
        public ActionResult AddFollower(int followerId)
        {
            var userId = User.UserId();


            var result =  _personService.AddFollower(followerId,userId);
            return CreateResponse(result);
          
        }
        [HttpGet("{userId:int}")]
        public ActionResult<List<PersonDto>> GetFollowers(int userId)
        {
            var result = _personService.GetFollowers(userId);
            return CreateResponse(result);
        }

        [HttpPost("send")]
        public ActionResult SendNotification([FromBody] NotificationCreateDto notificationDto)
        {
            var userId = User.UserId(); // Pretpostavka da postoji ekstenzija koja dohvaća UserId iz tokena
            var result = _notificationService.SendNotification(notificationDto, userId);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors.First().Message);
            }

            return Ok("Notifications sent successfully");
        }

    }
}

using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Stakeholders
{
    
        [Route("api/notification")]
        public class NotificationController : BaseApiController
        {
            private readonly INotificationService _notificationService;

            public NotificationController(IPersonService personService, INotificationService notificationService)
            {
                _notificationService = notificationService;
            }



            [HttpPost("send")]
            public ActionResult SendNotification([FromBody] NotificationCreateDto notificationDto)
            {
                var userId = User.UserId(); 
                var result = _notificationService.SendNotification(notificationDto, userId);

                if (result.IsFailed)
                {
                    return BadRequest(result.Errors.First().Message);
                }

                return Ok("Notifications sent successfully");
            }

            [HttpGet("{userId:int}/notifications")]
            public ActionResult<List<NotificationDto>> GetNotifications(int userId)
            {
                 var result = _notificationService.GetNotificationsByUserId(userId);
                 return CreateResponse(result);
            }

            [HttpPut("{notificationId:int}/mark-read")]
            public ActionResult MarkNotificationAsRead(int notificationId)
            {
                var result = _notificationService.MarkAsRead(notificationId);
                if (result.IsFailed)
                {
                    return BadRequest(result.Errors.First().Message);
                }
                return Ok("Notifikacija označena kao pročitana");
            }







    }
}


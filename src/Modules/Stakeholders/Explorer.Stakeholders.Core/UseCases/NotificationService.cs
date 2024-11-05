using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.ProfileNotifications;
using System.Reflection;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository,IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public Result SendNotification(NotificationCreateDto notificationDto, int UserId)
        {
            Person follower = _personRepository.GetByUserId(UserId);
            try
            {
                List<Notification> notifications = new List<Notification>();

                foreach (var foll in follower.Followers)
                {
                    var notification = new Notification(
                        notificationDto.Content,
                        Enum.TryParse(notificationDto.Type, out NotificationType type) ? type : NotificationType.None,
                        notificationDto.LinkId,
                        foll.PersonId
                    );

                    notifications.Add(notification);
                }

                _notificationRepository.AddRange(notifications);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}

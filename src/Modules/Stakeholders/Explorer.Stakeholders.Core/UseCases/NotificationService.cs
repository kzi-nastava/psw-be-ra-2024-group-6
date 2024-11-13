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
using Explorer.Stakeholders.Core.Domain.Persons;
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
            Person sender = _personRepository.GetByUserId(UserId);
            try
            {
                List<Notification> notifications = new List<Notification>();

                foreach (var foll in sender.Followers)
                {
                    var notification = new Notification(
                        notificationDto.Content,
                        Enum.TryParse(notificationDto.Type, out NotificationType type) ? type : NotificationType.None,
                        notificationDto.LinkId,
                        foll.PersonId,sender.Id
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

        // sends notification to author or tourist, for tour issues
        public Result SendNotification(NotificationDto notificationDto)
        {
            try
            {
                _notificationRepository.Add(_mapper.Map<Notification>(notificationDto));
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<List<NotificationDto>> GetNotificationsByUserId(int userId)
        {
            try
            {
                List<Notification> notifications = _notificationRepository.GetNotificationsByUserId(userId);

                // Ako nema notifikacija, vraća se prazna lista
                return notifications.Any()
                    ? notifications.Select(notification => new NotificationDto
                    {
                        Id = notification.Id,
                        Content = notification.Content,
                        Type = notification.Type.ToString(),
                        ReceiverPersonId = notification.ReceiverPersonId,
                        SenderPersonId = notification.SenderPersonId,
                        LinkId = notification.LinkId,
                        CreatedAt = notification.CreatedAt,
                        IsRead = notification.IsRead
                    }).ToList()
                    : new List<NotificationDto>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve notifications for user with ID {userId}. Error: {ex.Message}");
            }
        }

        public Result MarkAsRead(int notificationId)
        {
            try
            {
                _notificationRepository.MarkAsRead(notificationId);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Greška prilikom označavanja notifikacije kao pročitane. Detalji: {ex.Message}");
            }
        }
    }
}

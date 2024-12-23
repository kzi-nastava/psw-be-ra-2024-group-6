using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Internal;
using AutoMapper;
using Explorer.Stakeholders.API.Public;


namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalNotificationService : IInternalNotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public InternalNotificationService(INotificationRepository notificationRepository, IPersonService personService, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _personService = personService;
            _mapper = mapper;
        }
        public Result SendWalletNotification(long recieverId,long senderId, double adventureCoins)
        {
            try
            {
                long recieverPersonId = _personService.GetByUserId((int)recieverId).Value.Id;
                long senderPersonId = _personService.GetByUserId((int)senderId).Value.Id;
                string message = $"{adventureCoins} Adventure coins have been added to your wallet.";

                NotificationDto notificationDto = new NotificationDto
                {
                    Content = message,
                    Type = "None",
                    ReceiverPersonId = recieverPersonId,
                    SenderPersonId = senderPersonId,
                    LinkId = null,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false
                };
                _notificationRepository.Add(_mapper.Map<Notification>(notificationDto));
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result SendPublicCheckpointRequestNotification(long receiverId, string message, long adminId)
        {
            try
            {
                var notification = new Notification(message, NotificationType.None, null, receiverId, adminId);
                _notificationRepository.Add(notification);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }
    }
}


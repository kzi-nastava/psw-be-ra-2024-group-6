using AutoMapper;
using Explorer.Payments.Core.UseCases.Shopping;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases;

public class InternalUserPaymentService : IInternalUserPaymentService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public InternalUserPaymentService(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    // sending notification for successfull payment
    public void SendNotification(string content, long receiverId, long linkId, bool isRead)
    {
        var notification = new NotificationDto
        {
            Content = content,
            Type = NotificationType.Tour.ToString(),
            SenderPersonId = receiverId,
            ReceiverPersonId = receiverId,
            CreatedAt = DateTime.UtcNow,
            IsRead = false,
            LinkId = linkId
        };

        _notificationRepository.Add(_mapper.Map<Notification>(notification));
    }
}

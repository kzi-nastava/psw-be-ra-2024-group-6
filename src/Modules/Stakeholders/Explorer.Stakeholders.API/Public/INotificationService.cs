using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface INotificationService
    {
        Result SendNotification(NotificationCreateDto notificationDto, int UserId);
        Result<List<NotificationDto>> GetNotificationsByUserId(int userId);
        Result MarkAsRead(int notificationId);

    }
}

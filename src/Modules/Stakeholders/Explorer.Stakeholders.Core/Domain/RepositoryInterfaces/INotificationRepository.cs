using Explorer.Stakeholders.Core.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        void Add(Notification notification);
        void AddRange(List<Notification> notifications);

        public List<Notification> GetNotificationsByUserId(int userId);

        void MarkAsRead(int notificationId);
    }
}

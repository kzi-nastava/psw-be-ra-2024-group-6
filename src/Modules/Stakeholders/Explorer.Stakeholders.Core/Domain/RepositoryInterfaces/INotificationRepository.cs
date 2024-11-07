using Explorer.Stakeholders.Core.Domain.ProfileNotifications;
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
        void AddRange(IEnumerable<Notification> notifications);

        public List<Notification> GetNotificationsByUserId(int userId);

        void MarkAsRead(int notificationId);
    }
}

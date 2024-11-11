using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class NotificationDatabaseRepository : INotificationRepository
    {
        private readonly StakeholdersContext _dbContext;

        public NotificationDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Notification notification)
        {
            try
            {
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to add notification with ID {notification.Id}. Error: {ex.Message}");
            }
        }

        public void AddRange(IEnumerable<Notification> notifications)
        {
            try
            {
                _dbContext.Notifications.AddRange(notifications);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to add multiple notifications. Error: {ex.Message}");
            }
        }
    }
}

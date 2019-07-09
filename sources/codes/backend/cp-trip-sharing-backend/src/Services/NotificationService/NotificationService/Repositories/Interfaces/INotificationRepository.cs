using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<User> GetAllReceivers(string notificationId);

        IEnumerable<Notification> GetNotifications(string userId);
    }
}

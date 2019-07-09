using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Services.Interfaces
{
    public interface INotificationService
    {
        Notification Add(Notification notification);

        IEnumerable<Notification> GetNotifications(string userId);
    }
}

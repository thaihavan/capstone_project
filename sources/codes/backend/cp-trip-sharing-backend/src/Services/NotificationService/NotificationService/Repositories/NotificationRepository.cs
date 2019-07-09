using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories.DbContext;
using NotificationService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications = null;
        private readonly IMongoCollection<User> _users = null;

        public NotificationRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _notifications = dbContext.Notifications;
            _users = dbContext.Users;
        }

        public Notification Add(Notification param)
        {
            _notifications.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllReceivers(string notificationId)
        {
            var users = _notifications.AsQueryable()
                .Where(x => x.Id.Equals(notificationId))
                .FirstOrDefault()
                .Receivers.Join(
                    _users.AsQueryable(),
                    receiverId => receiverId,
                    user => user.Id,
                    (receiverId, user) => new User
                    {
                        Id = user.Id,
                        Connections = user.Connections
                    }
                 ).ToList();
            return users;
        }

        public Notification GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return _notifications
                .Find(n => n.Receivers.Contains(userId))
                .SortByDescending(n => n.Date)
                .Limit(30)
                .ToList();
        }

        public Notification Update(Notification param)
        {
            throw new NotImplementedException();
        }
    }
}

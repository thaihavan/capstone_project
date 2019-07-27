using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories;
using NotificationService.Repositories.DbContext;
using NotificationService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotificationService.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IUserRepository _userRepository = null;
        private readonly INotificationRepository _notificationRepository = null;

        public NotificationHub(IOptions<AppSettings> settings)
        {
            _userRepository = new UserRepository(settings);
            _notificationRepository = new NotificationRepository(settings);
        }

        public void SendNotification(Notification notification)
        {
            notification.Id = ObjectId.GenerateNewId().ToString();
            notification.Date = DateTime.Now;

            _notificationRepository.Add(notification);

            var users = _notificationRepository.GetAllReceivers(notification.Id);
            foreach (var user in users)
            {
                Clients.Clients(user.Connections).SendAsync("clientNotificationListener");
            }
        }

        public void SeenNotification(string notificationId, string userId)
        {
            _notificationRepository.AddToSeenIds(notificationId, userId);
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                _userRepository.Add(new User()
                {
                    Id = userId,
                    Connections = new List<string>(new string[] { Context.ConnectionId })
                });
            }
            else
            {
                user.Connections.Add(Context.ConnectionId);
                _userRepository.Update(user);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            var user = _userRepository.GetById(userId);
            user.Connections.Remove(Context.ConnectionId);

            _userRepository.Update(user);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

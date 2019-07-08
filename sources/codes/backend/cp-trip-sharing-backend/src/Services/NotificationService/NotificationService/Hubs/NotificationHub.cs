using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotificationService.Hubs
{
    public class NotificationHub : Hub
    {
        MongoDbContext _dbContext = null;
        private readonly IMongoCollection<User> _users = null;


        public NotificationHub(IOptions<AppSettings> settings)
        {
            _dbContext = new MongoDbContext(settings);
            _users = _dbContext.Users;

        }

        public void SendNotification(string to, string message)
        {
            
        }

        public void SendToAll(string toUser, string message)
        {
            Clients.All.SendAsync("sendToAll", toUser, message);
        }

        public override Task OnConnectedAsync()
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;
            var user = _users.Find(x => x.Id == userId).FirstOrDefault();

            if (user == null)
            {
                _users.InsertOne(new User()
                {
                    Id = userId,
                    Connections = new List<string>(new string[] { Context.ConnectionId })
                });
            }
            else
            {
                user.Connections.Add(Context.ConnectionId);
                _users.FindOneAndReplace(x => x.Id.Equals(user.Id), user);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var identity = Context.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var user = _users.Find(x => x.Id == userId).FirstOrDefault();
            user.Connections.Remove(Context.ConnectionId);

            _users.FindOneAndReplace(x => x.Id.Equals(user.Id), user);
            return base.OnDisconnectedAsync(exception);
        }
    }
}

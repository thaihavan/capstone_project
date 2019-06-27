using ChatService.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ChatService.Helpers;
using System;
using MongoDB.Driver;
using ChatService.DbContext;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ChatService.HubConfig
{
    //[Authorize(Roles ="member,admin")]
    public class ChatHub : Hub
    {
        
        
        private readonly IMongoCollection<Conversation> _conversations = null;
        private readonly IMongoCollection<User> _users = null;

        public ChatHub(IOptions<AppSettings> options)
        {
            var dbContext = new MongoDbContext(options);
            _conversations = dbContext.Conversations;
            _users = dbContext.Users;
        }

        public void FindConversation(string fromUser, string toUser, string type)
        {
            var conversation = _conversations.Find(x => x.Receivers.Contains(fromUser)
            && x.Receivers.Contains(toUser)
            && type.Equals("private")).FirstOrDefault();
            Clients.Caller.SendAsync("getConversationId", conversation.Id);
        }

        public void SendToConversation(string conversationId, string message)
        {

            var connections = _conversations.AsQueryable().Where(x => x.Id.Equals(conversationId)).FirstOrDefault().Receivers.Join(
                _users.AsQueryable(),
                receiver => receiver,
                user => user.UserId,
                (receiver, user) => new
                {
                    user.Connections
                }
                ).ToList();
            foreach(var c in connections)
            {
                Clients.Clients(c.Connections).SendAsync("sendToConversation", conversationId, message);
            }
            
        }

        public void AddNewConvesation(string type, List<string> receivers)
        {
            _conversations.InsertOne(new Conversation()
            {
                Type = type,
                Receivers = receivers
            });
        }

        public override Task OnConnectedAsync()
        {

            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var userId = identity.FindFirst("user_id").Value;
            string userId = "5d0a56496d30b14fdc3be218";
            User temp = _users.Find(x => x.UserId.Equals(userId)).FirstOrDefault();
            if (temp == null)
            {
                _users.InsertOne(new User()
                {
                    UserId = userId,
                    Connections = new List<string>(new string[] { Context.ConnectionId })
                });
            }
            else
            {
                temp.Connections.Add(Context.ConnectionId);
                _users.FindOneAndReplace(x => x.UserId.Equals(temp.UserId), temp);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var userId = identity.FindFirst("user_id").Value;
            string userId = "5d0a56496d30b14fdc3be218";
            User temp = _users.Find(x => x.UserId.Equals(userId)).FirstOrDefault();
            temp.Connections.Remove(Context.ConnectionId);
            _users.FindOneAndReplace(x => x.UserId.Equals(temp.UserId), temp);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
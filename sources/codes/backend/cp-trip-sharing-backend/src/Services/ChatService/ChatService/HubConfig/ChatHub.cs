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
using MongoDB.Bson;
using ChatService.Repositories.Interfaces;
using ChatService.Repositories;

namespace ChatService.HubConfig
{
    //[Authorize(Roles = "member,admin")]
    public class ChatHub : Hub
    {
        private readonly IConversationRepository _conversationRepository = null;
        private readonly IUserRepository _userRepository = null;
        private readonly IMessageRepository _messageRepository = null;

        public ChatHub(IOptions<AppSettings> options)
        {
            var dbContext = new MongoDbContext(options);
            _conversationRepository = new ConversationRepository(options);
            _userRepository = new UserRepository(options);
            _messageRepository = new MessageRepository(options);
        }

        public void SendToConversation(string senderId, string conversationId, string message)
        {
            var messageObject = new MessageDetail()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Content = message,
                ConversationId = conversationId,
                FromUserId = senderId,
                Time = DateTime.Now
            };
            _conversationRepository.UpdateSeenIds(conversationId, new List<string>() { senderId });
            _messageRepository.Add(messageObject);

            var users = _conversationRepository.GetAllUserInConversation(conversationId);
            foreach (var user in users)
            {
                if (user.Id != senderId)
                {
                    Clients.Clients(user.Connections).SendAsync("clientMessageListener", conversationId, messageObject);
                }
            }
        }

        public void SendToReceiver(string senderId, string receiverId, string message)
        {
            var conversation = new Conversation()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Type = "private",
                Receivers = new List<string>() { senderId, receiverId }
            };

            var messageObject = new MessageDetail()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Content = message,
                ConversationId = conversation.Id,
                FromUserId = senderId,
                Time = DateTime.Now
            };

            // Add conversation to db
            _conversationRepository.Add(conversation);
            _messageRepository.Add(messageObject);

            // Send to receiver
            var receiver = _userRepository.GetById(receiverId);
            Clients.Clients(receiver.Connections).SendAsync("clientMessageListener", conversation.Id, messageObject);
        }

        public override Task OnConnectedAsync()
        {
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var userId = identity.FindFirst("user_id").Value;

            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            User user = _userRepository.GetById(userId);
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
            //var identity = (ClaimsIdentity)Context.User.Identity;
            //var userId = identity.FindFirst("user_id").Value;
            var httpContext = Context.GetHttpContext();
            var userId = httpContext.Request.Query["userId"];

            User user = _userRepository.GetById(userId);
            user.Connections.Remove(Context.ConnectionId);
            _userRepository.Update(user);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
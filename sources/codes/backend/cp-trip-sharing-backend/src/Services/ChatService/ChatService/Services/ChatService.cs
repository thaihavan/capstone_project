using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories;
using ChatService.Repositories.Interfaces;
using ChatService.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly IConversationRepository _conversationRepository = null;
        private readonly MessageRepository _messageRepository = null;

        public ChatService(IOptions<AppSettings> settings)
        {
            _conversationRepository = new ConversationRepository(settings);
            _messageRepository = new MessageRepository(settings);
        }

        public ChatService(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public MessageDetail AddMessage(string receiverId, MessageDetail message)
        {
            var conversation = _conversationRepository.FindPrivateConversationByReceiverId(receiverId);
            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Type = "private",
                    Receivers = new List<string>() { message.FromUserId, receiverId },
                    CreatedDate = DateTime.Now,
                    SeenIds = new List<string>() { message.FromUserId }
                };
                conversation = _conversationRepository.Add(conversation);
            }
            else
            {
                conversation.SeenIds = new List<string>() { message.FromUserId };
                _conversationRepository.Update(conversation);
            }
            
            message.ConversationId = conversation.Id;

            return _messageRepository.Add(message);
        }

        public bool AddToSeenIds(string conversationId, string userId)
        {
            return _conversationRepository.AddToSeenIds(conversationId, userId);
        }

        public User AddUserToGroupChat(string conversationId, string userId)
        {
            return _conversationRepository.AddUserToGroupChat(conversationId, userId);
        }

        public Conversation CreateGroupChat(Conversation conversation)
        {
            return _conversationRepository.Add(conversation);
        }

        public IEnumerable<User> GetAllMember(string conversationId)
        {
            return _conversationRepository.GetAllUserInConversation(conversationId);
        }
        public IEnumerable<MessageDetail> GetByConversationId(string id)
        {
            return _conversationRepository.GetMessageByConversationId(id);
        }

        public Conversation GetById(string id)
        {
            return _conversationRepository.GetById(id);
        }

        public IEnumerable<Conversation> GetByUserId(string id)
        {
            return _conversationRepository.GetByUserId(id);
        }

        public bool RemoveUserFromGroupChat(string conversationId, string userId)
        {
            return _conversationRepository.RemoveUserFromGroupChat(conversationId, userId);
        }
    }
}

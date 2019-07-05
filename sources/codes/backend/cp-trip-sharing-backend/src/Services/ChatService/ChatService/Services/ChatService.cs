using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories;
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
        private readonly ConversationRepository _conversationRepository = null;
        private readonly MessageRepository _messageRepository = null;

        public ChatService(IOptions<AppSettings> settings)
        {
            _conversationRepository = new ConversationRepository(settings);
            _messageRepository = new MessageRepository(settings);
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
                    LastMessage = message.Content
                };
                conversation = _conversationRepository.Add(conversation);
                message.ConversationId = conversation.Id;
            }
            else
            {
                conversation.LastMessage = message.Content;
                message.ConversationId = conversation.Id;
                _conversationRepository.Update(conversation);
            }
            return _messageRepository.Add(message);
        }

        public IEnumerable<MessageDetail> GetByConversationId(string id)
        {
            return _conversationRepository.GetMessageByConversationId(id);
        }

        public IEnumerable<Conversation> GetByUserId(string id)
        {
            return _conversationRepository.GetByUserId(id);
        }
    }
}

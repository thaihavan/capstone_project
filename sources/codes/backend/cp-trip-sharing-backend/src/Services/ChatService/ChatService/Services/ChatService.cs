using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories;
using ChatService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly ConversationRepository _conversations = null;

        public ChatService(IOptions<AppSettings> settings)
        {
            _conversations = new ConversationRepository(settings);
        }

        public IEnumerable<MessageDetail> GetByConversationId(string id)
        {
            return _conversations.GetMessageByConversationId(id);
        }

        public IEnumerable<Conversation> GetByUserId(string id)
        {
            return _conversations.GetByUserId(id);
        }
    }
}

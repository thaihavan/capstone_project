using ChatService.DbContext;
using ChatService.Helpers;
using ChatService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Repositories
{
    public class ConversationRepository
    {
        private readonly IMongoCollection<Conversation> _conversations = null;
        private readonly IMongoCollection<MessageDetail> _messages = null;

        public ConversationRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _conversations = dbContext.Conversations;
            _messages = dbContext.Messages;
        }

        public IEnumerable<MessageDetail> GetMessageByConversationId(string id)
        {
            var messages = _messages.AsQueryable()
                .Where(x => x.ConversationId.Equals(id))
                .OrderByDescending(message => message.Time)
                .Select(x => x)
                .ToList();
            return messages;
        }

        public IEnumerable<Conversation> GetByUserId(string id)
        {
            var conversations = _conversations.AsQueryable()
                .Where(x => x.Receivers.Contains(id)).ToList();
            return conversations;
        }
    }
}

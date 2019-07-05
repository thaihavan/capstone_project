using ChatService.DbContext;
using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<MessageDetail> _messages = null;

        public MessageRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _messages = dbContext.Messages;
        }

        public MessageDetail Add(MessageDetail param)
        {
            _messages.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MessageDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public MessageDetail GetById(string id)
        {
            throw new NotImplementedException();
        }

        public MessageDetail Update(MessageDetail param)
        {
            throw new NotImplementedException();
        }
    }
}

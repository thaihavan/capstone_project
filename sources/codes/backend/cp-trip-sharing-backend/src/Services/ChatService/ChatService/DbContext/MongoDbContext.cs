using ChatService.Helpers;
using ChatService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext(IOptions<AppSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            if (mongoClient != null)
            {
                _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            }
        }

        public IMongoCollection<Conversation> Conversations
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Conversation>("Conversations");
                }
                return null;
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<User>("Users");
                }
                return null;
            }
        }

        public IMongoCollection<MessageDetail> Messages
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<MessageDetail>("Messages");
                }
                return null;
            }
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationService.Helpers;
using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories.DbContext
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

        //get online users collection
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

        // Get Notifications collection
        public IMongoCollection<Notification> Notifications
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Notification>("Notifications");
                }
                return null;
            }
        }
    }

}

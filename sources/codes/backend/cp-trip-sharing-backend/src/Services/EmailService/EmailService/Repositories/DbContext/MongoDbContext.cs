using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Helpers;
using EmailService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmailService.Repositories.DbContext
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

        public IMongoCollection<Email> Emails
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Email>("Mails");
                }
                return null;
            }
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Helpers;

namespace User.Repositories.DbContext
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

        // get user collection
        public IMongoCollection<Models.User> Users
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Models.User>("Users");
                }
                return null;
            }
        }
    }
}

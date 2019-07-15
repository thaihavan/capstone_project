using ApiGateway.Helpers;
using ApiGateway.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Repositories.DbContext
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

        // Get "BlacklistTokens" collection
        public IMongoCollection<BlacklistToken> BlackListTokens
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<BlacklistToken>("BlackListTokens");
                }
                return null;
            }
        }
    }
}

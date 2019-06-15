using IdentityProvider.Helpers;
using IdentityProvider.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Repositories.DbContext
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

        // Get "accounts" collection
        public IMongoCollection<Account> Accounts
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Account>("Accounts");
                }
                return null;
            }
        }

    }
}

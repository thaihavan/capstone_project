using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.Models;

namespace PostService.Repositories.DbContext
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


        public IMongoCollection<Post> Posts
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Post>("Posts");
                }
                return null;
            }
        }
    }
}

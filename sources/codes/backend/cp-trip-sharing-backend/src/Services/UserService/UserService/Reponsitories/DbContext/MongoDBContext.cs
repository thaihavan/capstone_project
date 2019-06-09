using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServices.Helpers;
using UserServices.Models;

namespace UserServices.Reponsitories.DbContext
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDBContext(IOptions<AppSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            if(mongoClient != null)
            {
                _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            }
        }

        //Get "follows" collection from MongoDN
        public IMongoCollection<Follow> FollowsCollection
        {
            get
            {
                if(_database != null)
                {
                    return _database.GetCollection<Follow>("follows");
                }
                return null;
            }
        }

        public IMongoCollection<Bookmark> BookmarksCollection
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Bookmark>("bookmarks");
                }
                return null;
            }
        }

        public IMongoCollection<Photo> PhotosCollection
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Photo>("photos");
                }
                return null;
            }
        }
    }
}

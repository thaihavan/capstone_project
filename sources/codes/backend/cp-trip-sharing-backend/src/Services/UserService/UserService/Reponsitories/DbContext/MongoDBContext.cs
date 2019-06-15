using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServices.Helpers;
using UserServices.Models;

namespace UserServices.Reponsitories.DbContext
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDbContext()
        {
            var settings = _readAppSettings();
            var mongoClient = new MongoClient(settings.ConnectionString);
            if (mongoClient != null)
            {
                _database = mongoClient.GetDatabase(settings.DatabaseName);
            }
        }

        private AppSettings _readAppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var appSettings = configurationBuilder.Build().GetSection("AppSettings");

            return new AppSettings()
            {
                ConnectionString = appSettings.GetSection("ConnectionString").Value,
                DatabaseName = appSettings.GetSection("DatabaseName").Value
            };
        }

        //Get "follows" collection from MongoDN
        public IMongoCollection<Follow> FollowCollection
        {
            get
            {
                if(_database != null)
                {
                    return _database.GetCollection<Follow>("Follows");
                }
                return null;
            }
        }

        public IMongoCollection<Bookmark> BookmarkCollection
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Bookmark>("Bookmarks");
                }
                return null;
            }
        }

        public IMongoCollection<Photo> PhotoCollection
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Photo>("Photos");
                }
                return null;
            }
        }

        public IMongoCollection<Block> BlockCollection
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Block>("Blocks");
                }
                return null;
            }
        }

        // get user collection
        public IMongoCollection<User> Users
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

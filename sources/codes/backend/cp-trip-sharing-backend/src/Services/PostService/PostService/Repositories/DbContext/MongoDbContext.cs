using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostService.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PostService.Repositories.DbContext
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

        public IMongoCollection<VirtualTrip> VirtualTrips
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<VirtualTrip>("VirtualTrips");
                }
                return null;
            }
        }
    }
}

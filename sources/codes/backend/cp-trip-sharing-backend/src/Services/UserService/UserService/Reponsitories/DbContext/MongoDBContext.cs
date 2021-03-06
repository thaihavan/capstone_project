﻿using System;
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

        public MongoDbContext(IOptions<AppSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            if (mongoClient != null)
            {
                _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
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

        public IMongoCollection<Report> Reports
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Report>("ReportedUsers");
                }
                return null;
            }
        }

        public IMongoCollection<ReportType> ReportTypes
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<ReportType>("ReportUserTypes");
                }
                return null;
            }
        }
    }
}

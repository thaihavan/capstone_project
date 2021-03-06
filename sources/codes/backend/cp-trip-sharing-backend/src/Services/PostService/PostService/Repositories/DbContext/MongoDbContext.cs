﻿using Microsoft.Extensions.Options;
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


        public IMongoCollection<VirtualTrip> VirtualTrips
        {
            get
            {
                if(_database != null)
                {
                    return _database.GetCollection<VirtualTrip>("VirtualTrips");
                }
                return null;
            }
        }

        public IMongoCollection<Article> Articles
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Article>("Articles");
                }
                return null;
            }
        }

        public IMongoCollection<Like> Likes
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Like>("Likes");
                }
                return null;
            }
        }

        public IMongoCollection<Comment> Comments
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Comment>("Comments");
                }
                return null;
            }
        }

        public IMongoCollection<Topic> Topics
        {
            get
            {
                if(_database != null)
                {
                    return _database.GetCollection<Topic>("Topics");
                }
                return null;
            }
        }

        public IMongoCollection<Author> Authors
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<Author>("Authors");
                }
                return null;
            }
        }

        public IMongoCollection<CompanionPost> CompanionPosts
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<CompanionPost>("CompanionPosts");
                }
                return null;
            }
        }

        public IMongoCollection<CompanionPostJoinRequest> CompanionPostJoinRequests
        {
            get
            {
                if (_database != null)
                {
                    return _database.GetCollection<CompanionPostJoinRequest>("CompanionPostJoinRequests");
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
                    return _database.GetCollection<Report>("Reports");
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
                    return _database.GetCollection<ReportType>("ReportTypes");
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
    }
}

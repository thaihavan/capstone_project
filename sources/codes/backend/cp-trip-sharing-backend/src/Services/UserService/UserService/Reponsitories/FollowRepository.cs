﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories
{
    public class FollowRepository : IRepository<Follow>
    {
        private readonly IMongoCollection<Follow> _follows = null;

        public FollowRepository()
        {
            var dbContext = new MongoDbContext();
            _follows = dbContext.FollowCollection;
        }

        public Follow Add(Follow follow)
        {
            _follows.InsertOne(follow);
            return follow;
        }

        public Follow GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Follow> GetAll()
        {
            List<Follow> follows = _follows.Find(follow => true).ToList();
            return follows;
        }

        public Follow Unfollow(Follow follow)
        {
            _follows.DeleteOne(temp => temp.Follower.Equals(follow.Follower) && temp.Following.Equals(follow.Following));
            return follow;
        }

        public Follow Update(Follow document)
        {
            throw new NotImplementedException();
        }

        public Follow Delete(Follow document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Follow> GetAll(string id)
        {
            throw new NotImplementedException();
        }
    }
}
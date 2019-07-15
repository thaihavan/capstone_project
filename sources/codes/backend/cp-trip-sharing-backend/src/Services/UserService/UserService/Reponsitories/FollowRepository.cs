using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories.Interfaces
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IMongoCollection<Follow> _follows = null;
        private readonly IMongoCollection<User> _user = null;

        public FollowRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _follows = dbContext.FollowCollection;
            _user = dbContext.Users;
        }

        public Follow Add(Follow follow)
        {
            if (IsFollowed(follow.Follower, follow.Following))
            {
                return null;
            }
            _follows.InsertOne(follow);

            return follow;
        }

        public Follow Delete(Follow document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Follow> GetAll()
        {
            List<Follow> follows = _follows.Find(follow => true).ToList();
            return follows;
        }

        public Follow GetById(string id)
        {
            throw new NotImplementedException();
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

        public IEnumerable<User> GetAllFollower(string userId)
        {
            return _follows.AsQueryable().Where(x => x.Following.Equals(userId)).Join(
                _user.AsQueryable(),
                follow => follow.Follower,
                user => user.Id,
                (follow, user) => user
                ).ToList();
        }

        public IEnumerable<User> GetAllFollowing(string userId)
        {
            return _follows.AsQueryable().Where(x => x.Follower.Equals(userId)).Join(
                _user.AsQueryable(),
                follow => follow.Following,
                user => user.Id,
                (follow, user) => user
                ).ToList();
        }

        public bool IsFollowed(string follower, string following)
        {
            return _follows.CountDocuments(x => x.Follower.Equals(follower) && x.Following.Equals(following)) > 0 ? true : false;
        }

        public List<string> GetAllFollowingId(string userId)
        {
            return _follows.AsQueryable()
                .Where(x => x.Follower.Equals(userId))
                .Select(x => x.Following)
                .ToList();
        }

        public List<string> GetAllFollowerId(string userId)
        {
            return _follows.AsQueryable()
                .Where(x => x.Following.Equals(userId))
                .Select(x => x.Follower)
                .ToList();
        }
    }
}

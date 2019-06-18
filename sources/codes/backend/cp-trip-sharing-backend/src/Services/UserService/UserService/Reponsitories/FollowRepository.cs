using Microsoft.Extensions.Options;
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
        
        public FollowRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _follows = dbContext.FollowCollection;
        }

        public Follow Add(Follow follow)
        {
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

        public IEnumerable<Follow> GetAll(string id)
        {
            throw new NotImplementedException();
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
    }
}

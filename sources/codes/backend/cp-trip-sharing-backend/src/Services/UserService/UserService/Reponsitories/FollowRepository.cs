using Microsoft.Extensions.Options;
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

        public FollowRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDBContext(settings);
            _follows = dbContext.FollowsCollection;
        }

        public bool Add(Follow follows)
        {
            _follows.InsertOne(follows);
            return true;
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

        public bool Unfollow(Follow follows)
        {
            return _follows.DeleteOne(follow => follow.Follower.Equals(follows.Follower) && follow.Following.Equals(follows.Following)).IsAcknowledged;
        }

        public bool Update(Follow document)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Follow document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Follow> GetAll(string id)
        {
            throw new NotImplementedException();
        }
    }
}

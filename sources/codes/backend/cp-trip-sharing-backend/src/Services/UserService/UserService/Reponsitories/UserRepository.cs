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
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users = null;

        public UserRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _users = dbContext.Users;
        }
        
        public User Add(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public User Delete(User document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = _users.Find(x => true).ToList();
            return users;
        }

        public User GetById(string id)
        {
            var user = _users.Find(x => x.Id.Equals(new BsonObjectId(new ObjectId(id)))).FirstOrDefault();
            return user;
        }

        public User Update(User user)
        {
            var result = _users.ReplaceOne(u => u.Id.Equals(user.Id), user);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return user;
        }
    }
}

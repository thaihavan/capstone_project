using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IMongoCollection<User> _users = null;
        public UserRepository()
        {
            var dbContext = new MongoDbContext();
            _users = dbContext.Users;
        }

        public User Add(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = _users.Find(x => true).ToList();
            return users;
        }

        public User GetById(string id)
        {
            var user = _users.Find(x => x.Id.Equals(id)).FirstOrDefault();
            return user;
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }

        public User Delete(User document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll(string id)
        {
            throw new NotImplementedException();
        }

    }
}

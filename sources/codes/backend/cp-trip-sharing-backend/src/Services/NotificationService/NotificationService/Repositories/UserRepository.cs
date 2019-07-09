using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories.DbContext;
using NotificationService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users = null;

        public UserRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _users = dbContext.Users;
        }

        public User Add(User param)
        {
            _users.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            return _users.Find(x => x.Id == id).FirstOrDefault();
        }

        public User Update(User param)
        {
            var user = _users.FindOneAndReplace(x => x.Id.Equals(param.Id), param);
            return user;
        }
    }
}

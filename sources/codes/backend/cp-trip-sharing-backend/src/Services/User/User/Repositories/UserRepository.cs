using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Helpers;
using User.Models;
using User.Repositories.DbContext;

namespace User.Repositories
{
    public class UserRepository : IRepository<Models.User>
    {
        private readonly IMongoCollection<Models.User> _users = null;

        public UserRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _users = dbContext.Users;
        }

        public Models.User Add(Models.User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public IEnumerable<Models.User> GetAll()
        {
            List<Models.User> users = _users.Find(x => true).ToList();
            return users;
        }

        public Models.User GetById(string id)
        {
            var user = _users.Find(x => x.Id.Equals(id)).FirstOrDefault();
            return user;
        }

        public Models.User Update(Models.User user)
        {
            throw new NotImplementedException();
        }

        public Models.User GetUserById(string id)
        {
            return _users.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}

using ChatService.DbContext;
using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Repositories
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
            return _users.AsQueryable().Where(u => u.Id == id).FirstOrDefault();
        }

        public User InsertOrUpdate(User user)
        {
            var updateDefinition = Builders<User>.Update
                .Set("display_name", user.DisplayName)
                .Set("profile_image", user.ProfileImage);

            var result = _users.UpdateOne(
                a => a.Id == user.Id,
                updateDefinition,
                new UpdateOptions { IsUpsert = true });

            if (!result.IsAcknowledged)
            {
                return null;
            }
            return user;
        }

        public User Update(User param)
        {
            _users.FindOneAndReplace(x => x.Id.Equals(param.Id), param);
            return param;
        }
    }
}

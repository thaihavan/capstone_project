using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories.DbContext;
using IdentityProvider.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Repositories
{
    public class AccountRepository : IRepository<Account>
    {

        
        private readonly IMongoCollection<Account> _accounts = null;

        public AccountRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _accounts = dbContext.Accounts;
        }

        public bool Add(Account account)
        {
            _accounts.InsertOne(account);
            return true;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Account Get(string id)
        {
            Account account = _accounts.Find(Builders<Account>.Filter.Eq("_id",ObjectId.Parse(id))).ToList().FirstOrDefault();
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = _accounts.Find(account => true).ToList();
            return accounts;
        }

        public bool Update(Account account)
        {
            _accounts.FindOneAndReplace(
                Builders<Account>.Filter.Eq("_id", account.Id),
                account);
            return true;
        }

        public Account GetByEmail(string email)
        {      
            return _accounts.Find(account => account.Email.Equals(email)).FirstOrDefault();
        }
      
    }
}

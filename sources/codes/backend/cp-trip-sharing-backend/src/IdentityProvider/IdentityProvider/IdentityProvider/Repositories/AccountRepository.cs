using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories.DbContext;
using Microsoft.Extensions.Options;
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
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Account Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = _accounts.Find(account => true).ToList();
            return accounts;
        }

        public bool Update(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetByEmail(string email)
        {
            return _accounts.Find(account => account.Email.Equals(email)).FirstOrDefault();
        }
    }
}

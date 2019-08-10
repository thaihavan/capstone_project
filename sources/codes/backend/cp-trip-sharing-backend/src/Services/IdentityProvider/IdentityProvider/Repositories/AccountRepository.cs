using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories.DbContext;
using IdentityProvider.Repositories.Interfaces;
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
    public class AccountRepository : IAccountRepository
    {


        private readonly IMongoCollection<Account> _accounts = null;

        public AccountRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _accounts = dbContext.Accounts;
        }

        public AccountRepository()
        {
        }

        public Account Add(Account account)
        {
            _accounts.InsertOne(account);
            return account;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Account Get(string id)
        {
            Account account = _accounts.Find(x => x.Id == id).ToList().FirstOrDefault();
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            List<Account> accounts = _accounts.Find(account => true).ToList();

            return accounts;
        }

        public Account Update(Account account)
        {
            _accounts.FindOneAndReplace(a => a.Id == account.Id, account);

            return account;
        }

        public Account GetByEmail(string email)
        {
            return _accounts.Find(account => account.Email.Equals(email)).FirstOrDefault();
        }

        public Account GetByGoogleId(string id)
        {
            return _accounts.Find(x => x.GoogleId.Equals(id)).FirstOrDefault();
        }

        public Account GetByFacebookId(string id)
        {
            return _accounts.Find(x => x.FaceBookId.Equals(id)).FirstOrDefault();
        }
    }
}

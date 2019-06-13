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

        private static Random random = new Random();
        private readonly IMongoCollection<Account> _accounts = null;

        public AccountRepository()
        {
            var dbContext = new MongoDbContext();
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
            throw new NotImplementedException();
        }

        public Account GetByEmail(string email)
        {      
            return _accounts.Find(account => account.Email.Equals(email)).FirstOrDefault();
        }

        public bool ChangePassword(string userId, string newPassword) {
            //Get account salt from db 
            var salt = _accounts.Find(x => x.UserId.Equals(userId)).FirstOrDefault().PasswordSalt;
            // Generate new encrypted password for db
            var newEncryptedPassword = Hash.HashPassword(newPassword, salt);

            _accounts.FindOneAndUpdate(
                Builders<Account>.Filter.Eq("UserId", userId),
                Builders<Account>.Update.Set("Password", newEncryptedPassword)
                );
            return true;
        }

        public bool ResetPassword(string email)
        {
            var salt = _accounts.Find(x => x.Email.Equals(email)).FirstOrDefault().PasswordSalt;
            // Generate new encrypted password for db
            var newEncryptedPassword = Hash.HashPassword(GenerateRandomPassword(), salt);

            _accounts.FindOneAndUpdate(
                Builders<Account>.Filter.Eq("Email", email),
                Builders<Account>.Update.Set("Password", newEncryptedPassword)
                );
            return true;
        }

        // Generate new random password to reset password
        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

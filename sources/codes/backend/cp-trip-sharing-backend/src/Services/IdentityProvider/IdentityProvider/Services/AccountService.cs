using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories;
using IdentityProvider.Services.Interfaces;
using IdentityProvider.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace IdentityProvider.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public AccountService(IOptions<AppSettings> settings)
        {
            _accountRepository = new AccountRepository();
            _settings = settings;
        }

        public Account Authenticate(string email, string password)
        {
            var account = _accountRepository.GetByEmail(email);
            if (account != null)
            {
                var isValid = Hash.HashPassword(password, account.PasswordSalt) == account.Password;
                if (isValid)
                {
                    account.UserId = new BsonObjectId(ObjectId.GenerateNewId());
                    account.Token = JwtToken.Generate(_settings.Value.Secret, account);
                }
                // Set important fields to null
                account.Password = null;
                account.PasswordSalt = null;
            }

            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public bool Register(Account account)
        {
            if (_accountRepository.GetByEmail(account.Email) != null) return false;
            var salt = Salt.Generate();
            var encryptedAccount = new Account()
            {
                Username = account.Username,
                Email = account.Email,
                Password=Hash.HashPassword(account.Password, salt),
                PasswordSalt=salt,
                Role="member",
                UserId=new BsonObjectId(ObjectId.GenerateNewId())
            };
            return _accountRepository.Add(encryptedAccount);
        }

        public bool ChangePassword(string accountId,string oldPassword, string newPassword) {
            return _accountRepository.ChangePassword(accountId, oldPassword, newPassword);
        }

        public bool ResetPassword(string email)
        {
            return _accountRepository.ResetPassword(email);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories;
using IdentityProvider.Repositories.Interfaces;
using IdentityProvider.Services.Interfaces;
using IdentityProvider.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace IdentityProvider.Services
{
    public class AccountService : IAccountService
    {
        private static Random _random = new Random();

        private readonly IAccountRepository _accountRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public AccountService(IOptions<AppSettings> settings)
        {
            _accountRepository = new AccountRepository(settings);
            _settings = settings;
        }

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountService(IAccountRepository accountRepository,IOptions<AppSettings> settings)
        {
            _accountRepository = accountRepository;
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

        public Account Register(Account account)
        {
            if (_accountRepository.GetByEmail(account.Email) != null) return null;
            var salt = Salt.Generate();
            var encryptedAccount = new Account()
            {
                Username = account.Username,
                Email = account.Email,
                Password=Hash.HashPassword(account.Password, salt),
                PasswordSalt=salt,
                Role= "unverified",
                UserId=new BsonObjectId(ObjectId.GenerateNewId())
            };
            _accountRepository.Add(encryptedAccount);
            return encryptedAccount;
        }

        public bool ChangePassword(string accountId,string currentPassword, string newPassword) {
            var account = _accountRepository.Get(accountId);
            var salt = account.PasswordSalt;
            if (Hash.HashPassword(currentPassword, salt).Equals(account.Password))
            {
                string newEncryptedPassword = Hash.HashPassword(newPassword, salt);
                account.Password = newEncryptedPassword;
                var result = _accountRepository.Update(account);
                return result;
            }
            else return false;
        }

        public string GetResetPasswordToken(string email)
        {
            var account = _accountRepository.GetByEmail(email);
            var result=JwtToken.GenerateResetPasswordToken(_settings.Value.Secret, account);
            return result;
        }

        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public bool VerifyEmail(string id)
        {
            //get eq account and set role to member
            var account=_accountRepository.Get(id);
            account.Role = "member";
            //update account with member role
            return _accountRepository.Update(account);           
        }

        public bool ResetPassword(string accountId, string newPassword)
        {
            var account = _accountRepository.Get(accountId);
            var salt = account.PasswordSalt;
            string newEncryptedPassword = Hash.HashPassword(newPassword, salt);
            account.Password = newEncryptedPassword;
            var result = _accountRepository.Update(account);
            return result;
           
        }
    }
}

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

namespace IdentityProvider.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public AccountService(IOptions<AppSettings> settings)
        {
            _accountRepository = new AccountRepository(settings);
            _settings = settings;
        }

        public string Authenticate(string email, string password)
        {
            var token = "";
            var account = _accountRepository.GetByEmail(email);
            if (account != null)
            {
                var isValid = Hash.HashPassword(password, account.PasswordSalt) == account.Password;
                if (isValid)
                {
                    token = JwtToken.Generate(_settings.Value.Secret, account);
                }
            }

            return token;
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }
    }
}

using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();

        Account Authenticate(string email, string password);

        bool Register(Account account);

        bool ChangePassword(string userId,string oldPassword, string newPassword);

        bool ResetPassword(string email);
    }
}

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

        Task<Account> RegisterAsync(Account account);

        bool ChangePassword(string userId,string oldPassword, string newPassword);

        Task<string> GetResetPasswordTokenAsync(string email);

        string GenerateRandomPassword();

        bool VerifyEmail(string id);

        bool ResetPassword(string accountId, string newPassword);
    }
}

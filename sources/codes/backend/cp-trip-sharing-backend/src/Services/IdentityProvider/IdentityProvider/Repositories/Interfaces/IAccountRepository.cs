using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Repositories.Interfaces
{
    public interface IAccountRepository:IRepository<Account>
    {
        Account GetByEmail(string email);
        Account GetByGoogleId(string id);
        Account GetByFacebookId(string id);
    }
}

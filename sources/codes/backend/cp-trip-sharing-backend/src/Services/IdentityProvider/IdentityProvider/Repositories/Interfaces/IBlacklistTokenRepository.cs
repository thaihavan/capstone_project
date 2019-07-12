using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Repositories.Interfaces
{
    public interface IBlacklistTokenRepository:IRepository<BlacklistToken>
    {
        Task<BlacklistToken> GetTokenAsync(string token);
        Task<BlacklistToken> AddAsync(string token)
    }
}

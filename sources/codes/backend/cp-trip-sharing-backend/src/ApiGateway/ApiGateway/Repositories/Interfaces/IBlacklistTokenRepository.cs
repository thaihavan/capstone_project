using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Repositories.Interfaces
{
    public interface IBlacklistTokenRepository : IRepository<BlacklistToken>
    {
        Task<BlacklistToken> GetTokenAsync(string token);

        Task<BlacklistToken> AddAsync(string token);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Services.Interfaces
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();

        Task DeactivateCurrentAsync();

        Task<bool> IsActiveAsync(string token);

        Task DeactivateAsync(string token);
    }
}

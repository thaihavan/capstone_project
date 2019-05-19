using IdentityProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}

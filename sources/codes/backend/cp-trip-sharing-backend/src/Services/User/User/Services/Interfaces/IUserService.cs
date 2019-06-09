using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Models.User> GetAll();

        Models.User Add(Models.User user);

        Models.User GetUserByAccountId(string userId);
    }
}

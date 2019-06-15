using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User Add(User user);
        User GetUserById(string userId);

        
    }
}

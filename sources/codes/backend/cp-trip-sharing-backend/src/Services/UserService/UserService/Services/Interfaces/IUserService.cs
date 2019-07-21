using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers(string search);

        User Add(User user);

        User GetUserById(string userId);

        User Update(User user);

        object GetUserStatistics(StatisticsFilter filter);
        
        void IncreaseContributionPoint(string userId, int point);
    }
}

using System;
using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers(string search);

        object GetUserStatistics(StatisticsFilter filter);

        void IncreaseContributionPoint(string userId, int point);

        bool CheckUsername(string username);

        bool BanAnUser(string userId);
    }
}
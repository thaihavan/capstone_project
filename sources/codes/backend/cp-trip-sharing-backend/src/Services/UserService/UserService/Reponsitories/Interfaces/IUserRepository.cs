using System;
using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsers(string search);

        object GetUserStatistics(DateTime from, DateTime to);
    }
}
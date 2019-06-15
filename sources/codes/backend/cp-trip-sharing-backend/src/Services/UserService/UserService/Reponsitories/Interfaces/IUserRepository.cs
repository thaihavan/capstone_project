using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User user);
        IEnumerable<User> GetAll();
        User GetById(string id);
    }
}
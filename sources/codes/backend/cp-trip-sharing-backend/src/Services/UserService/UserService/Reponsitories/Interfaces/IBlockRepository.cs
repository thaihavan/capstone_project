using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        IEnumerable<User> GetBlockedUsers(string blockerId);
    }
}
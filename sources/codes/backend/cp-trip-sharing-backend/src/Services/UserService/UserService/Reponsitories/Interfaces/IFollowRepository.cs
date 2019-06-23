using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IFollowRepository : IRepository<Follow>
    {
        Follow Unfollow(Follow follow);
        IEnumerable<Follow> GetAllFollower(string userId);
        IEnumerable<Follow> GetAllFollowing(string userId);
    }
}
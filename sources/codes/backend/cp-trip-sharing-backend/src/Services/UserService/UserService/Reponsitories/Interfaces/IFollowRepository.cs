using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IFollowRepository : IRepository<Follow>
    {
        Follow Unfollow(Follow follow);
        IEnumerable<User> GetAllFollower(string userId);
        IEnumerable<User> GetAllFollowing(string userId);
        bool IsFollowed(string follower, string following);
        List<string> GetAllFollowingId(string userId);
        List<string> GetAllFollowerId(string userId);
    }
}
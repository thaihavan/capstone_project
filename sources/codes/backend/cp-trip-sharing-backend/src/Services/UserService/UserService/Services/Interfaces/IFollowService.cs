using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IFollowService
    {
        Follow AddFollows(Follow follow);
        Follow Unfollow(Follow follow);
        bool IsFollowed(string follower, string following);
        IEnumerable<User> GetAllFollower(string userId);
        IEnumerable<User> GetAllFollowing(string userId);
    }
}
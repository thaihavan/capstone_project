using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IFollowService
    {
        Follow AddFollows(Follow follow);
        Follow Unfollow(Follow follow);
        IEnumerable<object> GetCurrentUserFollowed(string userId, List<string> userIds);
    }
}
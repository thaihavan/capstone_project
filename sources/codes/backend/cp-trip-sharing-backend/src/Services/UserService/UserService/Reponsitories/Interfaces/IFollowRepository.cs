using System.Collections.Generic;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IFollowRepository : IRepository<Follow>
    {
        Follow Add(Follow follow);
        IEnumerable<Follow> GetAll();
        Follow Unfollow(Follow follow);
    }
}
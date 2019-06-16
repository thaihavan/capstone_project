using PostService.Models;
using System.Collections.Generic;

namespace PostService.Repositories.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        Like Add(Like param);
        bool Delete(string objectId, string userId);
        IEnumerable<Like> GetLikeWithPost();
    }
}
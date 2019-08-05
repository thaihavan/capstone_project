using PostService.Models;
using System.Collections.Generic;

namespace PostService.Repositories.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        bool Delete(string objectId, string userId);
    }
}
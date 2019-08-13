using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetPosts(string search, int page);

        bool IncreaseLikeCount(string id);

        bool DecreaseLikeCount(string id);

        bool IncreaseCommentCount(string id);

        bool DecreaseCommentCount(string id);
    }
}

using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        bool IncreaseLikeCount(string id,int likeCount);

        bool DecreaseLikeCount(string id, int likeCount);

        bool IncreaseCommentCount(string id,int commentCount);

        bool DecreaseCommentCount(string id, int commentCount);
    }
}

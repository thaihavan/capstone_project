using System.Collections.Generic;
using PostService.Models;

namespace PostService.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetCommentByPost(string postId);
        IEnumerable<Comment> GetCommentByPost(string postId,string userId);
        bool IncreaseLikeCount(string commentId);
        bool DecreaseLikeCount(string commentId);
    }
}
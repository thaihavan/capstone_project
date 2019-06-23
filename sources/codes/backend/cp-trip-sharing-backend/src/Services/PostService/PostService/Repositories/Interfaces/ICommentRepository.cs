using System.Collections.Generic;
using PostService.Models;

namespace PostService.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment Add(Comment param);
        IEnumerable<Comment> GetCommentByPost(string postId);
        bool Delete(string id);
        IEnumerable<Comment> GetAll();
        Comment GetById(string id);
        Comment Update(Comment param);
        bool IncreaseLikeCount(string commentId);
        bool DecreaseLikeCount(string commentId);
    }
}
using System.Collections.Generic;
using PostService.Models;

namespace PostService.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment Add(Comment param);
        IEnumerable<Comment> CommentDict(string postId);
        bool Delete(string id);
        IEnumerable<Comment> GetAll();
        Comment GetById(string id);
        Comment Update(Comment param);
    }
}
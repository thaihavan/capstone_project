using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface ICommentService
    {
        Comment Add(Comment cmt);
        bool Delete(string id);
        IEnumerable<Comment> GetCommentByPost(string id);
        Comment Update(Comment cmt);
        IEnumerable<Comment> GetCommentByPost(string postId, string userId);
    }
}

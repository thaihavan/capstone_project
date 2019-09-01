using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IBookmarkService
    {
        Bookmark AddBookmark(Bookmark bookmark);
        bool DeleteBookmark(string postId, string userId);
        IEnumerable<Bookmark> GetUserBookmarks(string userId);
        IEnumerable<string> GetUserBookmarkId(string id);
        Bookmark GetById(string id);
    }
}

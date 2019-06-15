using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IBookmarkService
    {
        Bookmark AddBookmark(Bookmark bookmark);
        Bookmark DeleteBookmark(Bookmark bookmark);
        Bookmark GetUserBookmark(string userId);
    }
}
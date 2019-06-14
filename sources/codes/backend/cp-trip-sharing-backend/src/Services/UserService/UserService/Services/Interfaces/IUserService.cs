using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User Add(User user);
        User GetUserById(string userId);

        Follow AddFollows(Follow follows);
        Follow Unfollow(Follow follows);

        Bookmark AddBookmark(Bookmark bookmark);
        Bookmark DeleteBookmark(Bookmark bookmark);
        Bookmark GetUserBookmark(string userId);

        Photo AddPhoto(Photo photo);
        IEnumerable<Photo> GetAllPhoto(string userId);

        Block Block(Block block);
        Block UnBlock(Block block);
    }
}

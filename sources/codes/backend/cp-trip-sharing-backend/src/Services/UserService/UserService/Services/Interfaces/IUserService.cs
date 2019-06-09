using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Follow> GetAll();

        bool AddFollows(Follow follows);
        bool Unfollow(Follow follows);

        bool AddBookmark(Bookmark bookmark);
        bool DeleteBookmark(Bookmark bookmark);
        Bookmark GetUserBookmark(string userId);

        bool AddPhoto(Photo photo);
        IEnumerable<Photo> GetAllPhotos(string userId);
    }
}

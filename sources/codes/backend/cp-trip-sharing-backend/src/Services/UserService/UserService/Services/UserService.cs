using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class UserService : IUserService
    {
        private readonly FollowRepository _followsRepository = null;
        private readonly BookmarkRepository _bookmarksRepository = null;
        private readonly PhotoRepository _photosRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public UserService(IOptions<AppSettings> settings)
        {
            _followsRepository = new FollowRepository(settings);
            _bookmarksRepository = new BookmarkRepository(settings);
            _settings = settings;
        }

        public IEnumerable<Follow> GetAll()
        {
            return _followsRepository.GetAll();
        }

        //add follows to DB
        public bool AddFollows(Follow follows)
        {
            return _followsRepository.Add(follows);
        }

        public bool Unfollow(Follow follows)
        {
            return _followsRepository.Unfollow(follows);
        }

        public bool AddBookmark(Bookmark bookmark)
        {
            return _bookmarksRepository.Add(bookmark);
        }

        public bool DeleteBookmark(Bookmark bookmark)
        {
            return _bookmarksRepository.Delete(bookmark);
        }

        public Bookmark GetUserBookmark(string userId)
        {
            return _bookmarksRepository.GetById(userId);
        }

        public bool AddPhoto(Photo photo)
        {
            return _photosRepository.Add(photo);
        }

        public IEnumerable<Photo> GetAllPhotos(string userId)
        {
            return _photosRepository.GetAll(userId);
        }
    }
}

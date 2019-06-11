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
        private readonly FollowRepository _followRepository = null;
        private readonly BookmarkRepository _bookmarkRepository = null;
        private readonly PhotoRepository _photoRepository = null;
        private readonly BlockRepository _blockRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public UserService(IOptions<AppSettings> settings)
        {
            _followRepository = new FollowRepository(settings);
            _bookmarkRepository = new BookmarkRepository(settings);
            _photoRepository = new PhotoRepository(settings);
            _settings = settings;
        }

        public IEnumerable<Follow> GetAll()
        {
            return _followRepository.GetAll();
        }

        //add follows to DB
        public bool AddFollows(Follow follows)
        {
            return _followRepository.Add(follows);
        }

        public bool Unfollow(Follow follows)
        {
            return _followRepository.Unfollow(follows);
        }

        public bool AddBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Add(bookmark);
        }

        public bool DeleteBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Delete(bookmark);
        }

        public Bookmark GetUserBookmark(string userId)
        {
            return _bookmarkRepository.GetById(userId);
        }

        public bool AddPhoto(Photo photo)
        {
            return _photoRepository.Add(photo);
        }

        public IEnumerable<Photo> GetAllPhoto(string userId)
        {
            return _photoRepository.GetAll(userId);
        }

        public bool Block(Block block)
        {
            return _blockRepository.Add(block);
        }

        public bool UnBlock(Block block)
        {
            return _blockRepository.Delete(block);
        }
    }
}

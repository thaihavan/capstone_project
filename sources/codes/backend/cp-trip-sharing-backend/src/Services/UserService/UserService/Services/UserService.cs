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
        private readonly UserRepository _userRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public UserService()
        {
            _followRepository = new FollowRepository();
            _bookmarkRepository = new BookmarkRepository();
            _photoRepository = new PhotoRepository();
        }

        public User Add(User user)
        {
            var result = _userRepository.Add(user);
            return result;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(string accountId)
        {
            return _userRepository.GetById(accountId);
        }

        //add follows to DB
        public Follow AddFollows(Follow follows)
        {
            return _followRepository.Add(follows);
        }

        public Follow Unfollow(Follow follows)
        {
            return _followRepository.Unfollow(follows);
        }

        public Bookmark AddBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Add(bookmark);
        }

        public Bookmark DeleteBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Delete(bookmark);
        }

        public Bookmark GetUserBookmark(string userId)
        {
            return _bookmarkRepository.GetById(userId);
        }

        public Photo AddPhoto(Photo photo)
        {
            return _photoRepository.Add(photo);
        }

        public IEnumerable<Photo> GetAllPhoto(string userId)
        {
            return _photoRepository.GetAll(userId);
        }

        public Block Block(Block block)
        {
            return _blockRepository.Add(block);
        }

        public Block UnBlock(Block block)
        {
            return _blockRepository.Delete(block);
        }

        
    }
}

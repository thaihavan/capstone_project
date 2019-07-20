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
        private readonly UserRepository _userRepository = null;
        private readonly IPublishToTopic _publishToTopic = null;

        public UserService(IOptions<AppSettings> settings)
        {
            _userRepository = new UserRepository(settings);
            _publishToTopic = new PublishToTopic();
        }

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Add(User user)
        {
            var result = _userRepository.Add(user);
            if (result != null)
            {
                var author = new Author()
                {
                    Id = user.Id.ToString(),
                    DisplayName = user.DisplayName,
                    ProfileImage = user.Avatar
                };

                // Comment it if run in local environment
                _publishToTopic.PublishAuthor(author);
            }
            return result;
        }

        public int GetNumberOfUser(string timePeriod)
        {
            return _userRepository.GetNumberOfUser(timePeriod);
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public IEnumerable<User> GetUsers(string search)
        {
            return _userRepository.GetUsers(search);
        }

        public void IncreaseContributionPoint(string userId, int point)
        {
            _userRepository.IncreaseContributionPoint(userId, point);
        }

        public User Update(User user)
        {
            var result = _userRepository.Update(user);
            if (result != null)
            {
                var author = new Author()
                {
                    Id = user.Id.ToString(),
                    DisplayName = user.DisplayName,
                    ProfileImage = user.Avatar
                };

                // Comment it if run in local environment
                _publishToTopic.PublishAuthor(author);
            }
            return result;
        }
    }
}

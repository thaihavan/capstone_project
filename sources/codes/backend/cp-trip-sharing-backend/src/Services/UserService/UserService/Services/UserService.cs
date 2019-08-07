using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository = null;
        private readonly IPublishToTopic _publishToTopic = null;

        public UserService(IOptions<AppSettings> settings)
        {
            _userRepository = new UserRepository(settings);
            _publishToTopic = new PublishToTopic();
        }

        public UserService(IUserRepository userRepository, IPublishToTopic publishToTopic)
        {
            _userRepository = userRepository;
            _publishToTopic = publishToTopic;
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

        public bool BanAnUser(string userId)
        {
            return _userRepository.BanAnUser(userId);
        }

        public bool CheckUsername(string username)
        {
            return this._userRepository.CheckUsername(username);
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public IEnumerable<User> GetUsers(string search)
        {
            return _userRepository.GetUsers(search);
        }

        public object GetUserStatistics(StatisticsFilter filter)
        {
            filter.From = filter.From.Date.AddHours(12);
            filter.To = filter.To.Date.AddDays(1).AddHours(12);
            return _userRepository.GetUserStatistics(filter);
        } 

        public void IncreaseContributionPoint(string userId, int point)
        {
            _userRepository.IncreaseContributionPoint(userId, point);
        }

        public bool UnBanAnUser(string userId)
        {
            return _userRepository.UnBanAnUser(userId);
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

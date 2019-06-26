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
                //_publishToTopic.PublishAuthor(author);
            }
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

        public User Update(User user)
        {
            return _userRepository.Update(user);
        }
    }
}

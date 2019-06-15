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

        public UserService(IOptions<AppSettings> settings)
        {
            _userRepository = new UserRepository(settings);
        }

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}

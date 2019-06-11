using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Helpers;
using User.Models;
using User.Repositories;
using User.Services.Interfaces;

namespace User.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository = null;

        private readonly IOptions<AppSettings> _settings = null;

        public UserService(IOptions<AppSettings> setting)
        {
            _userRepository = new UserRepository(setting);
            _settings = setting;
        }

        public Models.User Add(Models.User user)
        {
            var result = _userRepository.Add(user);
            return result;
        }

        public IEnumerable<Models.User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Models.User GetUserById(string accountId)
        {
            return _userRepository.GetUserById(accountId);
        }
    }
}

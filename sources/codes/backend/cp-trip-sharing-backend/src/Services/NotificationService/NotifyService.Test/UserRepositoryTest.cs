using Microsoft.Extensions.Options;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyServiceTest
{
    [TestFixture]
    class UserRepositoryTest
    {
        User user = null;
        UserRepository _userRepository;

        [SetUp]
        public void Config()
        {
            List<string> connections = new List<string>();
            connections.Add("5d2843264168004938e6ff9x");

            user = new User()
            {
                Id = "5d2843264168004938e6ff9a",
                Connections = connections
            };

            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-Identity"
            };
            _userRepository = new UserRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAdd()
        {
            User userReturn = _userRepository.Add(user);
            Assert.IsNotNull(userReturn);
        }

        [TestCase]
        public void TestGetById()
        {
            User userReturn = _userRepository.GetById("5d2843264168004938e6ff9a");
            Assert.IsNotNull(userReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            user.Connections.Add("5d2843264168004938e6ff9z");
            User userReturn = _userRepository.Update(user);
            Assert.IsNotNull(userReturn);
        }
    }
}

using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatService.Test
{
    [TestFixture]
    class UserRepositoryTest
    {
        User user = null;
        AppSettings _settings = null;
        UserRepository _userRepository;

        [SetUp]
        public void Config()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-Identity"
            };

            _userRepository = new UserRepository(Options.Create(_setting));
        
        List<string> connections = new List<string>();
            connections.Add("9SCYQ0tXMlQCNiOMTraN7Q");
            user = new User()
            {
                Id = "5d0b233b1a0a4200017de69c",
                Connections = connections,
                DisplayName = "phongtv",
                ProfileImage = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png"
            };

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
            User userReturn = _userRepository.GetById("5d0b233b1a0a4200017de69c");
            Assert.IsNotNull(userReturn);
        }

        [TestCase]
        public void TestInsertOrUpdate()
        {
            user.DisplayName = "PHONGTV UPDATE";
            User userReturn = _userRepository.InsertOrUpdate(user);
            Assert.IsNotNull(userReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            user.DisplayName = "PHONGTV UPDATE V2";
            User userReturn = _userRepository.Update(user);
            Assert.IsNotNull(userReturn);
        }
    }
}

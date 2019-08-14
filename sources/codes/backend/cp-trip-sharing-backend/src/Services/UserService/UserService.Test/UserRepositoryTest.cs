//using NUnit.Framework;
//using UserServices.Reponsitories;
//using UserServices.Helpers;
//using UserServices.Reponsitories.Interfaces;
//using Microsoft.Extensions.Options;
//using UserServices.Models;
//using MongoDB.Bson;
//using System;
//namespace UserService.Test
//{
//    [TestFixture]
//    public class UserRepositoryTest
//    {
//        IUserRepository _userRepository = null;
//        [SetUp]
//        public void Setup()
//        {
//            var _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-UserService"
//            };
//            _userRepository = new UserRepository(Options.Create(_setting));
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            Assert.IsNotNull(_userRepository.GetAll());
//        }

//        [TestCase]
//        public void TestGetById()
//        {
//            Assert.IsNotNull(_userRepository.GetById("5d0a176e1a0a4200017de6c5"));
//        }
//    }
//}

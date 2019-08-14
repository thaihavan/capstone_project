//using NUnit.Framework;
//using UserServices.Reponsitories;
//using UserServices.Helpers;
//using UserServices.Reponsitories.Interfaces;
//using Microsoft.Extensions.Options;
//using UserServices.Models;
//using MongoDB.Bson;

//namespace UserService.Test
//{
//    [TestFixture]
//    public class BookmarkRepositoryTest
//    {
//        IBookmarkRepository _bookmarkRepository = null;
//        [SetUp]
//        public void Setup()
//        {
//            var _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-UserService"
//            };
//            _bookmarkRepository = new BookmarkRepository(Options.Create(_setting));
//        }

//        [TestCase]
//        public void TestAddTrue()
//        {
//            var expected = new Bookmark()
//            {
//                PostId = "5d027ea59b358d247cd21a55",
//                UserId = "5d027ea59b358d247cd21a55"
//            };
//            var actual = _bookmarkRepository.Add(expected);
//            Assert.AreEqual(expected, actual);
//        }

//        [TestCase]
//        public void TestGetById()
//        {
//            var actual = _bookmarkRepository.GetById("id");
//            Assert.IsNotNull(actual);
//        }

//        [TestCase]
//        public void TestDelete()
//        {
//            var expected = new Bookmark()
//            {
//                PostId = "5d027ea59b358d247cd21a55",
//                UserId = "5d027ea59b358d247cd21a55"
//            };
//            var actual = _bookmarkRepository.Delete(expected);
//            Assert.AreEqual(expected, actual);
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            var result = _bookmarkRepository.GetAll("5d027ea59b358d247cd21a55");
//            Assert.IsNotNull(result);
//        }
//    }
//}

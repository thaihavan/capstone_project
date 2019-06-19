using NUnit.Framework;
using UserServices.Reponsitories;
using UserServices.Helpers;
using UserServices.Reponsitories.Interfaces;
using Microsoft.Extensions.Options;
using UserServices.Models;
using MongoDB.Bson;

namespace UserService.Test
{
    [TestFixture]
    public class FollowRepositoryTest
    {
        IFollowRepository _followRepository = null;
        [SetUp]
        public void Setup()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-UserService"
            };
            _followRepository = new FollowRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAdd()
        {
            var expected = new Follow()
            {
                Follower = new BsonObjectId(ObjectId.GenerateNewId()),
                Following = new BsonObjectId(ObjectId.GenerateNewId())
            };
            var actual = _followRepository.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestUnfollow()
        {
            var expected = new Follow()
            {
                Follower = new BsonObjectId(ObjectId.GenerateNewId()),
                Following = new BsonObjectId(ObjectId.GenerateNewId())
            };
            var actual = _followRepository.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestGetAllFollower()
        {
            Assert.IsNotNull(_followRepository.GetAllFollower("5d027ea59b358d247cd21a55"));
        }

        [TestCase]
        public void TestGetAllFollowing()
        {
            Assert.IsNotNull(_followRepository.GetAllFollower("5d04cc6bd9b65c318c171b7a"));
        }

    }
}

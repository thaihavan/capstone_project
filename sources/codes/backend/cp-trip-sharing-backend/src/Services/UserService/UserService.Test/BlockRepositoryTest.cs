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
    public class BlockRepositoryTest
    {
        IBlockRepository _blockRepository = null;

        [SetUp]
        public void Setup()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-UserService"
            };
            _blockRepository = new BlockRepository(Options.Create(_setting));  
        }

        [TestCase]
        public void TestAddBlockTrue()
        {
            Block expected = new Block()
            {
                BlockerId = new MongoDB.Bson.BsonObjectId(ObjectId.Parse("5d027ea59b358d247cd21a55")),
                BlockedId = new MongoDB.Bson.BsonObjectId(ObjectId.Parse("5d027ea59b358d247cd21a55"))
            };
            var actual = _blockRepository.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestUnBlock()
        {
            Block expected = new Block()
            {
                BlockerId = new MongoDB.Bson.BsonObjectId(ObjectId.Parse("5d027ea59b358d247cd21a55")),
                BlockedId = new MongoDB.Bson.BsonObjectId(ObjectId.Parse("5d027ea59b358d247cd21a55"))
            };
            var actual = _blockRepository.Delete(expected);
            Assert.AreEqual(expected, actual);
        }
    }
}
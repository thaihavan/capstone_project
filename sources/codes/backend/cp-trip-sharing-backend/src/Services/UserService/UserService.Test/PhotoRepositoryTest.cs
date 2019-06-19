using NUnit.Framework;
using UserServices.Reponsitories;
using UserServices.Helpers;
using UserServices.Reponsitories.Interfaces;
using Microsoft.Extensions.Options;
using UserServices.Models;
using MongoDB.Bson;
using System;

namespace UserService.Test
{
    [TestFixture]
    public class PhotoRepositoryTest
    {
        IPhotoRepository _photoRepository = null;
        [SetUp]
        public void Setup()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-UserService"
            };
            _photoRepository = new PhotoRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAdd()
        {
            var expected = new Photo()
            {
                Author = new BsonObjectId(ObjectId.GenerateNewId()),
                Date = DateTime.Now,
                Id = new BsonObjectId(ObjectId.GenerateNewId()),
                Url = "some url"
            };
            var actual = _photoRepository.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestCase]
        public void TestGetById()
        {
            Assert.Pass();
        }

        [TestCase]
        public void TestGetAll()
        {
            Assert.IsNotNull(_photoRepository.GetAll("5d04cc6bd9b65c318c171b7a"));
        }
    }
}

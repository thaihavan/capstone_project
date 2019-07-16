using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class LikeServiceTest
    {
        AppSettings _settings;
        Mock<ILikeRepository> mockLikeService;

        [SetUp]
        public void Config()
        {
            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            mockLikeService = new Mock<ILikeRepository>();
        }

        //[TestCase]
        //public void TestAdd()
        //{
        //    Like like = new Like()
        //    {
        //        ObjectId = "5d027ea59b358d247cd21a55",
        //        ObjectType = "post",
        //        UserId = "5d027ea59b358d247cd21a54",
        //        Date = DateTime.Now
        //    };
        //    mockLikeService.Setup(x => x.Add(It.IsAny<Like>())).Returns(like);
        //    var testService = new LikeService(Options.Create(_settings));
        //    Like result = testService.Add(like);
        //    Assert.AreEqual(like, result);
        //}

        //[TestCase]
        //public void TestDelete()
        //{
        //    string userId = "5d027ea59b358d247cd21a55";
        //    string objectId = "5d027ea59b358d247cd21a55";
        //    mockLikeService.Setup(x => x.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        //    var testService = new LikeService(Options.Create(_settings));
        //    bool result = testService.Delete(objectId, userId)
        //}

    }
}

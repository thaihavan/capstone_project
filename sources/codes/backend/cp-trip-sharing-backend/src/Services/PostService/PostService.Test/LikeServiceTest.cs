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
        Mock<ILikeRepository> mockLikeRepository;
        Mock<IPostRepository> mockPostRepository;
        Mock<ICommentRepository> mockCommentRepository;

        [SetUp]
        public void Config()
        {
            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            mockLikeRepository = new Mock<ILikeRepository>();
            mockPostRepository = new Mock<IPostRepository>();
            mockCommentRepository = new Mock<ICommentRepository>();
        }

        [TestCase]
        public void TestAddCasePost()
        {
            Like like = new Like()
            {
                ObjectId = "5d027ea59b358d247cd21a55",
                ObjectType = "post",
                UserId = "5d027ea59b358d247cd21a54",
                Date = DateTime.Now
            };
            mockPostRepository.Setup(x => x.IncreaseLikeCount(It.IsAny<string>())).Returns(true);
            mockLikeRepository.Setup(x => x.Add(It.IsAny<Like>())).Returns(like);
            var testService = new LikeService(mockLikeRepository.Object,mockPostRepository.Object, mockCommentRepository.Object);
            Like result = testService.Add(like);
            Assert.AreEqual(like, result);
        }

        [TestCase]
        public void TestAddCaseComment()
        {
            Like like = new Like()
            {
                ObjectId = "5d027ea59b358d247cd21a55",
                ObjectType = "comment",
                UserId = "5d027ea59b358d247cd21a54",
                Date = DateTime.Now
            };
            mockPostRepository.Setup(x => x.IncreaseLikeCount(It.IsAny<string>())).Returns(true);
            mockLikeRepository.Setup(x => x.Add(It.IsAny<Like>())).Returns(like);
            var testService = new LikeService(mockLikeRepository.Object, mockPostRepository.Object, mockCommentRepository.Object);
            Like result = testService.Add(like);
            Assert.AreEqual(like, result);
        }

        [TestCase]
        public void TestDeleteCasePost()
        {
            Like like = new Like()
            {
                ObjectId = "5d027ea59b358d247cd21a55",
                ObjectType = "post",
                UserId = "5d027ea59b358d247cd21a54",
                Date = DateTime.Now
            };
            mockPostRepository.Setup(x => x.DecreaseCommentCount(It.IsAny<string>())).Returns(true);
            mockLikeRepository.Setup(x => x.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var testService = new LikeService(mockLikeRepository.Object, mockPostRepository.Object, mockCommentRepository.Object);
            bool checkDelete = testService.Delete(like);
            Assert.IsTrue(checkDelete);
        }

        [TestCase]
        public void TestDeleteCaseComment()
        {
            Like like = new Like()
            {
                ObjectId = "5d027ea59b358d247cd21a55",
                ObjectType = "comment",
                UserId = "5d027ea59b358d247cd21a54",
                Date = DateTime.Now
            };
            mockPostRepository.Setup(x => x.DecreaseCommentCount(It.IsAny<string>())).Returns(true);
            mockLikeRepository.Setup(x => x.Delete(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var testService = new LikeService(mockLikeRepository.Object, mockPostRepository.Object, mockCommentRepository.Object);
            bool checkDelete = testService.Delete(like);
            Assert.IsTrue(checkDelete);
        }

    }
}

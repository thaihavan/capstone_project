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
    public class CommentServiceTest
    {
        AppSettings _setting;
        Mock<ICommentRepository> mockCommentService;

        [SetUp]
        public void Config()
        {
            _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            mockCommentService = new Mock<ICommentRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            Comment cmt = new Comment()
            {
                PostId = "5d027ea59b358d247cd21a55",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                Active = true
            };
            mockCommentService.Setup(x => x.Add(It.IsAny<Comment>())).Returns(cmt);
            var testService = new CommentService(mockCommentService.Object, Options.Create(_setting));
            Comment comment = testService.Add(cmt);
            Assert.AreEqual(cmt, comment);
        }

        [TestCase]
        public void TestDelete()
        {
            string id = "5d05eee61c9d4400005e8ba6";
            mockCommentService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var testService = new CommentService(mockCommentService.Object, Options.Create(_setting));
            bool result = testService.Delete(id);
            Assert.AreEqual(result, true);
        }

        [TestCase]
        public void TestGetCommentByPost()
        {
            Comment cmt = new Comment()
            {
                PostId = "5d027ea59b358d247cd21a55",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                Active = true
            };
            List<Comment> comments = new List<Comment>();
            comments.Add(cmt);
            mockCommentService.Setup(x => x.GetCommentByPost(It.IsAny<string>())).Returns(comments);
            var testService = new CommentService(mockCommentService.Object, Options.Create(_setting));
            IEnumerable<Comment> result = testService.GetCommentByPost("5d027ea59b358d247cd21a55");
            Assert.AreEqual(comments, result);
        }

        [TestCase]
        public void TestUpdate()
        {
            Comment cmt = new Comment()
            {
                PostId = "5d027ea59b358d247cd21a55",
                Content = "test commentservice",
                Date = DateTime.Now,
                Active = true
            };
            mockCommentService.Setup(x => x.Update(It.IsAny<Comment>())).Returns(cmt);
            var testService = new CommentService(mockCommentService.Object, Options.Create(_setting));
            Comment result = testService.Update(cmt);
            Assert.AreEqual(cmt, result);
        }

    }
}

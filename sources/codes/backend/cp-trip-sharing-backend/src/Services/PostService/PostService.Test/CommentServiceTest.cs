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
        Mock<ICommentRepository> mockCommentRepository;
        Mock<IPostRepository> mockPostRepository;
        Comment cmt;

        [SetUp]
        public void Config()
        {
            cmt = new Comment()
            {
                Id = "5d027ea59b358d247cd219a0",
                AuthorId = "5d027ea59b358d247cd219a1",
                PostId = "5d027ea59b358d247cd219a2",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                IsActive = true,
                Liked = false,
                LikeCount = 0
            };
            _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            mockCommentRepository = new Mock<ICommentRepository>();
            mockPostRepository = new Mock<IPostRepository>();
        }

        [TestCase]
        public void TestAdd()
        {           
            mockCommentRepository.Setup(x => x.Add(It.IsAny<Comment>())).Returns(cmt);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            Comment comment = testService.Add(cmt);
            Assert.AreEqual(cmt, comment);
        }

        [TestCase]
        public void TestDelete()
        {
            string id = "5d027ea59b358d247cd219a0";
            mockCommentRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(cmt);
            mockCommentRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            mockPostRepository.Setup(x => x.DecreaseCommentCount(It.IsAny<string>())).Returns(true);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            bool result = testService.Delete(id);
            Assert.AreEqual(result, true);
        }

        [TestCase]
        public void TestGetCommentByPost()
        {          
            List<Comment> comments = new List<Comment>();
            comments.Add(cmt);
            mockCommentRepository.Setup(x => x.GetCommentByPost(It.IsAny<string>())).Returns(comments);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            IEnumerable<Comment> result = testService.GetCommentByPost("5d027ea59b358d247cd219a0");
            Assert.AreEqual(comments, result);
        }

        [TestCase]
        public void TestUpdate()
        {
            mockCommentRepository.Setup(x => x.Update(It.IsAny<Comment>())).Returns(cmt);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            Comment result = testService.Update(cmt);
            Assert.AreEqual(cmt, result);
        }

        [TestCase]
        public void TestGetCommentByPostbyPostId()
        {
            IEnumerable<Comment> comments = new List<Comment>() {
               new Comment(){
                Id = "5d027ea59b358d247cd219a0",
                AuthorId = "5d027ea59b358d247cd219a1",
                PostId = "5d027ea59b358d247cd219a2",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                IsActive = true,
                Liked = false,
                LikeCount = 0
               }
            };
            mockCommentRepository.Setup(x => x.GetCommentByPost(It.IsAny<string>(), It.IsAny<string>())).Returns(comments);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            IEnumerable <Comment> ienumerableComment = testService.GetCommentByPost("postId", "userId");
            Assert.IsNotNull(ienumerableComment);
        }

    }
}

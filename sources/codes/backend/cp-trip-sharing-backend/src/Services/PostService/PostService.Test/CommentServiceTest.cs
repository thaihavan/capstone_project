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
using System.Linq;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class CommentServiceTest
    {
        AppSettings _setting;
        Mock<ICommentRepository> mockCommentRepository;
        Mock<IPostRepository> mockPostRepository;
        Comment cmt, cmtSecond = null;
        List<Comment> listChildComment = new List<Comment>();
        Author author = null;

        [SetUp]
        public void Config()
        {
            author = new Author()
            {
                Id = "5d247a04eff1030d7c5209a1",
                DisplayName = "authorName",
                ProfileImage = "profileImage"
            };           

            cmt = new Comment()
            {
                Id = "5d027ea59b358d247cd219a0",
                AuthorId = "5d027ea59b358d247cd219a1",
                PostId = "5d027ea59b358d247cd219a2",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                IsActive = true,
                Liked = false,
                LikeCount = 0,
                Author = author,
                Childs = null,
                ParentId = null
            };

            cmtSecond = new Comment()
            {
                Id = "5d027ea59b358d247cd21911",
                AuthorId = "5d027ea59b358d247cd219a12",
                PostId = "5d027ea59b358d247cd219a22",
                Content = "day la test commentservice2",
                Date = DateTime.Now,
                IsActive = true,
                Liked = false,
                LikeCount = 0,
                Author = author,
                Childs = listChildComment,
                ParentId = "5d027ea59b358d247cd219a2"
            };

            listChildComment.Add(cmtSecond);

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
            Assert.IsTrue(result);
        }

        [TestCase]
        public void TestGetCommentByPost()
        {          
            List<Comment> listComments = new List<Comment>();
            listComments.Add(cmt);
            listComments.Add(cmtSecond);
            IEnumerable<Comment> comments = listComments;
            mockCommentRepository.Setup(x => x.GetCommentByPost(It.IsAny<string>())).Returns(comments);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            IEnumerable<Comment> result = testService.GetCommentByPost("5d027ea59b358d247cd219a0");
            Comment commentActual = result.FirstOrDefault();
            Assert.AreEqual(cmt, commentActual);
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
                cmt,
                cmtSecond
            };
             
            mockCommentRepository.Setup(x => x.GetCommentByPost(It.IsAny<string>(), It.IsAny<string>())).Returns(comments);
            var testService = new CommentService(mockCommentRepository.Object, mockPostRepository.Object, Options.Create(_setting));
            IEnumerable <Comment> ienumerableComment = testService.GetCommentByPost("5d027ea59b358d247cd219a1", "5d027ea59b358d247cd21912");
            Comment commentActual = ienumerableComment.FirstOrDefault();
            Assert.AreEqual(commentActual, cmt);
        }

    }
}

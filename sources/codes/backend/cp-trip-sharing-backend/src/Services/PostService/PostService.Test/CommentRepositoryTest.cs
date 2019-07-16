using NUnit.Framework;
using PostService.Helpers;
using PostService.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using PostService.Models;
using MongoDB.Bson;

namespace PostService.Test
{
    [TestFixture]
    class CommentRepositoryTest
    {
        CommentRepository _commentRepository = null;
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
                Active = true,
                Liked = false,
                LikeCount = 0
            };
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            _commentRepository = new CommentRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAddTrue()
        {            
            Comment testAddDb = _commentRepository.Add(cmt);
            Assert.AreEqual(testAddDb, cmt);
        }

        //[TestCase]
        //public void TestDelete()
        //{
        //    bool checkdeleted = _commentRepository.Delete("5d027ea59b358d247cd219a0");
        //    Assert.IsTrue(checkdeleted);
        //}

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<Comment> comments = _commentRepository.GetAll();
            Assert.IsNotNull(comments);
        }

        [TestCase]
        public void TestGetByIdTrue()
        {
            Comment comment = _commentRepository.GetById("5d027ea59b358d247cd219a0");
            Assert.IsNotNull(comment);
        }

        [TestCase]
        public void TestGetByIdFalse()
        {
            Comment comment = _commentRepository.GetById("5d027ea59b358d247cd219a1");
            Assert.IsNull(comment);
        }

        [TestCase]
        public void TestUpdate()
        {
            cmt.Content = "Content Edit";
            Comment comment = _commentRepository.Update(cmt);
            Assert.IsNotNull(comment);
        }

        [TestCase]
        public void TestGetCommentByPost()
        {
            IEnumerable<Comment> comments = _commentRepository.GetCommentByPost("5d027ea59b358d247cd219a2");
            Assert.IsNotNull(comments);
        }

        [TestCase]
        public void TestIncreaseLikeCount()
        {
            bool checkIncreaseLikeCount = _commentRepository.IncreaseLikeCount("5d027ea59b358d247cd219a0");
            Assert.IsTrue(checkIncreaseLikeCount);
        }

        [TestCase]
        public void TestDecreaseLikeCount()
        {
            bool checkDecreaseLikeCount = _commentRepository.DecreaseLikeCount("5d027ea59b358d247cd219a0");
            Assert.IsTrue(checkDecreaseLikeCount);
        }
    }
}

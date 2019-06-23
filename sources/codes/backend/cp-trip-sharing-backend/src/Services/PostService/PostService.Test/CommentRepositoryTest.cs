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
        [SetUp]
        public void Config()
        {
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
            Comment cmt = new Comment()
            {
                PostId = "5d027ea59b358d247cd21a55",
                Content = "jkdsfjsfdfdskjfdskjnfdsnkjfdsknj",
                Date = DateTime.Parse("2019-04-05"),
                Active = true
            };
            Comment testAddDb = _commentRepository.Add(cmt);
            Assert.AreEqual(testAddDb, cmt);
        }

        [TestCase]
        public void TestGetTrue()
        {
            Comment comment = _commentRepository.GetById("5d07084a1c9d4400006ef556");
            Assert.IsNotNull(comment);
        }

        [TestCase]
        public void TestGetFalse()
        {
            Comment comment = _commentRepository.GetById("5d027ea59b358d247cd21a54");
            Assert.IsNull(comment);
        }

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<Comment> comments = _commentRepository.GetAll();
            Assert.IsNotNull(comments);
        }

        [TestCase]
        public void TestUpdate()
        {
            Comment cmt = new Comment()
            {
                PostId = "5d027ea59b358d247cd21a55",
                Content = "jkdsfjsfdfdskjffdsknj",
                Date = DateTime.Parse("2019-04-05"),
                Active = true
            };
            Assert.IsNotNull(_commentRepository.Update(cmt));
        }

        
    }
}

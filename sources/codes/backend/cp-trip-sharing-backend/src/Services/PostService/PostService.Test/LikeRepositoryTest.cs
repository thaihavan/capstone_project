using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class LikeRepositoryTest
    {
        AppSettings _settings;
        LikeRepository _likeRepository = null;

        [SetUp]
        public void Config()
        {
            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            _likeRepository = new LikeRepository(Options.Create(_settings));
        }

        [TestCase]
        public void TestAddLike()
        {
            Like like = new Like()
            {
                LikedObject = new BsonObjectId(ObjectId.Parse("5d027ea59b358d247cd21a55")),
                ObjectType = "comment",
                UserId = new BsonObjectId(ObjectId.Parse("5d04cc646c9cec228cf9bb09")),
                Date = DateTime.Now
            };
            Like testLike = _likeRepository.Add(like);
            Assert.AreEqual(testLike, like);
        }

        [TestCase]
        public void TestGetLikeWithPost()
        {
            string postId = "5d027ea59b358d247cd21a55";
            IEnumerable<Like> likes = _likeRepository.GetLikeWithPost(postId);
            Assert.IsNotNull(likes);
        }

        [TestCase]
        public void TestDelete()
        {
            string postId = "5d027ea59b358d247cd21a55";
            bool like = _likeRepository.Delete(postId);
            Assert.AreEqual(true, like);
        }
    }
}

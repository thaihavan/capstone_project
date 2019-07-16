using Microsoft.Extensions.Options;
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
    public class PostRepositoryTest
    {
        AppSettings _settings = null;
        Post post = null;
        PostRepository _postRepository = null;

        [SetUp]
        public void config()
        {
            post = new Post()
            {
                Id = "5d07d847a2c5f845707dc69a",
                Content = "<p>Post Test</p>",
                AuthorId = "5d0b2b0b1c9d440000d8e9a1",
                CommentCount = 0,
                IsActive = true,
                IsPublic = true,
                LikeCount = 0,
                CoverImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png",
                PostType = "article",
                PubDate = DateTime.Now,
                liked = false,
                Title = "Post Test",
            };

            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };

            _postRepository = new PostRepository(Options.Create(_settings));
        }

        [TestCase]
        public void TestAdd()
        {
            Post postReturn = _postRepository.Add(post);
            Assert.IsNotNull(postReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            bool checkDeleted = _postRepository.Delete("5d07d847a2c5f845707dc69a");
            Assert.IsTrue(checkDeleted);
        }

        [TestCase]
        public void TestDecreaseCommentCount()
        {
            bool checkDecreaseCommentCount = _postRepository.DecreaseCommentCount("5d07d847a2c5f845707dc69a");
            Assert.IsTrue(checkDecreaseCommentCount);
        }

        [TestCase]
        public void TestDecreaseLikeCount()
        {
            bool checkDecreaseLikeCount = _postRepository.DecreaseLikeCount("5d07d847a2c5f845707dc69a");
            Assert.IsTrue(checkDecreaseLikeCount);
        }

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<Post> posts = _postRepository.GetAll();
            Assert.IsNotNull(posts);
        }

        [TestCase]
        public void TestGetById()
        {
            Post post = _postRepository.GetById("5d07d847a2c5f845707dc69a");
            Assert.IsNotNull(post);
        }

        [TestCase]
        public void TestIncreaseCommentCount()
        {
            bool checkIncreaseCommentCount = _postRepository.IncreaseCommentCount("5d07d847a2c5f845707dc69a");
            Assert.IsTrue(checkIncreaseCommentCount);
        }

        [TestCase]
        public void TestIncreaseLikeCount()
        {
            bool checkIncreaseLikeCount = _postRepository.IncreaseLikeCount("5d07d847a2c5f845707dc69a");
            Assert.IsTrue(checkIncreaseLikeCount);
        }

        [TestCase]
        public void TestUpdate()
        {
            post.Content = "Update Cotent post";
            Post postReturn = _postRepository.Update(post);
            Assert.IsNotNull(postReturn);
        }
    }
}

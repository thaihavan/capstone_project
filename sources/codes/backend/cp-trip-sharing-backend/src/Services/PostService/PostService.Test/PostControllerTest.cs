using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class PostControllerTest
    {
        Mock<IPostService> mockPostService;
        Post post = null;
        StatisticsFilter statisticsFilter = null;

        [SetUp]
        public void Config()
        {
            mockPostService = new Mock<IPostService>();
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

            statisticsFilter = new StatisticsFilter()
            {
                To = DateTime.Now,
                From = DateTime.Parse("12/06/2019")
            };
        }

        [TestCase]
        public void TestGetAll()
        {
            //List<Post> posts = new List<Post>();
            //posts.Add(post);
            //IEnumerable<Post> iEnumerablePost = posts;
            //mockPostService.Setup(x => x.GetAll()).Returns(iEnumerablePost);
            //var _postController = new PostController(mockPostService.Object);
            //IActionResult getAllPost = _postController.GetAll();
            //var type = getAllPost.GetType();
            //Assert.AreEqual(type.Name, "OkObjectResult");
        }


        [TestCase]
        public void TestGetById()
        {
            List<Post> posts = new List<Post>();
            posts.Add(post);
            IEnumerable<Post> iEnumerablePost = posts;
            mockPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(post);
            var _postController = new PostController(mockPostService.Object);
            IActionResult getPost = _postController.GetById("asd6dssds5dasfdaf5f");
            var type = getPost.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetPostStatistics()
        {
           
        }
    }
}

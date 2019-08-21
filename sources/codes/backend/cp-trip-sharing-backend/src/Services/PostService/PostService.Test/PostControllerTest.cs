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
            Author author = new Author()
            {
                Id = "5d247a04eff1030d7c5209a1",
                DisplayName = "authorName",
                ProfileImage = "profileImage"
            };

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
                Author = author
            };

            statisticsFilter = new StatisticsFilter()
            {
                To = DateTime.Now,
                From = DateTime.Parse("12/06/2019")
            };
        }

        [TestCase]
        public void TestGetPosts()
        {
            List<Post> listPosts = new List<Post>();
            listPosts.Add(post);
            IEnumerable<Post> iEnumerablePosts = listPosts;
            mockPostService.Setup(x => x.GetPosts(It.IsAny<string>(), It.IsAny<int>())).Returns(iEnumerablePosts);
            var postController = new PostController(mockPostService.Object);
            var getPosts = postController.GetPosts("Kham pha", 1);
            var type = getPosts.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUpdatePost()
        {
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            var postController = new PostController(mockPostService.Object);
            var updatePostl = postController.UpdatePost(post);
            var type = updatePostl.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }


        [TestCase]
        public void TestUpdatePostReturnBadRequest()
        {
            Post postNull = null;
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(postNull);
            var postController = new PostController(mockPostService.Object);
            var updatePostl = postController.UpdatePost(post);
            var type = updatePostl.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
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
            List<Post> posts = new List<Post>();
            posts.Add(post);
            mockPostService.Setup(x => x.GetAllPostStatistics(It.IsAny<StatisticsFilter>())).Returns(posts);
            var _postController = new PostController(mockPostService.Object);
            IActionResult getPostStatisticst = _postController.GetPostStatistics(statisticsFilter);
            var type = getPostStatisticst.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

       [TestCase]
       public void TestGetReportedPosts()
       {
            List<Post> posts = new List<Post>();
            posts.Add(post);
            mockPostService.Setup(x => x.GetAllPostStatistics(It.IsAny<StatisticsFilter>())).Returns(posts);
            var _postController = new PostController(mockPostService.Object);
            IActionResult getPostStatisticst = _postController.GetReportedPosts(statisticsFilter);
            var type = getPostStatisticst.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
       }
    }
}

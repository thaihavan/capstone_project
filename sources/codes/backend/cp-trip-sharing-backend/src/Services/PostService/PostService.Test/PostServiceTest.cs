using Moq;
using NUnit.Framework;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class PostServiceTest
    {
        Mock<IPostRepository> mockPostRepository = null;
        Mock<IPublishToTopic> mockPublishToTopic = null;
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

            mockPostRepository = new Mock<IPostRepository>();
            mockPublishToTopic = new Mock<IPublishToTopic>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockPostRepository.Setup(x => x.Add(It.IsAny<Post>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object,mockPublishToTopic.Object);
            Post postActual= _postService.Add(post);
            Assert.AreEqual(postActual, post);
        }

        [TestCase]
        public void TestGetById()
        {
            mockPostRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object, mockPublishToTopic.Object);
            Post postActual = _postService.GetById("5d07d847a2c5f845707dc69x");
            Assert.AreEqual(postActual, post);
        }

        [TestCase]
        public void TestUpdate()
        {
            mockPostRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object, mockPublishToTopic.Object);
            Post postActual = _postService.Update(post);
            Assert.AreEqual(postActual, post);
        }

        [TestCase]
        public void TestDelete()
        {
            mockPostRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object, mockPublishToTopic.Object);
            bool checkDeleted = _postService.Delete("5d07d847a2c5f845707dc69x");
            Assert.IsTrue(checkDeleted);
        }

        [TestCase]
        public void TestGetAllPostStatistics()
        {
            Mock<IArticleRepository> mockArticleRepository = new Mock<IArticleRepository>();
            Mock<IVirtualTripRepository> mockVirtualTripRepository = new Mock<IVirtualTripRepository>();
            Mock<ICompanionPostRepository> mockCompanionPostRepository = new Mock<ICompanionPostRepository>();
            mockArticleRepository.Setup(x => x.GetArticleStatistics(It.IsAny<StatisticsFilter>())).Returns(post);
            mockVirtualTripRepository.Setup(x => x.GetVirtualTripStatistics(It.IsAny<StatisticsFilter>())).Returns(post);
            mockCompanionPostRepository.Setup(x => x.GetCompanionPostStatistics(It.IsAny<StatisticsFilter>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockVirtualTripRepository.Object, mockArticleRepository.Object, mockCompanionPostRepository.Object);
            var getAllPostStatistics = _postService.GetAllPostStatistics(statisticsFilter);
            Assert.IsNotNull(getAllPostStatistics);
        }

        [TestCase]
        public void TestGetPosts()
        {
            List<Post> listPost = new List<Post>();
            listPost.Add(post);
            IEnumerable<Post> posts = listPost;
            mockPostRepository.Setup(x => x.GetPosts(It.IsAny<string>(), It.IsAny<int>())).Returns(posts);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object, mockPublishToTopic.Object);
            IEnumerable<Post> getPostsActual = _postService.GetPosts("Kham pha",1);
            Post postActual = getPostsActual.FirstOrDefault();
            Assert.AreEqual(postActual, post);
        }
    }
}

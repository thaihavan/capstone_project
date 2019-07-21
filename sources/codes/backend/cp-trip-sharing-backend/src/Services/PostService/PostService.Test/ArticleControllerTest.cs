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
    class ArticleControllerTest
    {
        Mock<IArticleService> mockArticleService;
        Mock<IPostService> mockPostService;
        Article article = null;
        PostFilter postFilter = null;

        [SetUp]
        public void Config()
        {
            List<ArticleDestinationItem> listArticleDestination = new List<ArticleDestinationItem>();
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
            listArticleDestination.Add(new ArticleDestinationItem()
            {
                Id = "articleDestinationItemId",
                Name = "articleDestinationItemName"
            });

            Author author = new Author()
            {
                Id = "authorId",
                DisplayName = "authorName",
                ProfileImage = "profilrImage"
            };

            Post post = new Post()
            {
                Id = "postid",
                AuthorId = "authorId",
                CommentCount = 0,
                Content = "content",
                IsActive = true,
                IsPublic = true,
                CoverImage = "coverImage",
                LikeCount = 0,
                PostType = "article",
                Title = "title",
                liked = false,
                PubDate = DateTime.Parse("2019-04-05"),
                Author = author
            };

            article = new Article()
            {
                Id = "5d247a04eff1030d7c5209a0",
                PostId = "5d247a04eff1030d7c5209a1",
                Destinations = listArticleDestination,
                Post = post,
                Topics = listTopics
            };

            postFilter = new PostFilter()
            {
                LocationId = "5sd239asdd8fass7",
                Search = "ha noi",
                TimePeriod = "Tuan qua",
                Topics = listTopics
            };

            mockArticleService = new Mock<IArticleService>();
            mockPostService = new Mock<IPostService>();
        }

        [TestCase]
        public void TestGetAllArticles()
        {
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            mockArticleService.Setup(x => x.GetAllArticles(It.IsAny<PostFilter>(),It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.GetAllArticles(postFilter,4);
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllArticlesByUser()
        {
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            mockArticleService.Setup(x => x.GetAllArticlesByUser(It.IsAny<string>(), It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.GetAllArticlesByUser("as4dasdd56sdasdasd44as2",postFilter, 4);
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetRecommendArticles()
        {
            UserInfo userInfo = null;
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            mockArticleService.Setup(x => x.GetRecommendArticles(It.IsAny<PostFilter>(), It.IsAny<UserInfo>(), It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.GetRecommendArticles(postFilter, 4);
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetPopularArticles()
        {
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            mockArticleService.Setup(x => x.GetPopularArticles(It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.GetPopularArticles(postFilter, 4);
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        /*Function co User.Identity chua test */

        [TestCase]
        public void TestGetArticleById()
        {
            //Mock<ControllerContext> mockControllerContext = new Mock<ControllerContext>();
            //mockControllerContext.Setup(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            //mockArticleService.Setup(x => x.GetArticleById(It.IsAny<string>(), It.IsAny<string>())).Returns(article);
            //var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            //IActionResult getArticleById = _articleController.GetArticleById("asddadsdad90sdsd8");
            //var type = getArticleById.GetType();
        }

        [TestCase]
        public void TestRemoveArticle()
        {
            mockArticleService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.RemoveArticle("asddadsdad90sdsd8");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }


        [TestCase]
        public void TestRemoveArticleReturnNull()
        {
            mockArticleService.Setup(x => x.Delete(It.IsAny<string>())).Returns(false);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.RemoveArticle("asddadsdad90sdsd8");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetNumberOfCompanionPost()
        {
            mockArticleService.Setup(x => x.GetNumberOfArticlePost(It.IsAny<PostFilter>())).Returns(3);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.GetNumberOfCompanionPost(postFilter);
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

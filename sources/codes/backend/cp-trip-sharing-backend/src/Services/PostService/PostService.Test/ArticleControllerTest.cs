using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        Post post = null;

        [SetUp]
        public void Config()
        {
            List<ArticleDestinationItem> listArticleDestination = new List<ArticleDestinationItem>();
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
            listArticleDestination.Add(new ArticleDestinationItem()
            {
                Id = "5d247a04eff1030d7c520a2k4",
                Name = "articleDestinationItemName"
            });

            Author author = new Author()
            {
                Id = "5d247a04eff1030d7c520a287",
                DisplayName = "authorName",
                ProfileImage = "profileImage"
            };

            post = new Post()
            {
                Id = "5d247a04eff1030d7c5209a1",
                AuthorId = "5d247a04eff1030d7c520a2m32",
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
                LocationId = "5d247a04eff1030d7c520123",
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
            IActionResult getAllArticles = _articleController.GetAllArticles(postFilter,4);
            var type = getAllArticles.GetType();
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
            IActionResult getAllArticlesByUser = _articleController.GetAllArticlesByUser("5d247a04eff1030d7c520a111", postFilter, 4);
            var type = getAllArticlesByUser.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetRecommendArticlesReturnOkObjectResult()
        {
            // mock ClaimsIdentity Object
            ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, "PhongTvs"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c5209a1")
            });
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            // mock data of GetRecommendArticles() function return _iEnumerableArticle
            mockArticleService.Setup(x => x.GetRecommendArticles(It.IsAny<PostFilter>(),
                It.IsAny<UserInfo>(), It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            // get actual value of GetRecommendArticles function from ActicleController
            IActionResult getRecommendArticles = _articleController.GetRecommendArticles(postFilter, 4);
            var type = getRecommendArticles.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetPopularArticlesReturnOkObjectResults()
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

        [TestCase]
        public void TestGetArticleById()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520a222")
                });

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockArticleService.Setup(x => x.GetArticleById(It.IsAny<string>(), It.IsAny<string>())).Returns(article);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            var getArticleById = _articleController.GetArticleById("5d247a04eff1030d7c520a1212");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetArticleByIdIsAuthenticated()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520a7uu77")
                },"authenticationType");

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockArticleService.Setup(x => x.GetArticleById(It.IsAny<string>(), It.IsAny<string>())).Returns(article);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getArticleById = _articleController.GetArticleById("5d247a04eff1030d7c520ac123");
            var okObjectResult = getArticleById as OkObjectResult;
            Article articleActual = (Article) okObjectResult.Value;
            Assert.AreEqual(articleActual, article);
        }

        [TestCase]
        public void TestCreateArticle()
        {
            var controller = new ArticleController(mockArticleService.Object, mockPostService.Object);


            var contextMock = new Mock<HttpContext>();            

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520v321")
                });

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Add(It.IsAny<Post>())).Returns(article.Post);
            mockArticleService.Setup(x => x.Add(It.IsAny<Article>())).Returns(article);
         
            controller.ControllerContext.HttpContext = contextMock.Object;

            IActionResult createArticle = controller.CreateArticle(article);
            OkObjectResult okObjectResult = createArticle as OkObjectResult;
            Article articleActual = (Article)okObjectResult.Value;
            Assert.AreEqual(articleActual,article);
        }

        [TestCase]
        public void TestUpdateArticleReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            article.PostId = "5d247a04eff1030d7c520a2xxx";
            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520a2m32")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            var checkUpdateArticle = _articleController.UpdateArticle(article);
            var type = checkUpdateArticle.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestUpdateArticleReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520a987a")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            var checkUpdateArticle = _articleController.UpdateArticle(article);
            var type = checkUpdateArticle.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestUpdateArticleSuccess()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d247a04eff1030d7c520a2m32")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            mockArticleService.Setup(x => x.Update(It.IsAny<Article>())).Returns(article);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult checkUpdateArticle = _articleController.UpdateArticle(article);
            OkObjectResult okObjectResult = checkUpdateArticle as OkObjectResult;
            Article articleActual = (Article) okObjectResult.Value;
            Assert.AreEqual(articleActual.Id, "5d247a04eff1030d7c5209a0");
        }


        [TestCase]
        public void TestRemoveArticle()
        {
            mockArticleService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.RemoveArticle("5d247a04eff1030d7c520a1188");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }


        [TestCase]
        public void TestRemoveArticleReturnBadRequestResult()
        {
            mockArticleService.Setup(x => x.Delete(It.IsAny<string>())).Returns(false);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            IActionResult getArticleById = _articleController.RemoveArticle("5d247a04eff1030d7c520l654");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }
    }
}

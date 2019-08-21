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
                Id = "5d247a04eff1030d7c5209a1",
                DisplayName = "authorName",
                ProfileImage = "profileImage"
            };

            Post post = new Post()
            {
                Id = "5d247a04eff1030d7c5209a1",
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
            IActionResult getAllArticlesByUser = _articleController.GetAllArticlesByUser("as4dasdd56sdasdasd44as2",postFilter, 4);
            var type = getAllArticlesByUser.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetRecommendArticles()
        {
            ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
            });
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            List<Article> articles = new List<Article>();
            articles.Add(article);
            IEnumerable<Article> _iEnumerableArticle = articles;
            mockArticleService.Setup(x => x.GetRecommendArticles(It.IsAny<PostFilter>(), It.IsAny<UserInfo>(), It.IsAny<int>())).Returns(_iEnumerableArticle);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getRecommendArticles = _articleController.GetRecommendArticles(postFilter, 4);
            var type = getRecommendArticles.GetType();
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

        [TestCase]
        public void TestGetArticleById()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockArticleService.Setup(x => x.GetArticleById(It.IsAny<string>(), It.IsAny<string>())).Returns(article);
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            var getArticleById = _articleController.GetArticleById("asddadsdad90sdsd8");
            var type = getArticleById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
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
        public void TestCreateArticle()
        {
            var controller = new ArticleController(mockArticleService.Object, mockPostService.Object);


            var contextMock = new Mock<HttpContext>();            

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Add(It.IsAny<Post>())).Returns(article.Post);
            mockArticleService.Setup(x => x.Add(It.IsAny<Article>())).Returns(article);
         
            controller.ControllerContext.HttpContext = contextMock.Object;

            var actual = controller.CreateArticle(article);

            Assert.IsNotNull(actual);
        }

        [TestCase]
        public void TestUpdateArticleReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            article.PostId = "";
            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
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
                    new Claim("user_id","dasfafsf6sfasfasf")
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
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            var _articleController = new ArticleController(mockArticleService.Object, mockPostService.Object);
            _articleController.ControllerContext.HttpContext = contextMock.Object;
            var checkUpdateArticle = _articleController.UpdateArticle(article);
            var type = checkUpdateArticle.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

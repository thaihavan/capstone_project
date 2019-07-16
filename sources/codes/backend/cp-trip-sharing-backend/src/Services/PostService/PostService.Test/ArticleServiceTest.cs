using Moq;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class ArticleServiceTest
    {
        Mock<IArticleRepository> mockArticleRepository;
        Article article;
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
            mockArticleRepository = new Mock<IArticleRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockArticleRepository.Setup(x => x.Add(It.IsAny<Article>())).Returns(article);
            var _articleService = new ArticleService(mockArticleRepository.Object);
            Article articleReturn = _articleService.Add(article);
            Assert.IsNotNull(articleReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            mockArticleRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var _articleService = new ArticleService(mockArticleRepository.Object);
            bool checkdeleted = _articleService.Delete("abc");
            Assert.IsTrue(checkdeleted);
        }

        [TestCase]
        public void TestGetArticleById()
        {
            mockArticleRepository.Setup(x => x.GetArticleById(It.IsAny<string>(), It.IsAny<string>())).Returns(article);
            var _articleService = new ArticleService(mockArticleRepository.Object);
            Article articleReturn = _articleService.GetArticleById("articleId","userId");
            Assert.IsNotNull(articleReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            mockArticleRepository.Setup(x => x.Update(It.IsAny<Article>())).Returns(article);
            var _articleService = new ArticleService(mockArticleRepository.Object);
            Article articleReturn = _articleService.Update(article);
            Assert.IsNotNull(articleReturn);
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using PostService.Repositories;
using PostService.Helpers;
using PostService.Models;

namespace PostService.Test
{
    [TestFixture]
    public class ArticleRepositoryTest
    {
        ArticleRepository _articleRepository = null;
        [SetUp]
        public void Config()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            _articleRepository = new ArticleRepository(Options.Create(_setting));
        }

        //[TestCase]
        //public void TestAdd()
        //{
        //    List<ArticleDestinationItem> listArticleDestination = new List<ArticleDestinationItem>();
        //    List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
        //    listArticleDestination.Add(new ArticleDestinationItem()
        //    {
        //        Id = "articleDestinationItemId",
        //        Name = "articleDestinationItemName"
        //    });

        //    Author author = new Author()
        //    {
        //        Id = "authorId",
        //        DisplayName = "authorName",
        //        ProfileImage = "profilrImage"
        //    };

        //    Post post = new Post()
        //    {
        //        Id = "postid",
        //        AuthorId = "authorId",
        //        CommentCount = 0,
        //        Content = "content",
        //        IsActive = true,
        //        IsPublic = true,
        //        CoverImage = "coverImage",
        //        LikeCount = 0,
        //        PostType = "article",
        //        Title = "title",
        //        liked = false,
        //        PubDate = DateTime.Parse("2019-04-05"),
        //        Author = author
        //    };

        //    Article article = new Article()
        //    {
        //        Id = "5d247a04eff1030d7c5209a0",
        //        PostId = "5d247a04eff1030d7c5209a1",
        //        Destinations = listArticleDestination,
        //        Post = post,
        //        Topics = listTopics
        //    };
        //    Article articleReturn = _articleRepository.Add(article);
        //    Assert.IsNotNull(articleReturn);
        //}

        //[TestCase]
        //public void TestDelete()
        //{
        //    bool testDelete = _articleRepository.Delete("5d247a04eff1030d7c5209a0");
        //    Assert.IsTrue(testDelete);
        //}

        //[TestCase]
        //public void TestGetAll()
        //{
        //    IEnumerable<Article> articles = _articleRepository.GetAll();
        //    Assert.IsNotNull(articles);
        //}

        [TestCase]
        public void TestGetByIdTrue()
        {
            Article article = _articleRepository.GetById("5d25f51bee3f2f2490db3f40");
            Assert.IsNotNull(article);
        }

        //[TestCase]
        //public void TestGetByIdFalse ()
        //{
        //    Article article = _articleRepository.GetById("5d247a04eff1030d7c5209a1");
        //    Assert.IsNull(article);
        //}

        [TestCase]
        public void TestUpdate()
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

            Article article = new Article()
            {
                Id = "5d247a04eff1030d7c5209a0",
                PostId = "5d247a04eff1030d7c5209a1",
                Destinations = listArticleDestination,
                Post = post,
                Topics = listTopics
            };

            Article articleUpdated = _articleRepository.Update(article);
            Assert.IsNotNull(articleUpdated);
        }

        [TestCase]
        public void TestGetArticleById()
        {
            Article article = _articleRepository.GetArticleById("5d25f51bee3f2f2490db3f40", "5d111299f3b75e0001f4ed78");
            Assert.IsNotNull(article);
        }
    }
    
}

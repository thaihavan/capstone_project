using Moq;
using NUnit.Framework;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class PostServiceTest
    {
        Mock<IPostRepository> mockPostRepository = null;
        Post post = null;

        [SetUp]
        public void Config()
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

            mockPostRepository = new Mock<IPostRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockPostRepository.Setup(x => x.Add(It.IsAny<Post>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object);
            Post postReturn = _postService.Add(post);
            Assert.IsNotNull(postReturn);
        }

        [TestCase]
        public void TestGetAll()
        {
        //    IEnumerable<Post> posts = new List<Post>()
        //    {
        //        new Post()
        //        {
        //            Id = "5d07d847a2c5f845707dc69x",
        //            Content = "<p>Post Test</p>",
        //            AuthorId = "5d0b2b0b1c9d440000d8e9ax",
        //            CommentCount = 0,
        //            IsActive = true,
        //            IsPublic = true,
        //            LikeCount = 0,
        //            CoverImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png",
        //            PostType = "article",
        //            PubDate = DateTime.Now,
        //            liked = false,
        //            Title = "Post Test",
        //        }
        //};
        //    mockPostRepository.Setup(x => x.GetAll()).Returns(posts);
        //    PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object);
        //    IEnumerable<Post> listpost = _postService.GetAll();
        //    Assert.IsNotNull(listpost);
        }

        [TestCase]
        public void TestGetById()
        {
            mockPostRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object);
            Post postReturn = _postService.GetById("5d07d847a2c5f845707dc69x");
            Assert.IsNotNull(postReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            mockPostRepository.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object);
            Post postfake = new Post();
            Post postReturn = _postService.Update(postfake);
            Assert.IsNotNull(postReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            mockPostRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            PostService.Services.PostService _postService = new PostService.Services.PostService(mockPostRepository.Object);
            bool checkDeleted = _postService.Delete("5d07d847a2c5f845707dc69x");
            Assert.IsTrue(checkDeleted);
        }
    }
}

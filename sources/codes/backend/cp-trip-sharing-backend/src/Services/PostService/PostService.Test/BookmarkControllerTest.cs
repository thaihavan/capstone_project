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


namespace UserService.Test
{
    [TestFixture]
    class BookmarkControllerTest
    {
        Bookmark bookmark,bookmarkSecond = null;
        ClaimsIdentity claims = null;
        Mock<IBookmarkService> mockBookmarkService;
        List<Bookmark> bookmarks = new List<Bookmark>();

        [SetUp]
        public void Config()
        {
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

            bookmark = new Bookmark()
            {
                Id = "5d1da79eb7ee1f00013b2e70",
                PostId = "5d0a17701a0a4200017de6c7",
                UserId = "5d0b7d335ec06233948a5056",
                Post = post
            };

            bookmarkSecond = new Bookmark()
            {
                Id = "5d1da79eb7ee1f00013b2e702",
                PostId = "5d0a17701a0a4200017de6c72",
                UserId = "5d0b7d335ec06233948a50562",
                Post = post
            };

            bookmarks.Add(bookmark);
            bookmarks.Add(bookmarkSecond);

            claims = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","afa5fafaf4aga4g")
              });

            mockBookmarkService = new Mock<IBookmarkService>();
        }    
      
        [TestCase]
        public void TestBookmark()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.AddBookmark(It.IsAny<Bookmark>())).Returns(bookmark);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addBookmark = bookmarkController.Bookmark(bookmark);
            var type = addBookmark.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestBookmarkReturnBadRequest()
        {
            Bookmark bookmarkNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.AddBookmark(It.IsAny<Bookmark>())).Returns(bookmarkNull);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addBookmark = bookmarkController.Bookmark(bookmark);
            var type = addBookmark.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestDeleteBookmark()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.GetById(It.IsAny<string>())).Returns(bookmark);
            mockBookmarkService.Setup(x => x.DeleteBookmark(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult deleteBookmark = bookmarkController.DeleteBookmark("5d1da79eb7ee1f00013b2e70");
            var type = deleteBookmark.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }


        [TestCase]
        public void TestDeleteBookmarkReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.GetById(It.IsAny<string>())).Returns(bookmark);
            mockBookmarkService.Setup(x => x.DeleteBookmark(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult deleteBookmark = bookmarkController.DeleteBookmark("5d1da79eb7ee1f00013b2e70");
            var type = deleteBookmark.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetUserBookmarks()
        {
            IEnumerable<Bookmark> ienumableBookmark = bookmarks;          
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.GetUserBookmarks(It.IsAny<string>())).Returns(ienumableBookmark);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getUserBookmark = bookmarkController.GetUserBookmarks();
            var type = getUserBookmark.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetUserBookmarkIds()
        {
            List<String> listUserId = new List<string>();
            listUserId.Add("5d4d1fc413376b00013a898e");
            listUserId.Add("5d4d1fc413376b00013a89as");
            IEnumerable<string> ienumableUserId = listUserId;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.GetUserBookmarkId(It.IsAny<string>())).Returns(ienumableUserId);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getUserBookmarkIds = bookmarkController.GetUserBookmarkIds();
            var type = getUserBookmarkIds.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

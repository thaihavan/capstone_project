using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UserServices.Controllers;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserService.Test
{
    [TestFixture]
    class BookmarkControllerTest
    {
        Bookmark bookmark = null;
        ClaimsIdentity claims = null;
        Mock<IBookmarkService> mockBookmarkService;

        [SetUp]
        public void Config()
        {
            bookmark = new Bookmark()
            {
                Id = "5d1da79eb7ee1f00013b2e70",
                CoverImage = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907110413435104-ojo6kpvcex0qwnql.png",
                PostId = "5d0a17701a0a4200017de6c7",
                PostType = "article",
                Title = "TRƯỚC 30 TUỔI, BẠN ĐÃ TRẢI NGHIỆM ĐƯỢC BAO NHIÊU ĐIỀU DƯỚI ĐÂY RỒI?",
                UserId = "5d0b7d335ec06233948a5056",                
            };

            claims = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","afa5fafaf4aga4g")
              });

            mockBookmarkService = new Mock<IBookmarkService>();
        }

        IEnumerable<Bookmark> ienumableBookmark()
        {
            yield return bookmark;
        }

        IEnumerable<string> ienumableUserId()
        {
            yield return "5d0b7d335ec06233948a5056";
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
            mockBookmarkService.Setup(x => x.DeleteBookmark(It.IsAny<Bookmark>())).Returns(bookmark);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult deleteBookmark = bookmarkController.DeleteBookmark("5d1da79eb7ee1f00013b2e70");
            var type = deleteBookmark.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }


        [TestCase]
        public void TestDeleteBookmarkReturnBadRequest()
        {
            Bookmark boorkmarkNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBookmarkService.Setup(x => x.DeleteBookmark(It.IsAny<Bookmark>())).Returns(boorkmarkNull);
            var bookmarkController = new BookmarkController(mockBookmarkService.Object);
            bookmarkController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult deleteBookmark = bookmarkController.DeleteBookmark("5d1da79eb7ee1f00013b2e70");
            var type = deleteBookmark.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetUserBookmarks()
        {
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

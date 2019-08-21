using Moq;
using NUnit.Framework;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserService.Test
{
    [TestFixture]
    class BookmarkServiceTest
    {
        Mock<IBookmarkRepository> mockBookmarkRepository;
        Bookmark bookmark, bookmarkSecond = null;
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

            mockBookmarkRepository = new Mock<IBookmarkRepository>();
        }

        [TestCase]
        public void TestAddBookmark()
        {
            mockBookmarkRepository.Setup(x => x.Add(It.IsAny<Bookmark>())).Returns(bookmark);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            Bookmark bookmarkReturn = bookmarkService.AddBookmark(bookmark);
            Assert.AreEqual(bookmarkReturn, bookmarkReturn);
        }

        [TestCase]
        public void TestDeleteBookmark()
        {
            mockBookmarkRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            bool deleteBookmark = bookmarkService.DeleteBookmark("5d0a17701a0a4200017de6c7");
            Assert.IsTrue(deleteBookmark);
        }

        [TestCase]
        public void TestGetById()
        {
            mockBookmarkRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(bookmark);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            Bookmark bookmarkActual = bookmarkService.GetById("5d0a17701a0a4200017de6c7");
            Assert.AreEqual(bookmarkActual, bookmark);
        }

        [TestCase]
        public void TestGetUserBookmarkId()
        {
            List<String> listUserId = new List<string>();
            listUserId.Add("5d4d1fc413376b00013a898e");
            listUserId.Add("5d4d1fc413376b00013a89as");
            IEnumerable<string> ienumableGetUserBookmarkId = listUserId;
            mockBookmarkRepository.Setup(x => x.GetUserBookmarkId(It.IsAny<string>())).Returns(ienumableGetUserBookmarkId);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            IEnumerable<string> ienumableReturn = bookmarkService.GetUserBookmarkId("5d0a17701a0a4200017de6c7");
            string userIdActual = ienumableReturn.FirstOrDefault();
            Assert.AreEqual(userIdActual, "5d4d1fc413376b00013a898e");
        }

        [TestCase]
        public void TestGetUserBookmarks()
        {
            IEnumerable<Bookmark> ienumableGetAllBookmark = bookmarks;
            mockBookmarkRepository.Setup(x => x.GetAll(It.IsAny<string>())).Returns(ienumableGetAllBookmark);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            IEnumerable<Bookmark> ienumableReturn = bookmarkService.GetUserBookmarks("5d0a17701a0a4200017de6c7");
            Bookmark bookmarkActual = ienumableGetAllBookmark.FirstOrDefault();
            Assert.AreEqual(bookmarkActual.Id, "5d1da79eb7ee1f00013b2e70");
        }
    }
}

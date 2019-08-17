using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UserServices.Models;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services;

namespace UserService.Test
{
    [TestFixture]
    class BookmarkServiceTest
    {
        Mock<IBookmarkRepository> mockBookmarkRepository;
        Bookmark bookmark = null;

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
            mockBookmarkRepository = new Mock<IBookmarkRepository>();
        }

        public IEnumerable<string> ienumableGetUserBookmarkId()
        {
            string[] users = { "5d0a17701a0a4200017de6c7", "5d0a17701a0a4200017de6c7" };
            foreach(var user in users)
            yield return user;
        }

        public IEnumerable<Bookmark> ienumableGetAllBookmark()
        {
            yield return bookmark;
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
            mockBookmarkRepository.Setup(x => x.Delete(It.IsAny<Bookmark>())).Returns(bookmark);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            Bookmark bookmarkReturn = bookmarkService.DeleteBookmark(bookmark);
            Assert.AreEqual(bookmarkReturn, bookmarkReturn);
        }

        [TestCase]
        public void TestGetUserBookmarkId()
        {
            mockBookmarkRepository.Setup(x => x.GetUserBookmarkId(It.IsAny<string>())).Returns(ienumableGetUserBookmarkId);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            IEnumerable<string> ienumableReturn = bookmarkService.GetUserBookmarkId("5d0a17701a0a4200017de6c7");
            Assert.IsNotEmpty(ienumableReturn);
        }

        [TestCase]
        public void TestGetUserBookmarks()
        {
            mockBookmarkRepository.Setup(x => x.GetAll(It.IsAny<string>())).Returns(ienumableGetAllBookmark);
            var bookmarkService = new BookmarkService(mockBookmarkRepository.Object);
            IEnumerable<Bookmark> ienumableReturn = bookmarkService.GetUserBookmarks("5d0a17701a0a4200017de6c7");
            Assert.IsNotEmpty(ienumableReturn);
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly BookmarkRepository _bookmarkRepository = null;

        public BookmarkService(BookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public BookmarkService(IOptions<AppSettings> settings)
        {
            _bookmarkRepository = new BookmarkRepository(settings);
        }

        public Bookmark AddBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Add(bookmark);
        }

        public Bookmark DeleteBookmark(Bookmark bookmark)
        {
            return _bookmarkRepository.Delete(bookmark);
        }

        public Bookmark GetUserBookmark(string userId)
        {
            return _bookmarkRepository.GetById(userId);
        }
    }
}

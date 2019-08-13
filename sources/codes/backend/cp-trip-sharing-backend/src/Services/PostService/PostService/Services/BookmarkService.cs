using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository = null;

        public BookmarkService(IBookmarkRepository bookmarkRepository)
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

        public bool DeleteBookmark(string id)
        {
            return _bookmarkRepository.Delete(id);
        }

        public Bookmark GetById(string id)
        {
            return _bookmarkRepository.GetById(id);
        }

        public IEnumerable<string> GetUserBookmarkId(string id)
        {
            return _bookmarkRepository.GetUserBookmarkId(id);
        }

        public IEnumerable<Bookmark> GetUserBookmarks(string userId, int page)
        {
            return _bookmarkRepository.GetAll(userId,page);
        }
    }
}

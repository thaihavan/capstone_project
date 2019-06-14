using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories
{
    public class BookmarkRepository : IRepository<Bookmark>
    {
        private readonly IMongoCollection<Bookmark> _bookmarks = null;

        public BookmarkRepository()
        {
            var dbContext = new MongoDbContext();
            _bookmarks = dbContext.BookmarkCollection;
        }

        public Bookmark Add(Bookmark bookmark)
        {
            _bookmarks.InsertOne(bookmark);
            return bookmark;
        }

        public Bookmark GetById(string user_id)
        {
            return _bookmarks.Find(bookmark => bookmark.UserId.Equals(user_id)).FirstOrDefault();
        }

        public IEnumerable<Bookmark> GetAll()
        {
            throw new NotImplementedException();
        }

        public Bookmark Update(Bookmark document)
        {
            throw new NotImplementedException();
        }

        public Bookmark Delete(Bookmark document)
        {
            _bookmarks.DeleteOne(item => item.UserId.Equals(document.UserId) && item.PostId.Equals(document.PostId));
            return document;
        }

        public IEnumerable<Bookmark> GetAll(string id)
        {
            List<Bookmark> bookmarks = _bookmarks.Find(temp => temp.UserId.Equals(id)).ToList();
            return bookmarks;
        }
    }
}

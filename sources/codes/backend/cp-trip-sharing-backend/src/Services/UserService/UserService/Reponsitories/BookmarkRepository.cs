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

        public BookmarkRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDBContext(settings);
            _bookmarks = dbContext.BookmarksCollection;
        }

        public bool Add(Bookmark bookmarks)
        {
            _bookmarks.InsertOne(bookmarks);
            return true;
        }

        public Bookmark GetById(string user_id)
        {
            return _bookmarks.Find(bookmark => bookmark.UserId.Equals(user_id)).FirstOrDefault();
        }

        public IEnumerable<Bookmark> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Bookmark document)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Bookmark document)
        {
            return _bookmarks.DeleteOne(item => item.UserId.Equals(document.UserId) && item.PostId.Equals(document.PostId)).IsAcknowledged;
        }

        public IEnumerable<Bookmark> GetAll(string id)
        {
            List<Bookmark> bookmarks = _bookmarks.Find(temp => temp.UserId.Equals(id)).ToList();
            return bookmarks;
        }
    }
}

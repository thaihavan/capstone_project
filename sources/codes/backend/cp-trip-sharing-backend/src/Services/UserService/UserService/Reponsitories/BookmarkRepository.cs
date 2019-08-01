using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly IMongoCollection<Bookmark> _bookmarks = null;
        private readonly IMongoCollection<User> _users = null;
        
        public BookmarkRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _bookmarks = dbContext.BookmarkCollection;
            _users = dbContext.Users;
        }
        
        public Bookmark Add(Bookmark bookmark)
        {
            _bookmarks.InsertOne(bookmark);
            return bookmark;
        }

        public Bookmark GetById(string user_id)
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
            Func<Bookmark, User, Bookmark> selectAuthor =
                ((bookmark, user) => { bookmark.Author = user; return bookmark; });
            //List<Bookmark> bookmarks = _bookmarks.Find(temp => temp.UserId.Equals(id)).ToList();
            var bookmarks = _bookmarks.AsQueryable().Where(x => x.UserId.Equals(id))
                .Join(_users.AsQueryable(),
                bookmark => bookmark.AuthorId,
                user => user.Id,
                selectAuthor
                ).ToList();
            return bookmarks;
        }

        public Bookmark Update(Bookmark document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bookmark> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetUserBookmarkId(string id)
    
        {
            List<string> bookmarks = _bookmarks
                .AsQueryable()
                .Where(x => x.UserId
                .Equals(id))
                .Select(x => x.PostId).ToList();
            return bookmarks;
        }
    }
}

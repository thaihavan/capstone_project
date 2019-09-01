using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories
{
    public class BookmarkRepository: IBookmarkRepository
    {
        private readonly IMongoCollection<Bookmark> _bookmarks = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Post> _posts = null;

        public BookmarkRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _bookmarks = dbContext.BookmarkCollection;
            _authors = dbContext.Authors;
            _posts = dbContext.Posts;
        }

        public Bookmark Add(Bookmark bookmark)
        {
            _bookmarks.InsertOne(bookmark);
            return bookmark;
        }

        public Bookmark GetById(string id)
        {
            return _bookmarks.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Bookmark> GetAll(string id)
        {
            Func<Bookmark, Post, Bookmark> selectPost =
                ((bookmark, post) => { bookmark.Post = post; return bookmark; });
            Func<Bookmark, Author, Bookmark> selectAuthor =
                ((bookmark, author) => { bookmark.Post.Author = author; return bookmark; });

            var bookmarks = _bookmarks.AsQueryable()
                .Where(x => x.UserId.Equals(id))
                .Join(_posts.AsQueryable(),
                    bookmark => bookmark.PostId,
                    post => post.Id,
                    selectPost
                )
                .Join(_authors.AsQueryable(),
                    bookmark => bookmark.Post.AuthorId,
                    author => author.Id,
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
                .Where(x => x.UserId.Equals(id))
                .Select(x => x.PostId).ToList();
            return bookmarks;
        }

        public bool Delete(string id)
        {
            _bookmarks.FindOneAndDelete(x => x.Id.Equals(id));
            return true;
        }

        public bool DeleteBookmark(string postId, string userId)
        {
            _bookmarks.FindOneAndDelete(
                Builders<Bookmark>.Filter.Eq(x => x.PostId, postId) &
                Builders<Bookmark>.Filter.Eq(x => x.UserId, userId)
                );
            return true;
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
    public class ArticleRepository : IArticleRepository
    {
        private readonly IMongoCollection<Article> _articles = null;
        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Like> _likes = null;

        public ArticleRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _articles = dbContext.Articles;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
            _likes = dbContext.Likes;
        }

        public Article Add(Article param)
        {
            _articles.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            _articles.DeleteOne(a => a.Id == id);

            return true;
        }

        public IEnumerable<Article> GetAll()
        {
            return _articles.Find(a => true).ToList();
        }

        public Article GetById(string id)
        {
            return _articles.Find(a => a.Id == id).FirstOrDefault();
        }

        public Article Update(Article param)
        {
            var result = _articles.ReplaceOne(a => a.Id == param.Id , param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<Article> GetAllArticleInfo()
        {
            var articles = _posts.AsQueryable().Join(
                _authors.AsQueryable(),
                post => post.AuthorId,
                author => author.Id,
                (post, author) => new
                {
                    Id = post.Id,
                    Post = new Post
                    {
                        Id = post.Id,
                        AuthorId = post.AuthorId,
                        Content = post.Content,
                        CommentCount = post.CommentCount,
                        IsActive = post.IsActive,
                        IsPublic = post.IsPublic,
                        LikeCount = post.LikeCount,
                        PostType = post.PostType,
                        PubDate = post.PubDate,
                        Title = post.Title,
                        Author = new Author()
                        {
                            Id = author.Id,
                            DisplayName = author.DisplayName,
                            ProfileImage = author.ProfileImage
                        }
                    }
                }).Join(
                    _articles.AsQueryable(),
                    pa => pa.Id,
                    article => article.PostId,
                    (pa, article) => new Article()
                    {
                        Id = article.Id,
                        Topics = article.Topics,
                        Destinations = article.Destinations,
                        PostId = article.PostId,
                        CoverImage = article.CoverImage,
                        Post = pa.Post
                    }
                );
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticleByUser(string userId)
        {
            var c = GetAllArticleInfo();
            var result = c.Where(x => x.Post.AuthorId == userId).ToList();
            return result;
        }

        public Article GetArticleInfoById(string id)
        {
            var articles = _posts.AsQueryable().Join(
                _authors.AsQueryable(),
                post => post.AuthorId,
                author => author.Id,
                (post, author) => new
                {
                    Id = post.Id,
                    Post = new Post
                    {
                        Id = post.Id,
                        AuthorId = post.AuthorId,
                        Content = post.Content,
                        CommentCount = post.CommentCount,
                        IsActive = post.IsActive,
                        IsPublic = post.IsPublic,
                        LikeCount = post.LikeCount,
                        PostType = post.PostType,
                        PubDate = post.PubDate,
                        Title = post.Title,
                        Author = new Author()
                        {
                            Id = author.Id,
                            DisplayName = author.DisplayName,
                            ProfileImage = author.ProfileImage
                        }
                    }
                }).Join(
                    _articles.AsQueryable().Where(a => a.Id == id),
                    pa => pa.Id,
                    article => article.PostId,
                    (pa, article) => new Article()
                    {
                        Id = article.Id,
                        Topics = article.Topics,
                        Destinations = article.Destinations,
                        PostId = article.PostId,
                        CoverImage = article.CoverImage,
                        Post = pa.Post
                    }
                );
            return articles.FirstOrDefault();
        }

        public IEnumerable<Article> GetAllArticleWithPost()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetAllArticleInfo(string userId)
        {
            Func<Article, IEnumerable<Like>,Article> UpdateLike = ((a, b) => { a.liked = b.Count() > 0 ? true : false; return a; });
            var articles = _posts.AsQueryable().Join(
                _authors.AsQueryable(),
                post => post.AuthorId,
                author => author.Id,
                (post, author) => new
                {
                    Id = post.Id,
                    Post = new Post
                    {
                        Id = post.Id,
                        AuthorId = post.AuthorId,
                        Content = post.Content,
                        CommentCount = post.CommentCount,
                        IsActive = post.IsActive,
                        IsPublic = post.IsPublic,
                        LikeCount = post.LikeCount,
                        PostType = post.PostType,
                        PubDate = post.PubDate,
                        Title = post.Title,
                        Author = new Author()
                        {
                            Id = author.Id,
                            DisplayName = author.DisplayName,
                            ProfileImage = author.ProfileImage
                        }
                    }
                }).Join(
                    _articles.AsQueryable(),
                    pa => pa.Id,
                    article => article.PostId,
                    (pa, article) => new Article()
                    {
                        Id = article.Id,
                        Topics = article.Topics,
                        Destinations = article.Destinations,
                        PostId = article.PostId,
                        CoverImage = article.CoverImage,
                        Post = pa.Post
                    }).GroupJoin(
                    _likes.AsQueryable().Where(x=>x.UserId==userId&&x.ObjectType=="post"),
                    article=> article.PostId,
                    like=>like.ObjectId,
                    UpdateLike                  
                );
            return articles.ToList();
        }
    }
}

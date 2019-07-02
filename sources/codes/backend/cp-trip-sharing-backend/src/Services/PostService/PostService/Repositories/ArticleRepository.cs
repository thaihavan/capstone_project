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
using System.Linq.Expressions;
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
                        CoverImage = post.CoverImage,
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
                        Post = pa.Post
                    }
                ).OrderByDescending(a => a.Post.PubDate);
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticleByUser(string userId)
        {
            var c = GetAllArticleInfo();
            var result = c.Where(x => x.Post.AuthorId == userId).ToList();
            return result;
        }

        public Article GetArticleInfoById(string id,string userId)
        {
            Func<Post,Author,Post> SelectPostWithAuthor =
                ((post, author) => { post.Author = author;return post; });

            Func<Post, Article, Article> SelectArticleWithPost =
                ((post, article) => { article.Post = post;return article; });

            Func<Article, IEnumerable<Like>, Article> UpdateLike =
                ((article, likes) => { article.Post.liked = likes.Count() > 0 ? true : false; return article; });

            var articles = _posts.AsQueryable()
                .Join(
                    _authors.AsQueryable(),
                    post => post.AuthorId,
                    author => author.Id,
                    SelectPostWithAuthor)
                .Join(
                    _articles.AsQueryable(),
                    post => post.Id,
                    article => article.PostId,
                    SelectArticleWithPost)
                .GroupJoin(_likes.AsQueryable().Where(x => x.UserId == userId && x.ObjectType == "post"),
                    article => article.PostId,
                    like => like.ObjectId,
                    UpdateLike)
                    .Where(article => article.Id == id).Select(a => a);
            return articles.FirstOrDefault();
        }

        public IEnumerable<Article> GetAllArticleInfo(string userId)
        {
            Func<Article, IEnumerable<Like>, Article> UpdateLike =
                ((article, likes) => { article.Post.liked = likes.Count() > 0 ? true : false; return article; });

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
                        CoverImage = post.CoverImage,
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
                        Post = pa.Post
                    }).GroupJoin(
                    _likes.AsQueryable().Where(x => x.UserId == userId && x.ObjectType == "post"),
                    article => article.PostId,
                    like => like.ObjectId,
                    UpdateLike
                ).OrderByDescending(a => a.Post.PubDate);
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticleInfo(string userId, PostFilter postFilter)
        {
            // Time period filter
            var filterDate = new DateTime(0);
            var now = DateTime.Now;
            switch (postFilter.TimePeriod)
            {
                case "today":
                    filterDate = now.AddDays(-1);
                    break;
                case "this_week":
                    filterDate = now.AddDays(-7);
                    break;
                case "this_month":
                    filterDate = now.AddDays(-30);
                    break;
                case "this_year":
                    filterDate = now.AddDays(-365);
                    break;
                case "all_time":
                    filterDate = new DateTime(0);
                    break;
            }

            // Topic filter
            Expression<Func<Article, bool>> topicFilter;
            if (postFilter.Topics.Count > 0)
            {
                topicFilter = a => a.Topics.Any(x => postFilter.Topics.Any(y => x == y));
            }
            else
            {
                topicFilter = a => true;
            }

            Func<Article, IEnumerable<Like>, Article> UpdateLike =
                ((article, likes) => { article.Post.liked = likes.Count() > 0 ? true : false; return article; });

            var articles = _posts.AsQueryable()
                .Where(p => p.PubDate >= filterDate)
                .Join(
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
                            CoverImage = post.CoverImage,
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
                        Post = pa.Post
                    }).GroupJoin(
                        _likes.AsQueryable().Where(x => x.UserId == userId && x.ObjectType == "post"),
                        article => article.PostId,
                        like => like.ObjectId,
                        UpdateLike
                ).Where(topicFilter.Compile()).Select(a => a);
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticleInfo(PostFilter postFilter)
        {
            // Time period filter
            var filterDate = new DateTime(0);
            var now = DateTime.Now;
            switch (postFilter.TimePeriod)
            {
                case "today":
                    filterDate = now.AddDays(-1);
                    break;
                case "this_week":
                    filterDate = now.AddDays(-7);
                    break;
                case "this_month":
                    filterDate = now.AddDays(-30);
                    break;
                case "this_year":
                    filterDate = now.AddDays(-365);
                    break;
                case "all_time":
                    filterDate = new DateTime(0);
                    break;
            }

            // Topic filter
            Expression<Func<Article, bool>> topicFilter;
            if (postFilter.Topics.Count > 0)
            {
                topicFilter = a => a.Topics.Any(x => postFilter.Topics.Any(y => x == y));
            }
            else
            {
                topicFilter = a => true;
            }

            var articles = _posts.AsQueryable()
                .Where(p => p.PubDate >= filterDate)
                .Join(
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
                            CoverImage = post.CoverImage,
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
                        Post = pa.Post
                    }
                ).Where(topicFilter.Compile()).Select(a => a);
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticleByUser(string userId, PostFilter postFilter)
        {
            var c = GetAllArticleInfo(postFilter);
            var result = c.Where(x => x.Post.AuthorId == userId).ToList();
            return result;
        }
    }
}

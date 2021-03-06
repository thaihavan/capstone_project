﻿using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var article = _articles.Find(a => a.Id == id).FirstOrDefault();
            _posts.FindOneAndUpdate(
                Builders<Post>.Filter.Eq(x => x.Id, article.PostId),
                Builders<Post>.Update.Set(x => x.IsActive, false)
                );
            return true;
        }

        public IEnumerable<Article> GetAll()
        {
            return _articles.Find(a => a.Post.IsActive == true).ToList();
        }

        public Article GetById(string id)
        {
            return _articles.Find(a => a.Id == id).FirstOrDefault();
        }

        public Article Update(Article param)
        {
            var result = _articles.ReplaceOne(a => a.Id == param.Id, param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public Article GetArticleById(string id, string userId)
        {
            Func<Post, Author, Post> SelectPostWithAuthor =
                ((post, author) => { post.Author = author; return post; });

            Func<Post, Article, Article> SelectArticleWithPost =
                ((post, article) => { article.Post = post; return article; });

            Func<Article, IEnumerable<Like>, Article> UpdateLike =
                ((article, likes) => { article.Post.liked = likes.Count() > 0 ? true : false; return article; });

            IEnumerable<Article> articles;

            if (userId == string.Empty)
            {
                articles = _posts.AsQueryable()
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
                .Where(a => a.Id == id).Select(a => a);
            }
            else
            {
                articles = _posts.AsQueryable()
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
            }

            return articles.FirstOrDefault();
        }

        public IEnumerable<Article> GetAllArticles(PostFilter postFilter, int page)
        {
            // Search filter

            // Text search
            if (postFilter.Search == null)
            {
                postFilter.Search = "";
            }
            postFilter.Search = postFilter.Search.Trim();

            // LocationId search
            if (postFilter.LocationId == null)
            {
                postFilter.LocationId = "";
            }

            Expression<Func<Article, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => postFilter.LocationId == "" || d.Id == postFilter.LocationId);

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
            Expression<Func<Article, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;
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

            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<Article, Author, Article> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });

            var articles = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.PubDate >= filterDate && p.IsActive),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                )
                .Where(searchFilter.Compile())
                .Where(topicFilter.Compile())
                .Where(dateFilter.Compile())
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * (page - 1))
                .Take(12);
            return articles.ToList();
        }

        public IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter, int page)
        {
            // Search filter

            // Text search
            if (postFilter.Search == null)
            {
                postFilter.Search = "";
            }
            postFilter.Search = postFilter.Search.Trim();

            // LocationId search
            if (postFilter.LocationId == null)
            {
                postFilter.LocationId = "";
            }

            Expression<Func<Article, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => postFilter.LocationId == "" || d.Id == postFilter.LocationId);

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

            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<Article, Author, Article> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });

            var articles = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.PubDate >= filterDate && p.AuthorId == userId && p.IsActive),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                )
                .Where(searchFilter.Compile())
                .Where(topicFilter.Compile())
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * (page - 1))
                .Take(12);
            return articles.ToList();
        }

        public IEnumerable<Article> GetRecommendArticles(PostFilter postFilter, UserInfo userInfo, int page)
        {
            //UserInfo filter
            Expression<Func<Article, bool>> userInfoFilter =
                article => (userInfo.Follows.Any(x => x==article.Post.AuthorId))|| (article.Topics.Any(x => userInfo.Topics.Any(y => x == y)));
            

            // Text search
            if (postFilter.Search == null)
            {
                postFilter.Search = "";
            }
            postFilter.Search = postFilter.Search.Trim();

            // LocationId search
            if (postFilter.LocationId == null)
            {
                postFilter.LocationId = "";
            }

            Expression<Func<Article, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => postFilter.LocationId == "" || d.Id == postFilter.LocationId);

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
            Expression<Func<Article, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;
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

            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<Article, Author, Article> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });

            var articles = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.PubDate >= filterDate && p.IsActive),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                )
                .Where(searchFilter.Compile())
                .Where(topicFilter.Compile())
                .Where(dateFilter.Compile())
                .Where(userInfoFilter.Compile())
                .OrderByDescending(a => a.Post.PubDate)
                .Select(a => a)
                .Skip(12 * (page-1))
                .Take(12);
            return articles.ToList();
        }

        public IEnumerable<Article> GetPopularArticles(PostFilter postFilter, int page)
        {
            // Search filter

            // Text search
            if (postFilter.Search == null)
            {
                postFilter.Search = "";
            }
            postFilter.Search = postFilter.Search.Trim();

            // LocationId search
            if (postFilter.LocationId == null)
            {
                postFilter.LocationId = "";
            }

            Expression<Func<Article, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => postFilter.LocationId == "" || d.Id == postFilter.LocationId);

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

            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<Article, Author, Article> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });

            var articles = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.PubDate >= filterDate && p.IsActive),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                )
                .Where(searchFilter.Compile())
                .Where(topicFilter.Compile())
                .OrderByDescending(a => a.Post.LikeCount)
                .ThenByDescending(a => a.Post.PubDate)
                .Select(a => a)
                .Skip(12 * (page - 1))
                .Take(12);
            return articles.ToList();
        }

        public object GetArticleStatistics(StatisticsFilter filter)
        {
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "dd-MM-yyyy";
            format.DateSeparator = "-";            

            //time filter 
            Expression<Func<Article, bool>> dateFilter =
                article => article.Post.PubDate > filter.From && article.Post.PubDate <= filter.To;

            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });

            var articles = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.IsActive),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                )
                .Where(dateFilter.Compile())
                .OrderBy(x => x.Post.PubDate)
                .Select(x => x)
                .ToList();

            var data = articles.GroupBy(x => x.Post.PubDate.ToString("dd-MM-yyy"))
                    .Select(x => new
                    {
                        name = x.Key,
                        value = x.Count()
                    }).ToList();

            var dummyData = Enumerable.Range(0, (filter.To - filter.From).Days)
                .Select(i => new
                {
                    name = filter.From.AddDays(i).ToString("dd-MM-yyy"),
                    value = 0
                }).ToList();

            var exceptData = data.Select(x => new
            {
                name = x.name,
                value = 0
            }).ToList();

            var result = data.Union(
                    dummyData.Except(exceptData)
                )
                .OrderBy(x => Convert.ToDateTime(x.name, format))
                .Select(x => x);
            return new
            {
                name = "Bài viết",
                series = result
            };
        }

        
    }
}

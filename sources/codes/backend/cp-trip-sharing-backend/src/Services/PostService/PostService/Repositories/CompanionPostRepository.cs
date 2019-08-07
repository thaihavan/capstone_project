using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;

namespace PostService.Repositories
{
    public class CompanionPostRepository : ICompanionPostRepository
    {
        private readonly IMongoCollection<CompanionPost> _companionPosts = null;
        private readonly IMongoCollection<CompanionPostJoinRequest> _companionPostJoinRequests = null;
        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Like> _likes = null;

        public CompanionPostRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _companionPosts = dbContext.CompanionPosts;
            _companionPostJoinRequests = dbContext.CompanionPostJoinRequests;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
            _likes = dbContext.Likes;
        }

        public CompanionPost Add(CompanionPost param)
        {
            _companionPosts.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            var postId = _companionPosts.Find(x => x.Id.Equals(id)).FirstOrDefault().PostId;
            _posts.FindOneAndUpdate(
                Builders<Post>.Filter.Eq(x => x.Id, postId),
                Builders<Post>.Update.Set(x => x.IsActive, false)
                );
            return true;
        }

        public IEnumerable<CompanionPost> GetAll()
        {
            throw new NotImplementedException();
        }

        public CompanionPost GetById(string id)
        {
            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });

            var result = _companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .FirstOrDefault();
            return result;
        }

        public CompanionPost Update(CompanionPost param)
        {
            _companionPosts.FindOneAndReplace(
                Builders<CompanionPost>.Filter.Eq(x => x.Id, param.Id),
                param
                );
            return param;
        }

        public CompanionPost GetById(string id, string userId)
        {
            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });
            Func<CompanionPost, IEnumerable<Like>, CompanionPost> UpdateLike =
                ((CompanionPost, likes) => { CompanionPost.Post.liked = likes.Count() > 0 ? true : false; return CompanionPost; });
            Func<CompanionPost, IEnumerable<CompanionPostJoinRequest>, CompanionPost> UpdateRequested =
                ((CompanionPost, requests) => { CompanionPost.Requested = requests.Count() > 0 ? true : false; return CompanionPost; });

            CompanionPost result;

            if (string.IsNullOrEmpty(userId))
            {
                result = _companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .FirstOrDefault();
            }
            else
            {
                result = _companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .GroupJoin(
                        _companionPostJoinRequests.AsQueryable().Where(x => x.UserId.Equals(userId)),
                        companionPost => companionPost.Id,
                        request => request.CompanionPostId,
                        UpdateRequested)
                    .GroupJoin(
                        _likes.AsQueryable().Where(x => x.UserId.Equals(userId) && x.ObjectType.Equals("post")),
                        companionPost => companionPost.PostId,
                        like => like.ObjectId,
                        UpdateLike)
                    .FirstOrDefault();
            }

            return result;
        }

        public IEnumerable<CompanionPost> GetAll(PostFilter filter, int page)
        {
            // Search filter

            // Text search
            if (filter.Search == null)
            {
                filter.Search = "";
            }
            filter.Search = filter.Search.Trim();

            // LocationId search
            if (filter.LocationId == null)
            {
                filter.LocationId = "";
            }

            Expression<Func<CompanionPost, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(filter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => filter.LocationId == "" || d.Id == filter.LocationId);

            // Time period filter
            var filterDate = new DateTime(0);
            var now = DateTime.Now;
            switch (filter.TimePeriod)
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
            Expression<Func<CompanionPost, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;


            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });

            IEnumerable<CompanionPost> result;

            result = _companionPosts.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.IsActive),
                    companionPost => companionPost.PostId,
                    post => post.Id,
                    SelectCompanionPostWithPost)
                .Join(
                    _authors.AsQueryable(),
                    companionPostWithPost => companionPostWithPost.Post.AuthorId,
                    author => author.Id,
                    SelectCompanionPostWithAuthor)
                .Where(searchFilter.Compile())
                .Where(dateFilter.Compile())
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * (page - 1))
                .Take(12)
                .ToList();

            return result;
        }

        public IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId)
        {
            Func<CompanionPostJoinRequest, Author, CompanionPostJoinRequest> SelectCompanionPostRequestWithUser =
                ((post, user) => { post.User = user; return post; });

            var result = _companionPostJoinRequests.AsQueryable().Where(x => x.CompanionPostId.Equals(companionPostId))
                .Join(_authors.AsQueryable(),
                post => post.UserId,
                user => user.Id,
                SelectCompanionPostRequestWithUser
                ).OrderByDescending(x => x.Date).ToList();
            return result;

        }

        public CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param)
        {
            _companionPostJoinRequests.InsertOne(param);
            return param;
        }

        public bool DeleteJoinRequest(string requestId)
        {
            _companionPostJoinRequests.FindOneAndDelete(
                Builders<CompanionPostJoinRequest>.Filter.Eq(x => x.Id, requestId));
            return true;
        }

        public IEnumerable<CompanionPost> GetAllCompanionPostByUser(string userId, PostFilter filter, int page)
        {
            // Search filter

            // Text search
            if (filter.Search == null)
            {
                filter.Search = "";
            }
            filter.Search = filter.Search.Trim();

            // LocationId search
            if (filter.LocationId == null)
            {
                filter.LocationId = "";
            }

            Expression<Func<CompanionPost, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(filter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Destinations.Any(d => filter.LocationId == "" || d.Id == filter.LocationId);

            // Time period filter
            var filterDate = new DateTime(0);
            var now = DateTime.Now;
            switch (filter.TimePeriod)
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
            Expression<Func<CompanionPost, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;


            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });

            IEnumerable<CompanionPost> result;

            result = _companionPosts.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.IsActive),
                    companionPost => companionPost.PostId,
                    post => post.Id,
                    SelectCompanionPostWithPost)
                .Join(
                    _authors.AsQueryable(),
                    companionPostWithPost => companionPostWithPost.Post.AuthorId,
                    author => author.Id,
                    SelectCompanionPostWithAuthor)
                .Where(searchFilter.Compile())
                .Where(a => a.Post.AuthorId.Equals(userId, StringComparison.Ordinal))
                .Where(dateFilter.Compile())
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * (page - 1))
                .Take(12)
                .ToList();
            return result;
        }

        public CompanionPostJoinRequest GetRequestById(string requestId)
        {
            return _companionPostJoinRequests.Find(x => x.Id.Equals(requestId)).FirstOrDefault();
        }

        public CompanionPostJoinRequest GetRequestByUserIdAndPostId(string userId, string postId)
        {
            return _companionPostJoinRequests.Find(x => x.UserId.Equals(userId) && x.CompanionPostId.Equals(postId)).FirstOrDefault();
        }

        public object GetCompanionPostStatistics(StatisticsFilter filter)
        {
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "dd-MM-yyyy";
            format.DateSeparator = "-";           

            //time filter 
            Expression<Func<CompanionPost, bool>> dateFilter =
                post => post.Post.PubDate > filter.From && post.Post.PubDate <= filter.To;

            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });

            var companionPosts = _companionPosts.AsQueryable()
                .Join(
                    _posts.AsQueryable().Where(p => p.IsActive),
                    companionPost => companionPost.PostId,
                    post => post.Id,
                    SelectCompanionPostWithPost)
                .Where(dateFilter.Compile())
                .OrderBy(x => x.Post.PubDate)
                .Select(x => x)
                .ToList();

            var data = companionPosts.GroupBy(x => x.Post.PubDate.ToString("dd-MM-yyy"))
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
                name = "CompanionPost",
                series = result
            };
        }
    }
}

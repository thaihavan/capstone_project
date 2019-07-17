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
    public class VirtualTripRepository : IVirtualTripRepository
    {
        private readonly IMongoCollection<VirtualTrip> _virtualTrips = null;
        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Like> _likes = null;

        public VirtualTripRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _virtualTrips = dbContext.VirtualTrips;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
            _likes = dbContext.Likes;
        }

        public VirtualTrip Add(VirtualTrip param)
        {
            _virtualTrips.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            return _virtualTrips.DeleteOne(x => x.Id == id).IsAcknowledged;
        }

        public IEnumerable<VirtualTrip> GetAll()
        {
            return _virtualTrips.Find(v => true).ToList();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTrips.Find(v => v.Id == id).FirstOrDefault();
        }

        public VirtualTrip Update(VirtualTrip param)
        {
            var relult = _virtualTrips.ReplaceOne(v => v.Id == param.Id, param);
            if (!relult.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public VirtualTrip GetVirtualTrip(string id)
        {
            var virtualTrips = _posts.AsQueryable().Join(
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
                    _virtualTrips.AsQueryable(),
                    pv => pv.Id,
                    v => v.PostId,
                    (pv, v) => new VirtualTrip()
                    {
                        Id = v.Id,
                        Items = v.Items,
                        PostId = v.PostId,
                        Post = pv.Post
                    }
                ).Where(v => v.Id == id).Select(v => v);
            return virtualTrips.FirstOrDefault();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTrips(string userId, PostFilter postFilter, int page)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTrips(PostFilter postFilter, int page)
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

            Expression<Func<VirtualTrip, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Items.Any(d => postFilter.LocationId == "" || d.LocationId == postFilter.LocationId);

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
            Expression<Func<VirtualTrip, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;

            Func<VirtualTrip, Post, VirtualTrip> SelectVirtualTripWithPost =
                ((virtualTrip, post) => { virtualTrip.Post = post; return virtualTrip; });
            Func<VirtualTrip, Author, VirtualTrip> SelectVirtualTripWithAuthor =
                ((virtualTrip, author) => { virtualTrip.Post.Author = author; return virtualTrip; });

            var result = _virtualTrips.AsQueryable()
                .Join(
                    _posts.AsQueryable(),
                    virtualTrip => virtualTrip.PostId,
                    post => post.Id,
                    SelectVirtualTripWithPost)
                .Join(
                    _authors.AsQueryable(),
                    virtualTrip => virtualTrip.Post.AuthorId,
                    author => author.Id,
                    SelectVirtualTripWithAuthor)
                .Where(searchFilter.Compile())               
                .Where(dateFilter.Compile())
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * page)
                .Take(12);
            return result.ToList();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripsByUser(string userId, PostFilter postFilter, int page)
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

            Expression<Func<VirtualTrip, bool>> searchFilter;
            searchFilter = a => a.Post.Title.IndexOf(postFilter.Search, StringComparison.OrdinalIgnoreCase) >= 0
                                && a.Items.Any(d => postFilter.LocationId == "" || d.LocationId == postFilter.LocationId);

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
            Expression<Func<VirtualTrip, bool>> dateFilter =
                post => post.Post.PubDate >= filterDate;

            Func<VirtualTrip, Post, VirtualTrip> SelectVirtualTripWithPost =
                ((virtualTrip, post) => { virtualTrip.Post = post; return virtualTrip; });
            Func<VirtualTrip, Author, VirtualTrip> SelectVirtualTripWithAuthor =
                ((virtualTrip, author) => { virtualTrip.Post.Author = author; return virtualTrip; });

            var result = _virtualTrips.AsQueryable()
                .Join(
                    _posts.AsQueryable(),
                    virtualTrip => virtualTrip.PostId,
                    post => post.Id,
                    SelectVirtualTripWithPost)
                .Join(
                    _authors.AsQueryable(),
                    virtualTrip => virtualTrip.Post.AuthorId,
                    author => author.Id,
                    SelectVirtualTripWithAuthor)
                .Where(searchFilter.Compile())
                .Where(a => a.Post.AuthorId.Equals(userId, StringComparison.Ordinal))
                .Where(dateFilter.Compile())                
                .Select(a => a)
                .OrderByDescending(a => a.Post.PubDate)
                .Skip(12 * page)
                .Take(12);
            return result.ToList();
        }
    }
}

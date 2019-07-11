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

        public IEnumerable<VirtualTrip> GetAllVirtualTrips(string userId, PostFilter postFilter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTrips(PostFilter postFilter)
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

            var virtualTrips = _posts.AsQueryable()
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
                    _virtualTrips.AsQueryable(),
                    pv => pv.Id,
                    v => v.PostId,
                    (pv, v) => new VirtualTrip()
                    {
                        Id = v.Id,
                        Items = v.Items,
                        PostId = v.PostId,
                        Post = pv.Post
                    });
            return virtualTrips.ToList();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripsByUser(string userId, PostFilter postFilter)
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

            var virtualTrips = _posts.AsQueryable()
                .Where(p => p.PubDate >= filterDate && p.AuthorId == userId)
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
                    _virtualTrips.AsQueryable(),
                    pv => pv.Id,
                    v => v.PostId,
                    (pv, v) => new VirtualTrip()
                    {
                        Id = v.Id,
                        Items = v.Items,
                        PostId = v.PostId,
                        Post = pv.Post
                    });
            return virtualTrips.ToList();
        }
    }
}

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
            _likes= dbContext.Likes;
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

        public IEnumerable<VirtualTrip> GetAllVirtualTripWithPost()
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
                    });
            return virtualTrips.ToList();
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripWithPost(string userId)
        {
            Func<VirtualTrip, IEnumerable<Like>, VirtualTrip> UpdateLiked =
                ((virtualTrip, likes) => { virtualTrip.Post.liked = likes.Count() > 0 ? true : false; return virtualTrip; });
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
                    }).GroupJoin(
                    _likes.AsQueryable().Where(x=>x.ObjectType=="post"&&x.UserId==userId),
                    virtualTrip=>virtualTrip.PostId,
                    like=>like.ObjectId,
                    UpdateLiked
                );
            return virtualTrips.ToList();
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
    }
}

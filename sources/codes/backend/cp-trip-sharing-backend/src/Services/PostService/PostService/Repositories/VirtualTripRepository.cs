﻿using Microsoft.Extensions.Options;
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

        public VirtualTripRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _virtualTrips = dbContext.VirtualTrips;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
        }

        public VirtualTrip Add(VirtualTrip param)
        {
            _virtualTrips.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            return _virtualTrips.DeleteOne(x => x.Id.Equals(new BsonObjectId(id))).IsAcknowledged;
        }

        public IEnumerable<VirtualTrip> GetAll()
        {
            return _virtualTrips.Find(v => true).ToList();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTrips.Find(v => v.Id.Equals(new BsonObjectId(id))).FirstOrDefault();
        }

        public VirtualTrip Update(VirtualTrip param)
        {
            var filter = Builders<VirtualTrip>.Filter.Eq(v => v.Id, param.Id);
            var relult = _virtualTrips.ReplaceOne(filter, param);
            if (!relult.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripWithPost()
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
                );
            return articles.ToList();
        }
    }
}

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
    public class LikeRepository : ILikeRepository
    {
        private readonly IMongoCollection<Like> _likes = null;
        private readonly IMongoCollection<Post> _post = null;

        public LikeRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _likes = dbContext.Likes;
            _post = dbContext.Posts;
        }

        public Like Add(Like param)
        {
            _likes.InsertOne(param);
            return param;
        }
        //Unlike
        public bool Delete(string id)
        {
            var filter = Builders<Like>.Filter.Eq(a => a.Id, new BsonObjectId(id));
            return _likes.DeleteOne(filter).IsAcknowledged;
        }

        public IEnumerable<Like> GetLikeWithPost(string postId)
        {
            var likes = from like in _likes.AsQueryable() where like.LikedObject.Equals(new BsonObjectId(ObjectId.Parse(postId)))
                        join p in _post.AsQueryable() on like.LikedObject equals p.Id into joined
                        from post in joined
                        select new Like
                        {
                            Id = like.Id,
                            LikedObject = like.LikedObject,
                            ObjectType = "post",
                            Date = like.Date,
                            UserId = like.UserId,
                            Post = post
                        };
            return likes.ToList();
        }

        public IEnumerable<Like> GetAll()
        {
            throw new NotImplementedException();
        }

        public Like GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Like Update(Like param)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string objectId, string userId)
        {
            var filter = Builders<Like>.Filter.Eq(a => a.LikedObject, new BsonObjectId(objectId)) & Builders<Like>.Filter.Eq(a => a.UserId, new BsonObjectId(userId));
            return _likes.DeleteOne(filter).IsAcknowledged;
        }
    }
}

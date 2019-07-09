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
    public class PostRepository : IPostRepository
    {

        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Comment> _comments = null;

        public PostRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _posts = dbContext.Posts;
            _comments = dbContext.Comments;
        }

        public Post Add(Post param)
        {
            _posts.InsertOne(param);
            return param;
        }

        public bool DecreaseCommentCount(string id)
        {
            var commentCount =(int) _comments.Count(
                Builders<Comment>.Filter.Eq(x => x.PostId, id)
                );
            _posts.FindOneAndUpdate(
                Builders<Post>.Filter.Eq(x => x.Id, id),
                Builders<Post>.Update.Set(x => x.CommentCount, commentCount)
                );
            return true;
        }

        public bool DecreaseLikeCount(string id)
        {
            _posts.FindOneAndUpdate(
                p => p.Id == id,
                Builders<Post>.Update.Inc("like_count",-1)
                );
            return true;
        }

        public bool Delete(string id)
        {
            _posts.DeleteOne(p => p.Id == id);

            return true;
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts.Find(p => true).ToList();
        }

        public Post GetById(string id)
        {
            return _posts.Find(p => p.Id == id).FirstOrDefault();
        }

        public bool IncreaseCommentCount(string id)
        {            
            _posts.FindOneAndUpdate(
                p => p.Id == id,
                Builders<Post>.Update.Inc("comment_count",1)
                );
            return true;
        }

        public bool IncreaseLikeCount(string id)
        {
            _posts.FindOneAndUpdate(
                p => p.Id == id,
                Builders<Post>.Update.Inc("like_count", 1)
                );
            return true;
        }

        public Post Update(Post param)
        {
            var relult = _posts.ReplaceOne(p => p.Id == param.Id, param);
            if (!relult.IsAcknowledged)
            {
                return null;
            }
            return param;
        }
    }
}

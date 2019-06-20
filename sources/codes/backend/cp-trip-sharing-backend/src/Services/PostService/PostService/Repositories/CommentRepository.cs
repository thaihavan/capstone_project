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
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comments = null;

        public CommentRepository(IMongoCollection<Comment> comments)
        {
            _comments = comments;
        }

        public CommentRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _comments = dbContext.Comments;
        }

        public Comment Add(Comment param)
        {
            _comments.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            return _comments.DeleteOne(temp => temp.Id.Equals(new BsonObjectId(id))).IsAcknowledged;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _comments.Find(x => true).ToList();
        }

        public Comment GetById(string id)
        {
            return _comments.Find(Builders<Comment>.Filter.Eq("_id", new BsonObjectId(id))).ToList().FirstOrDefault();
        }

        public Comment Update(Comment param)
        {
            var filter = Builders<Comment>.Filter.Eq(x => x.Id, param.Id);
            var result = _comments.ReplaceOne(filter, param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<Comment> GetCommentByPost(string postId)
        {
            List<Comment> comments = new List<Comment>();
            var allComment = _comments.Find(x => x.PostId.Equals(new BsonObjectId(postId))).ToList();
            var dict = allComment.ToDictionary(x => x.Id, x => x);
            foreach(var x in dict)
            {
                if (x.Value.ParentId == null)
                {
                    comments.Add(x.Value);
                }
                else
                {
                    var parent = dict[x.Value.ParentId];
                    parent.Childs.Add(x.Value);
                }
            }
            return comments;
        }
    }
}

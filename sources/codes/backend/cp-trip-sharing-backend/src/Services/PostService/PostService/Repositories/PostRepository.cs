using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories
{
    public class PostRepository : IRepository<Post>
    {

        private readonly IMongoCollection<Post> _posts = null;

        public PostRepository()
        {
            var dbContext = new MongoDbContext();
            _posts = dbContext.Posts;
        }

        public Post Add(Post param)
        {
            _posts.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.Id, new BsonObjectId(id));

            _posts.DeleteOne(filter);

            return true;
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts.Find(p => true).ToList();
        }

        public Post GetById(string id)
        {
            return _posts.Find(p => p.Id.Equals(new BsonObjectId(id))).FirstOrDefault();
        }

        public Post Update(Post param)
        {
            var filter = Builders<Post>.Filter.Eq(p => p.Id, param.Id);
            var relult = _posts.ReplaceOne(filter, param);
            if (!relult.IsAcknowledged)
            {
                return null;
            }
            return param;
        }
    }
}

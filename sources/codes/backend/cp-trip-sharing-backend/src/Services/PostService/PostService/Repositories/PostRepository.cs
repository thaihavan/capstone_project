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
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts.Find(x=>true).ToList();
        }

        public Post GetById(string id)
        {
            return _posts.Find(p => p.Id.Equals(new BsonObjectId(id))).FirstOrDefault();
        }

        public Post Update(Post param)
        {
            throw new NotImplementedException();
        }
    }
}

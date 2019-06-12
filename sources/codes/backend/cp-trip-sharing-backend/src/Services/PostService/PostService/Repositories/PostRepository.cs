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

        public PostRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _posts = dbContext.Posts;
        }

        public Post Add(Post param)
        {
            _posts.InsertOne(param);
            return param;
        }

        public IEnumerable<Post> GetAll()
        {
            return _posts.Find(x=>true).ToList();
        }

        public Post GetById(string id)
        {
            return _posts.Find(Builders<Post>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }
    }
}

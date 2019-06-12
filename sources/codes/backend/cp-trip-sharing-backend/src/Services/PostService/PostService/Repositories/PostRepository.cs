using Microsoft.Extensions.Options;
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
            return _posts.AsQueryable().Select(x=>x).ToList();
        }

        public Post GetById(string id)
        {
            return _posts.AsQueryable().Where(x=>x.Id.Equals(id)).Select(x=>x).SingleOrDefault();
        }
    }
}

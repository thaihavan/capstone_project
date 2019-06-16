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
    public class ArticleRepository : IArticleRepository
    {
        private readonly IMongoCollection<Article> _articles = null;
        private readonly IMongoCollection<Post> _post = null;

        public ArticleRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _articles = dbContext.Articles;
            _post = dbContext.Posts;
        }

        public Article Add(Article param)
        {
            _articles.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            var filter = Builders<Article>.Filter.Eq(a => a.Id, new BsonObjectId(id));

            _articles.DeleteOne(filter);

            return true;
        }

        public IEnumerable<Article> GetAll()
        {
            return _articles.Find(a => true).ToList();
        }

        public Article GetById(string id)
        {
            return _articles.Find(a => a.Id.Equals(new BsonObjectId(id))).FirstOrDefault();
        }

        public Article Update(Article param)
        {
            var filter = Builders<Article>.Filter.Eq(a => a.Id, param.Id);
            var result = _articles.ReplaceOne(filter, param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<Article> GetAllArticleWithPost()
        {
            var articles = from a in _articles.AsQueryable()
                           join p in _post.AsQueryable() on a.PostId equals p.Id into joined
                           from post in joined
                           select new Article
                           {
                               Id = a.Id,
                               Topics = a.Topics,
                               Destinations = a.Destinations,
                               PostId = a.PostId,
                               Post = post
                           };
            return articles.ToList();
        }
    }
}

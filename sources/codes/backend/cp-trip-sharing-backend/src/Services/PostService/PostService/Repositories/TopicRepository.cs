using Microsoft.Extensions.Options;
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
    public class TopicRepository : ITopicRepository
    {
        private readonly IMongoCollection<Topic> _topics = null;

        public TopicRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _topics = dbContext.Topics;
        }

        public Topic Add(Topic param)
        {
            _topics.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAll()
        {
            return _topics.Find(x => true).ToList();
        }

        public Topic GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Topic Update(Topic param)
        {
            throw new NotImplementedException();
        }
    }
}

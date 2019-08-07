using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _topics.FindOneAndDelete(c=>c.Id.Equals(id));
            return true;
        }

        public bool DeleteMany(List<string> topics)
        {
            //var result = _topics.DeleteMany(Builders<Topic>.Filter.In(t => t.Id, topics));
            var updateDefinition = Builders<Topic>.Update
                .Set("is_active", false);

            var result = _topics.UpdateMany(Builders<Topic>.Filter.In(t => t.Id, topics), updateDefinition);

            return result.IsAcknowledged;
        }

        public IEnumerable<Topic> GetAll()
        {
            return _topics.Find(x => x.IsActive).ToList();
        }

        public Topic GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Topic InsertOrUpdate(Topic topic)
        {
            var updateDefinition = Builders<Topic>.Update
                .Set("name", topic.Name)
                .Set("img_url", topic.ImgUrl);

            var result = _topics.UpdateOne(
                a => a.Id == topic.Id,
                updateDefinition,
                new UpdateOptions { IsUpsert = true });

            if (!result.IsAcknowledged)
            {
                return null;
            }
            return topic;
        }

        public Topic Update(Topic param)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository = null;

        public TopicService(IOptions<AppSettings> settings)
        {
            _topicRepository = new TopicRepository(settings);
        }

        public Topic Add(Topic param)
        {
            _topicRepository.Add(param);
            return param;
        }

        public bool Delete(string topicId)
        {
           return _topicRepository.Delete(topicId);
        }

        public bool DeleteMany(List<string> topics)
        {
            return _topicRepository.DeleteMany(topics);
        }

        public IEnumerable<Topic> GetAll()
        {
            return _topicRepository.GetAll();
        }
    }
}

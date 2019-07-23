using System.Collections.Generic;
using PostService.Models;

namespace PostService.Services.Interfaces
{
    public interface ITopicService
    {
        Topic Add(Topic param);

        IEnumerable<Topic> GetAll();

        bool Delete(string topicId);

        bool DeleteMany(List<string> topics);

        Topic InsertOrUpdate(Topic topic);
    }
}
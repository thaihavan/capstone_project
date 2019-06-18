using System.Collections.Generic;
using PostService.Models;

namespace PostService.Services.Interfaces
{
    public interface ITopicService
    {
        Topic Add(Topic param);
        IEnumerable<Topic> GetAll();
    }
}
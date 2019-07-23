using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
        bool DeleteMany(List<string> topics);

        Topic InsertOrUpdate(Topic topic);
    }
}

using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
        IEnumerable<Topic> GetAll();

        Topic GetById(string id);

        Topic Add(Topic param);

        bool Delete(string id);

        Topic Update(Topic param);
    }
}

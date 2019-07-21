using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();

        Post GetById(string id);

        Post Add(Post post);

        Post Update(Post post);

        bool Delete(string id);

        object GetAllPostStatistics(StatisticsFilter filter);
    }
}

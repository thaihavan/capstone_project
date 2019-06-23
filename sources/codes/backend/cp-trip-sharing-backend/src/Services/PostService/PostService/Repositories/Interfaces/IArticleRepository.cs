using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {

        IEnumerable<Article> GetAllArticleWithPost();

        IEnumerable<Article> GetAllArticleByUser(string userId);

        IEnumerable<Article> GetAllArticleInfo();

        Article GetArticleInfoById(string id);

        IEnumerable<object> GetAllArticleInfo(string userId);
    }
}

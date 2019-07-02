using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetAllArticleByUser(string userId);

        IEnumerable<Article> GetAllArticleByUser(string userId, PostFilter postFilter);

        IEnumerable<Article> GetAllArticleInfo();

        Article GetArticleInfoById(string id, string userId);

        IEnumerable<Article> GetAllArticleInfo(string userId);

        IEnumerable<Article> GetAllArticleInfo(string userId, PostFilter postFilter);

        IEnumerable<Article> GetAllArticleInfo(PostFilter postFilter);
    }
}

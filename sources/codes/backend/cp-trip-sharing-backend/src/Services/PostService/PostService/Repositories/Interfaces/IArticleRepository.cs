using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter);

        Article GetArticleById(string id, string userId) ;

        IEnumerable<Article> GetAllArticles(string userId, PostFilter postFilter);

        IEnumerable<Article> GetAllArticles(PostFilter postFilter);
    }
}

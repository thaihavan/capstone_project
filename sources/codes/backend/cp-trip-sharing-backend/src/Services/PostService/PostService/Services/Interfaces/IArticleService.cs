using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IArticleService
    {
        Article GetArticleById(string id, string userId);

        Article Add(Article article);

        Article Update(Article article);

        bool Delete(string id);

        IEnumerable<Article> GetAllArticles(PostFilter postFilter, int page);

        IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter, int page);
    }
}

using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticleInfo();

        Article GetArticleInfoById(string id);

        IEnumerable<Article> GetAllArticleByUser(string userId);

        IEnumerable<Article> GetAllArticleByUser(string userId, PostFilter postFilter);

        Article GetById(string id);

        Article Add(Article article);

        Article Update(Article article);

        bool Delete(string id);

        IEnumerable<Article> GetAllArticleInfo(string userId);

        IEnumerable<Article> GetAllArticleInfo(string userId, PostFilter postFilter);

        IEnumerable<Article> GetAllArticleInfo(PostFilter postFilter);
    }
}

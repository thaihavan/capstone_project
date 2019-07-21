using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter, int page);

        Article GetArticleById(string id, string userId) ;

        IEnumerable<Article> GetAllArticles(PostFilter postFilter, int page);

        IEnumerable<Article> GetRecommendArticles(PostFilter postFilter, UserInfo userInfo, int page);

        IEnumerable<Article> GetPopularArticles(PostFilter postFilter, int page);

        object GetArticleStatistics(StatisticsFilter filter);
    }
}

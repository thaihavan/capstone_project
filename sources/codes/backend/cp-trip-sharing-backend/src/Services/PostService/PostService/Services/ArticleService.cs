using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository = null;

        public ArticleService(IOptions<AppSettings> settings)
        {
            _articleRepository = new ArticleRepository(settings);
        }

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public Article Add(Article article)
        {
            return _articleRepository.Add(article);
        }

        public bool Delete(string id)
        {
            return _articleRepository.Delete(id);
        }

        public Article GetArticleById(string id, string userId)
        {
            return _articleRepository.GetArticleById(id, userId);
        }

        public Article Update(Article article)
        {
            return _articleRepository.Update(article);
        }

        public IEnumerable<Article> GetAllArticles(PostFilter postFilter, int page)
        {
            return _articleRepository.GetAllArticles(postFilter, page);
        }

        public IEnumerable<Article> GetAllArticlesByUser(string userId, PostFilter postFilter, int page)
        {
            return _articleRepository.GetAllArticlesByUser(userId, postFilter, page);
        }

        public IEnumerable<Article> GetRecommendArticles(PostFilter postFilter, UserInfo userInfo, int page)
        {
            // TODO: Get UserInfo
            // TODO: Query Db
            return _articleRepository.GetRecommendArticles(postFilter, userInfo, page);
        }

        public IEnumerable<Article> GetPopularArticles(PostFilter postFilter, int page)
        {
            return _articleRepository.GetPopularArticles(postFilter, page);
        }
    
    }
}

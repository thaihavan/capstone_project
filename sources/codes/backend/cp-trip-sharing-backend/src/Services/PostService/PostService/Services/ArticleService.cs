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

        public IEnumerable<Article> GetAllArticleInfo()
        {
            return _articleRepository.GetAllArticleInfo();
        }

        public Article GetArticleInfoById(string id, string userId)
        {
            return _articleRepository.GetArticleInfoById(id,userId);
        }

        public Article GetById(string id)
        {
            return _articleRepository.GetById(id);
        }

        public Article Update(Article article)
        {
            return _articleRepository.Update(article);
        }

        public IEnumerable<Article> GetAllArticleByUser(string userId)
        {
            return _articleRepository.GetAllArticleByUser(userId);
        }

        public IEnumerable<Article> GetAllArticleInfo(string userId)
        {
            return _articleRepository.GetAllArticleInfo(userId);
        }

        public IEnumerable<Article> GetAllArticleInfo(string userId, PostFilter postFilter)
        {
            return _articleRepository.GetAllArticleInfo(userId, postFilter);
        }

        public IEnumerable<Article> GetAllArticleInfo(PostFilter postFilter)
        {
            return _articleRepository.GetAllArticleInfo(postFilter);
        }

        public IEnumerable<Article> GetAllArticleByUser(string userId, PostFilter postFilter)
        {
            return _articleRepository.GetAllArticleByUser(userId, postFilter);
        }
    }
}

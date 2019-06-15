using PostService.Models;
using PostService.Repositories;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleRepository _articleRepository = null;

        public ArticleService()
        {
            _articleRepository = new ArticleRepository();
        }

        public Article Add(Article article)
        {
            return _articleRepository.Add(article);
        }

        public bool Delete(string id)
        {
            return _articleRepository.Delete(id);
        }

        public IEnumerable<Article> GetAllArticleWithPost()
        {
            return _articleRepository.GetAllArticleWithPost();
        }

        public Article GetById(string id)
        {
            return _articleRepository.GetById(id);
        }

        public Article Update(Article article)
        {
            return _articleRepository.Update(article);
        }
    }
}

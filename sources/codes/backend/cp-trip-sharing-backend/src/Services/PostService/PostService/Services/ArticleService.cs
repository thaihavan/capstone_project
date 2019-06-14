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
        private ArticleRepository _articleRepository = null;

        public ArticleService()
        {
            _articleRepository = new ArticleRepository();
        }

        public IEnumerable<Article> GetAllArticleWithPost()
        {
            return _articleRepository.GetAllArticleWithPost();
        }

        public Article GetById(string id)
        {
            return _articleRepository.GetById(id);
        }
    }
}

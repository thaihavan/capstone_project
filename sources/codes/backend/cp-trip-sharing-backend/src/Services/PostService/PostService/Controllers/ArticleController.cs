using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Services;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IArticleService _articleService = null;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("all")]
        public IActionResult GetAllArticleWithPost()
        {
            var articles = _articleService.GetAllArticleWithPost();
            return Ok(articles);
        }

        [HttpGet]
        public IActionResult GetById([FromQuery] string id)
        {
            var article = _articleService.GetById(id);
            return Ok(article);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PostService.Models;
using PostService.Services;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService = null;
        private readonly IPostService _postService = null;

        public ArticleController(IArticleService articleService, IPostService postService)
        {
            _articleService = articleService;
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllArticleWithPost()
        {
            var articles = _articleService.GetAllArticleWithPost();
            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById([FromQuery] string id)
        {
            var article = _articleService.GetById(id);
            return Ok(article);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult CreateArticle([FromBody] Article article)
        {
            var authorId = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;

            if (!article.Post.Author.AuthorId.Equals(new BsonObjectId(authorId)))
            {
                return Unauthorized();
            }

            Post addedPost = _postService.Add(article.Post);
            Article addedArticle = _articleService.Add(article);

            return Ok(addedArticle);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult UpdateArticle([FromBody] Article article)
        {
            var authorId = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;

            if (!article.Post.Author.AuthorId.Equals(new BsonObjectId(authorId)))
            {
                return Unauthorized();
            }

            if (article.Post.Id != article.PostId)
            {
                return BadRequest(new ErrorMessage() { Message = "PostId doesn't match." });
            }

            Post updatedPost = _postService.Add(article.Post);
            Article updatedArticle = _articleService.Update(article);

            return Ok(updatedArticle);
        }
    }
}
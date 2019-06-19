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
        public IActionResult GetAllArticleInfo()
        {
            var articles = _articleService.GetAllArticleInfo();
            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById([FromQuery] string id)
        {
            var article = _articleService.GetById(id);
            return Ok(article);
        }

        [AllowAnonymous]

        [HttpGet("userid")]
        public IActionResult GetByUserId([FromQuery] string id)
        {
            var article = _articleService.GetAllArticleByUser(id);
            return Ok(article);
        }
        [HttpGet("full")]
        public IActionResult GetArticleInfoById([FromQuery] string id)
        {
            var article = _articleService.GetArticleInfoById(id);

            return Ok(article);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult CreateArticle([FromBody] Article article)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if (article.Post.Author == null)
            {
                article.Post.Author = new Author();
            }
            article.Post.Author.AuthorId = new BsonObjectId(userId);

            article.Id = new BsonObjectId(ObjectId.GenerateNewId());
            article.PostId = new BsonObjectId(ObjectId.GenerateNewId());
            article.Post.Id = article.PostId;
            article.Post.LikeCount = 0;  
            article.Post.CommentCount = 0;

            Post addedPost = _postService.Add(article.Post);
            Article addedArticle = _articleService.Add(article);

            return Ok(addedArticle);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult UpdateArticle([FromBody] Article article)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if (!article.Post.Author.AuthorId.Equals(new BsonObjectId(userId)))
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
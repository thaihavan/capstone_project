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
        private readonly IAuthorService _authorService = null;

        public ArticleController(IArticleService articleService, IPostService postService)
        {
            _articleService = articleService;
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllArticleInfo()
        {
            IEnumerable<object> articles = null;
            var identity = (ClaimsIdentity)User.Identity;
            if (User.Identity.IsAuthenticated)
            {
                var userId = identity.FindFirst("user_id").Value;
                articles = _articleService.GetAllArticleInfo(userId);         
            }else
            {
                articles = _articleService.GetAllArticleInfo();
            }           
            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpPost("all")]
        public IActionResult GetAllArticles(PostFilter postFilter)
        {
            IEnumerable<object> articles = null;
            var identity = (ClaimsIdentity)User.Identity;
            if (User.Identity.IsAuthenticated)
            {
                var userId = identity.FindFirst("user_id").Value;
                articles = _articleService.GetAllArticleInfo(userId, postFilter);
            }
            else
            {
                articles = _articleService.GetAllArticleInfo(postFilter);
            }
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
        [HttpPost("user")]
        public IActionResult GetByUserId([FromQuery] string userId, [FromBody] PostFilter postFilter)
        {
            var article = _articleService.GetAllArticleByUser(userId, postFilter);
            return Ok(article);
        }

        [AllowAnonymous]
        [HttpGet("user")]
        public IActionResult GetByUserId([FromQuery] string userId)
        {
            var article = _articleService.GetAllArticleByUser(userId);
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

            article.Post.AuthorId = userId;
            article.Id = ObjectId.GenerateNewId().ToString();
            article.PostId = ObjectId.GenerateNewId().ToString();
            article.Post.Id = article.PostId;
            article.Post.LikeCount = 0;
            article.Post.CommentCount = 0;
            article.Post.PubDate = DateTime.Now;

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

            if (!article.Post.AuthorId.Equals(new ObjectId(userId)))
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
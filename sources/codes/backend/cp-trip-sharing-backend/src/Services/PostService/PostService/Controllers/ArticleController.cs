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
        [HttpPost("all")]
        public IActionResult GetAllArticles(PostFilter postFilter)
        {
            IEnumerable<Article> articles = _articleService.GetAllArticles(postFilter);
            return Ok(articles);
        }

        [AllowAnonymous]
        [HttpPost("user")]
        public IActionResult GetAllArticlesByUser([FromQuery] string userId, [FromBody] PostFilter postFilter)
        {
            var article = _articleService.GetAllArticlesByUser(userId, postFilter);
            return Ok(article);
        }

        [AllowAnonymous]
        [HttpGet("full")]
        public IActionResult GetArticleById([FromQuery] string id)
        {
            Article article = null;
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var userId = identity.FindFirst("user_id").Value;
                article = _articleService.GetArticleById(id, userId);
            }
            else
            {
                article = _articleService.GetArticleById(id, String.Empty);
            }          
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
            article.Post.IsActive = true;

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

            if (!article.Post.AuthorId.Equals((userId)))
            {
                return Unauthorized();
            }

            if (article.Post.Id != article.PostId)
            {
                return BadRequest(new ErrorMessage() { Message = "PostId doesn't match." });
            }

            Post updatedPost = _postService.Update(article.Post);
            Article updatedArticle = _articleService.Update(article);

            return Ok(updatedArticle);
        }

        [Authorize(Roles = "member")]
        [HttpDelete("remove")]
        public IActionResult RemoveArticle([FromQuery] string articleId)
        {
            var result = _articleService.Delete(articleId);
            if(!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
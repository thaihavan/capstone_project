using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Services.Interfaces;
using PostService.Services;
using Microsoft.AspNetCore.Authorization;
using PostService.Models;
using MongoDB.Bson;
using System.Security.Claims;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService = null;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _postService.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] Post postParam) {
            var result = _postService.Add(postParam);
            if (result == null)
            {
                return BadRequest(new ErrorMessage { Message = "Error" });
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById([FromQuery]string postId)
        {
            var result = _postService.GetById(postId);
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] Post post)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if (!post.Author.AuthorId.Equals(new BsonObjectId(userId)))
            {
                return Unauthorized();
            }

            Post updatedPost = _postService.Add(post);

            return Ok(updatedPost);
        }
    }
}
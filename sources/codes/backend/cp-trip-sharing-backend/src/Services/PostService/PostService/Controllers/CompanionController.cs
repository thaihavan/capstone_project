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
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class CompanionController : ControllerBase
    {
        private readonly ICompanionPostService _companionPostService = null;
        private readonly IPostService _postService = null;

        public CompanionController(ICompanionPostService companionPostService, IPostService postService)
        {
            _companionPostService = companionPostService;
            _postService = postService;
        }

        [Authorize(Roles ="member")]
        [HttpPost("post/create")]
        public IActionResult Create([FromBody]CompanionPost param )
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            //generate new postid and new conversationid 
            var postId = ObjectId.GenerateNewId().ToString();
            var conversationId = ObjectId.GenerateNewId().ToString();

            param.Post.AuthorId = userId;
            param.Post.Id = postId;
            param.PostId = postId;
            param.ConversationId = conversationId;

            _postService.Add(param.Post);
            var result = _companionPostService.Add(param);
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpPost("post/update")]
        public IActionResult Update([FromBody]CompanionPost param)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            //generate new postid and new conversationid 
            var postId = ObjectId.GenerateNewId().ToString();
            var conversationId = ObjectId.GenerateNewId().ToString();

            param.Post.AuthorId = userId;
            param.Post.Id = postId;
            param.PostId = postId;
            param.ConversationId = conversationId;

            _postService.Add(param.Post);
            var result = _companionPostService.Add(param);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("post")]
        public IActionResult GetCompanionPostById([FromQuery]string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = User.Identity.IsAuthenticated? identity.FindFirst("user_id").Value:null;
            var result = _companionPostService.GetById(id,userId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("post/all")]
        public IActionResult GetAllCompanionPost()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = User.Identity.IsAuthenticated ? identity.FindFirst("user_id").Value : null;
            var result = _companionPostService.GetAll(userId);
            return Ok(result);
        }
    }
}
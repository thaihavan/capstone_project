using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [HttpPost("post/all")]
        public IActionResult GetAllCompanionPost([FromBody]PostFilter filter,[FromQuery]int page)
        {
            var result = _companionPostService.GetAll(filter, page);
            return Ok(result);
        }

        [Authorize(Roles ="member")]
        [HttpDelete("post")]
        public IActionResult DeleteCompanionPost([FromQuery]string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = User.Identity.IsAuthenticated ? identity.FindFirst("user_id").Value : null;
            var post = _companionPostService.GetById(id);
            if (!userId.Equals(post.Post.AuthorId)) return Unauthorized();
            var result = _companionPostService.Delete(id);
            return Ok(new { Message = "Success" });
        }

        [Authorize(Roles = "member")]
        [HttpPost("post/join")]
        public IActionResult JoinCompanion([FromBody]CompanionPostJoinRequest request)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            request.UserId = userId;
            var result=_companionPostService.AddNewRequest(request);
            
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpGet("post/requests")]
        public IActionResult GetAllRequest([FromQuery]string companionPostId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var companionPost = _companionPostService.GetById(companionPostId);
            if (!companionPost.Post.AuthorId.Equals(userId))
            {
                return Unauthorized();
            }else
            {
                return Ok(_companionPostService.GetAllJoinRequest(companionPostId));
            }           
        }

        [Authorize(Roles = "member")]
        [HttpDelete("post/request")]
        public  IActionResult DeleteRequest([FromBody]CompanionPostJoinRequest request)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var companionPost = _companionPostService.GetById(request.CompanionPostId);

            if (!companionPost.Post.AuthorId.Equals(userId))
            {
                return Unauthorized();
            }
            else
            {
                _companionPostService.DeleteJoinRequest(request.Id);
                return Ok();
            }
        }

        [Authorize(Roles = "member")]
        [HttpDelete("post/request/cancel")]
        public IActionResult CancelRequest([FromBody]string requestId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("user_id").Value;

            var request = _companionPostService.GetRequestById(requestId);
            if (!request.UserId.Equals(userId))
            {
                return Unauthorized();
            }else
            {
                return Ok(_companionPostService.DeleteJoinRequest(requestId));
            }
        }

        [AllowAnonymous]
        [HttpPost("post/user")]
        public IActionResult GetAllCompanionPostByUser([FromBody]PostFilter filter, [FromQuery]string userId, [FromQuery]int page)
        {
            return Ok(_companionPostService.GetAllCompanionPostByUser(userId, filter, page));
        }
    }
}
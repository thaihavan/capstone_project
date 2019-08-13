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
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService = null;
        private readonly IAuthorService _authorService = null;

        public CommentController(ICommentService commentService, IAuthorService authorService)
        {
            _commentService = commentService;
            _authorService = authorService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetCommentByPost([FromQuery] string id)
        {
            IEnumerable<Comment> comments = null;
            var identity = (ClaimsIdentity)User.Identity;
            if (User.Identity.IsAuthenticated)
            {
                var userId = identity.FindFirst("user_id").Value;
                comments = _commentService.GetCommentByPost(id,userId);
            }
            else
            {
                comments = _commentService.GetCommentByPost(id);
            }
            return Ok(comments);
        }

        [Authorize(Roles = "member")]
        [HttpPost("add")]
        public IActionResult AddComment([FromBody] Comment param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            // Set comment property
            param.Id = ObjectId.GenerateNewId().ToString();
            param.AuthorId = userId;
            param.IsActive = true;
            param.Date = DateTime.Now;
            param.Author = _authorService.GetById(param.AuthorId);

            _commentService.Add(param);
            return Ok(param);
        }

        [Authorize(Roles = "member")]
        [HttpDelete("delete")]
        public IActionResult DelComment([FromQuery] string id, string authorId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if (!authorId.Equals(userId))
            {
                return Unauthorized();
            }
            _commentService.Delete(id);
            return Ok();
        }

        [Authorize(Roles = "member, admin")]
        [HttpPut("update")]
        public IActionResult UpdateComment([FromBody] Comment param)
        {
            _commentService.Update(param);
            return Ok(param);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService = null;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize(Roles = "member")]
        [HttpGet("all")]
        public IActionResult GetCommentByPost([FromQuery] string id)
        {
            var cmts = _commentService.GetCommentByPost(id);
            if (cmts != null)
            {
                return Ok(cmts);
            }
            return BadRequest();
        }

        [Authorize(Roles = "member")]
        [HttpPost("comment")]
        public IActionResult AddComment([FromBody] Comment param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if(!param.UserId.Equals(new MongoDB.Bson.BsonObjectId(userId)))
            {
                return Unauthorized();
            }
            _commentService.Add(param);
            return Ok(param);
        }

        [Authorize(Roles = "member")]
        [HttpDelete("uncomment")]
        public IActionResult DelComment([FromQuery] string id, string authorId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if (!authorId.Equals(new MongoDB.Bson.BsonObjectId(userId)))
            {
                return Unauthorized();
            }
            _commentService.Delete(id);
            return Ok(id);
        }

        [Authorize(Roles = "member")]
        [HttpPut("updatecommnet")]
        public IActionResult UpdateComment([FromBody] Comment param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if (!param.UserId.Equals(new MongoDB.Bson.BsonObjectId(userId)))
            {
                return Unauthorized();
            }
            _commentService.Update(param);
            return Ok(param);
        }
    }
}
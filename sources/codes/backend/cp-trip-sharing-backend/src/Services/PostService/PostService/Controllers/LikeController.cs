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
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService = null;
        private readonly IPostService _postService = null;

        public LikeController(ILikeService likeService, IPostService postService)
        {
            _likeService = likeService;
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllLikeWithPost()
        {
            var likes = _likeService.GetLikeWithPost();
            return Ok(likes);
        }

        [Authorize(Roles = "member")]
        [HttpPost("like")]
        public IActionResult Like([FromBody] Like param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if(!param.UserId.Equals(new MongoDB.Bson.BsonObjectId(userId)))
            {
                return Unauthorized();
            }
            _likeService.Add(param);
            return Ok(param);
        }

        [Authorize(Roles = "member")]
        [HttpDelete("unlike")]
        public IActionResult Unlike([FromQuery] string objectId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            if(!_likeService.Delete(objectId, userId))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
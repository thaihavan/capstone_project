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
    [Authorize]
    [ApiController]
    public class VirtualTripController : ControllerBase
    {
        private readonly IVirtualTripService _virtualTripService = null;
        private readonly IPostService _postService = null;

        public VirtualTripController(IVirtualTripService virtualTripService, IPostService postService)
        {
            _virtualTripService = virtualTripService;
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllVirtualTripWithPost()
        {
            IEnumerable<VirtualTrip> virtualTrips = null;
            var identity = (ClaimsIdentity)User.Identity;
            if (User.Identity.IsAuthenticated)
            {
                var userId = identity.FindFirst("user_id").Value;
                virtualTrips = _virtualTripService.GetAllVirtualTripWithPost(userId);
            }
            else
            {
                virtualTrips = _virtualTripService.GetAllVirtualTripWithPost();
            }
            return Ok(virtualTrips);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetVirtualTrip([FromQuery] string id)
        {
            var virtualTrip = _virtualTripService.GetVirtualTrip(id);
            return Ok(virtualTrip);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult CreateArticle([FromBody] VirtualTrip virtualTrip)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            virtualTrip.Post.AuthorId = userId;
            virtualTrip.Id = ObjectId.GenerateNewId().ToString();
            virtualTrip.PostId = ObjectId.GenerateNewId().ToString();
            virtualTrip.Post.Id = virtualTrip.PostId;
            virtualTrip.Post.LikeCount = 0;
            virtualTrip.Post.CommentCount = 0;
            virtualTrip.Post.PubDate = DateTime.Now;

            Post addedPost = _postService.Add(virtualTrip.Post);
            VirtualTrip addedVirtualTrip = _virtualTripService.Add(virtualTrip);

            return Ok(addedVirtualTrip);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult UpdateArticle([FromBody] VirtualTrip virtualTrip)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if (!virtualTrip.Post.AuthorId.Equals(userId))
            {
                return Unauthorized();
            }

            if (virtualTrip.Post.Id != virtualTrip.PostId)
            {
                return BadRequest(new ErrorMessage() { Message = "PostId doesn't match." });
            }

            Post updatedPost = _postService.Update(virtualTrip.Post);
            VirtualTrip updatedArticle = _virtualTripService.Update(virtualTrip);

            return Ok(updatedArticle);
        }

        //[Authorize(Roles = "member")]
        //[HttpDelete("")]
    }
}
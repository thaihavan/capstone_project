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
        [HttpPost("all")]
        public IActionResult GetAllVirtualTrips([FromBody] PostFilter postFilter, [FromQuery]int page)
        {
            var virtualTrips = _virtualTripService.GetAllVirtualTrips(postFilter, page);

            return Ok(virtualTrips);
        }

        [AllowAnonymous]
        [HttpPost("user")]
        public IActionResult GetAllVirtualTrips([FromQuery] string userId, [FromBody] PostFilter postFilter, [FromQuery]int page)
        {
            var virtualTrips = _virtualTripService.GetAllVirtualTripsByUser(userId, postFilter, page);

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
        public IActionResult CreateVirtualTrip([FromBody] VirtualTrip virtualTrip)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            virtualTrip.Post.AuthorId = userId;
            virtualTrip.Id = ObjectId.GenerateNewId().ToString();
            virtualTrip.PostId = virtualTrip.Id;
            virtualTrip.Post.Id = virtualTrip.PostId;
            virtualTrip.Post.LikeCount = 0;
            virtualTrip.Post.CommentCount = 0;
            virtualTrip.Post.PubDate = DateTime.Now;
            virtualTrip.Post.IsActive = true;
            virtualTrip.Post.PostType = "VirtualTrip";

            Post addedPost = _postService.Add(virtualTrip.Post);
            VirtualTrip addedVirtualTrip = _virtualTripService.Add(virtualTrip);

            return Ok(addedVirtualTrip);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult UpdateVirtualTrip([FromBody] VirtualTrip virtualTrip)
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
    }
}
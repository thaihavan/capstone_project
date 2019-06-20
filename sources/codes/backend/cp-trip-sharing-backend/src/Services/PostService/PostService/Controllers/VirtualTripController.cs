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
            var virtualTrips = _virtualTripService.GetAllVirtualTripWithPost();
            return Ok(virtualTrips);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById([FromQuery] string id)
        {
            var virtualTrip = _virtualTripService.GetById(id);
            return Ok(virtualTrip);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult CreateArticle([FromBody] VirtualTrip virtualTrip)
        {
            if (virtualTrip.Post.Id != virtualTrip.PostId)
            {
                return BadRequest(new ErrorMessage() { Message = "PostId doesn't match." });
            }
            virtualTrip.Id = new BsonObjectId(ObjectId.GenerateNewId());

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

            if (!virtualTrip.Post.AuthorId.Equals(new ObjectId(userId)))
            {
                return Unauthorized();
            }

            if (virtualTrip.Post.Id != virtualTrip.PostId)
            {
                return BadRequest(new ErrorMessage() { Message = "PostId doesn't match." });
            }

            Post updatedPost = _postService.Add(virtualTrip.Post);
            VirtualTrip updatedArticle = _virtualTripService.Update(virtualTrip);

            return Ok(updatedArticle);
        }
    }
}
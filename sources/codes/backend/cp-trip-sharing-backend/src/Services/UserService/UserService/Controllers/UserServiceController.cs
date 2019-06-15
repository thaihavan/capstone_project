using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserServices.Models;
using UserServices.Services;
using UserServices.Services.Interfaces;

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IUserService _userService = null;

        public UserServiceController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "member")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User userParam)
        {
            var result = _userService.Add(userParam);
            return Created("", userParam);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("All")]
        public IActionResult GetAll([FromBody] User userParam)
        {
            var result = _userService.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("User")]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            var result = _userService.GetUserById(userId);
            return Ok(result);
        }

        // POST: api/UserServices/follow
        // body: { following : "id" }
        [Authorize(Roles = "member")]
        [HttpPost("follow")]
        public IActionResult Follow([FromBody] Follow param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            param.Follower =  ObjectId.Parse(userId);
            if (_userService.AddFollows(param) != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/UserServices/unfollow
        // body: { following : "id" }
        [Authorize(Roles = "member")]
        [HttpDelete("unfollow")]
        public IActionResult Unfollow([FromQuery] string followingId)
        {
            var param = new Follow
            {
                Following = ObjectId.Parse(followingId)
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            param.Follower = new ObjectId(userId);
            if (_userService.Unfollow(param) != null)
            {
                return Ok(param);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/UserService/bookmark
        // body { postId : "id" }
        [Authorize(Roles = "member")]
        [HttpPost("bookmark")]
        public IActionResult Bookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = new ObjectId(userId);
            if (_userService.AddBookmark(bookmark) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/UserServices/deletebookmark
        // body { postId : "id" }
        [Authorize(Roles ="member")]
        [HttpDelete("deletebookmark")]
        public IActionResult DeleteBookmark([FromQuery] string postId)
        {
            var bookmark = new Bookmark
            {
                PostId = new ObjectId(postId)
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = new ObjectId(userId);
            if (_userService.DeleteBookmark(bookmark) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/UserServices/allphoto
        [HttpGet("allphoto")]
        public IActionResult GetAllPhoto()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            var photos = _userService.GetAllPhoto(userId);
            return new OkObjectResult(photos);
        }

        // POST: api/UserServices/addphoto
        // body: { url : "url", date : "date" }
        [HttpPost("addphoto")]
        public IActionResult AddPhoto([FromBody] Photo photo)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            photo.Author = new ObjectId(userId);
            if (_userService.AddPhoto(photo) != null)
            {
                return Ok();
            }
            return NotFound();
        }

        // POST: api/UserServices/addblock
        // body: { blockedId : "id" }
        [HttpPost("addblock")]
        public IActionResult AddBlock([FromBody] Block block)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = new ObjectId(userId);
            if (_userService.Block(block) != null)
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/UserServices/unblock
        // body { BlockedId : "id" }
        [HttpDelete("unblock")]
        public IActionResult UnBlock([FromBody] Block block)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = new ObjectId(userId);
            if (_userService.UnBlock(block) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

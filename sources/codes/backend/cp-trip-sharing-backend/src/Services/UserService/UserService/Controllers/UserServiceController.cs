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
        [Authorize(Roles = "member")]
        [HttpPost("follow")]
        public IActionResult Follow([FromQuery] string following)
        {
            var follow = new Follow()
            {
                Following = new BsonObjectId(ObjectId.Parse(following))
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            follow.Follower = new BsonObjectId(ObjectId.Parse(userId));
            if (_userService.AddFollows(follow) != null)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/UserServices/unfollow
        [Authorize(Roles = "member")]
        [HttpDelete("unfollow")]
        public IActionResult Unfollow([FromQuery] string following)
        {
            var follow = new Follow
            {
                Following = ObjectId.Parse(following)
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            follow.Follower = new ObjectId(userId);
            if (_userService.Unfollow(follow) != null)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/UserService/bookmark
        [Authorize(Roles = "member")]
        [HttpPost("bookmark")]
        public IActionResult Bookmark([FromQuery] string postId)
        {
            var bookmark = new Bookmark()
            {
                PostId = new BsonObjectId(ObjectId.Parse(postId))
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = new BsonObjectId(ObjectId.Parse(userId));
            if (_userService.AddBookmark(bookmark) != null)
            {
                return Ok(bookmark);
            }
            else
            {
                return BadRequest();
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
                PostId = new BsonObjectId(ObjectId.Parse(postId))
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = new BsonObjectId(ObjectId.Parse(userId));
            if (_userService.DeleteBookmark(bookmark) != null)
            {
                return Ok(bookmark);
            }
            else
            {
                return BadRequest();
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
        [HttpPost("addphoto")]
        public IActionResult AddPhoto([FromQuery] string url,[FromQuery] DateTime date)
        {
            var photo = new Photo()
            {
                Url = url,
                Date = date
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            photo.Author = new BsonObjectId(ObjectId.Parse(userId));
            if (_userService.AddPhoto(photo) != null)
            {
                return Ok(photo);
            }
            return BadRequest();
        }

        // POST: api/UserServices/addblock
        [HttpPost("addblock")]
        public IActionResult AddBlock([FromQuery] string blocked)
        {
            var block = new Block()
            {
                BlockedId = new BsonObjectId(ObjectId.Parse(blocked))
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = new BsonObjectId(ObjectId.Parse(userId));
            if (_userService.Block(block) != null)
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/UserServices/unblock
        [HttpDelete("unblock")]
        public IActionResult UnBlock([FromQuery] string blocked)
        {
            var block = new Block()
            {
                BlockedId = new BsonObjectId(ObjectId.Parse(blocked))
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = new BsonObjectId(ObjectId.Parse(userId));
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

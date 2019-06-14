using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        //just test
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var follows = _userService.GetAll();
            return new OkObjectResult(follows);
        }

        // POST: api/UserServices/follow
        // body: { following : "id" }
        [HttpPost("follow")]
        public IActionResult Follow([FromBody] Follow param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            param.Follower = new ObjectId(userId);
            if (_userService.AddFollows(param))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/UserServices/unfollow
        // body: { following : "id" }
        [HttpDelete("unfollow")]
        public IActionResult Unfollow([FromBody] Follow follows)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            follows.Follower = new ObjectId(userId);
            if (_userService.Unfollow(follows))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/UserService/bookmark
        // body { postId : "id" }
        [HttpPost("bookmark")]
        public IActionResult Bookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            bookmark.UserId = new ObjectId(userId);
            if (_userService.AddBookmark(bookmark))
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
        [HttpDelete("deletebookmark")]
        public IActionResult DeleteBookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            bookmark.UserId = new ObjectId(userId);
            if (_userService.DeleteBookmark(bookmark))
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
            var userId = identity.FindFirst("user_Id").Value;
            var photos = _userService.GetAllPhoto(userId);
            return new OkObjectResult(photos);
        }

        // POST: api/UserServices/addphoto
        // body: { url : "url", date : "date" }
        [HttpPost("addphoto")]
        public IActionResult AddPhoto([FromBody] Photo photo)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            photo.Author = new ObjectId(userId);
            if (_userService.AddPhoto(photo))
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
            var userId = identity.FindFirst("user_Id").Value;
            block.BlockerId = new ObjectId(userId);
            if (_userService.Block(block))
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
            var userId = identity.FindFirst("user_Id").Value;
            block.BlockerId = new ObjectId(userId);
            if (_userService.UnBlock(block))
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

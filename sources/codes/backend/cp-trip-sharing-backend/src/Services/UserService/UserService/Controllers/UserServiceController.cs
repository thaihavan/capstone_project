using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        // param: { following : "id" }
        [HttpPost("follow")]
        public IActionResult Follow([FromBody] Follow follows)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            follows.Follower = userId;
            if (_userService.AddFollows(follows))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/UserServices/unfollow
        // param: { following : "id" }
        [HttpDelete("unfollow")]
        public IActionResult Unfollow([FromBody] Follow follows)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            follows.Follower = userId;
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
        // param { postId : "id" }
        [HttpPost("bookmark")]
        public IActionResult Bookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            bookmark.UserId = userId;
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
        // param { postId : "id" }
        [HttpDelete("deletebookmark")]
        public IActionResult DeleteBookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            bookmark.UserId = userId;
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
            var photos = _userService.GetAllPhotos(userId);
            return new OkObjectResult(photos);
        }

        // POST: api/UserServices/addphoto
        // param: { url : "url", date : "date" }
        [HttpPost("addphoto")]
        public IActionResult AddPhoto([FromBody] Photo photo)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            photo.Author = userId;
            if (_userService.AddPhoto(photo))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}

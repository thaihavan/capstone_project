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
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService = null;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        // POST: api/postservice/bookmark
        [Authorize(Roles = "member")]
        [HttpPost("bookmark")]
        public IActionResult Bookmark([FromBody] Bookmark bookmark)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = userId;
            if (_bookmarkService.AddBookmark(bookmark) != null)
            {
                return Ok(bookmark);
            }
            else
            {
                return BadRequest(bookmark);
            }
        }

        // DELETE: api/postservice/bookmark
        // body { postId : "id" }
        [Authorize(Roles = "member")]
        [HttpDelete("bookmark")]
        public IActionResult DeleteBookmark([FromQuery] string id)
        {
            var bookmark = _bookmarkService.GetById(id);
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            bookmark.UserId = userId;
            if (_bookmarkService.DeleteBookmark(id))
            {
                return Ok(bookmark);
            }
            else
            {
                return BadRequest();
            }
        }


        [Authorize(Roles = "member")]
        [HttpGet("bookmark")]
        public IActionResult GetUserBookmarks()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            return Ok(_bookmarkService.GetUserBookmarks(userId));
        }

        [Authorize(Roles = "member")]
        [HttpGet("bookmarkPostId")]
        public IActionResult GetUserBookmarkIds()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            return Ok(_bookmarkService.GetUserBookmarkId(userId));
        }
    }
}
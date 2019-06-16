﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserServices.Controllers
{
    [Route("api/userservice/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService = null;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
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
            if (_bookmarkService.AddBookmark(bookmark) != null)
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
        [Authorize(Roles = "member")]
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
            if (_bookmarkService.DeleteBookmark(bookmark) != null)
            {
                return Ok(bookmark);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
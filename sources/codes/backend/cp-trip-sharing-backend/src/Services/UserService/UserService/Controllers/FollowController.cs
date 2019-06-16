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
using UserServices.Services.Interfaces;

namespace UserServices.Controllers
{
    [Route("api/userservice/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService = null;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

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
            if (_followService.AddFollows(follow) != null)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest();
            }
        }

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
            if (_followService.Unfollow(follow) != null)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
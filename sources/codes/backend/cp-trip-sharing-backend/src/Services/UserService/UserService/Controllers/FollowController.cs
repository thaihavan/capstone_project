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
                Following = following
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            follow.Follower = userId;
            if (userId == following || _followService.AddFollows(follow) == null)
            {
                return BadRequest();
            }

            return Ok(follow);
        }

        [Authorize(Roles = "member")]
        [HttpDelete("unfollow")]
        public IActionResult Unfollow([FromQuery] string following)
        {
            var follow = new Follow
            {
                Following = following
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            follow.Follower = userId;
            if (_followService.Unfollow(follow) != null)
            {
                return Ok(follow);
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("followed")]
        public IActionResult GetCurrentUserFollowed([FromQuery] string following)
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                var userId = identity.FindFirst("user_id").Value;
                return Ok(new { followed = _followService.IsFollowed(userId, following) });
            }
            else return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("follower")]
        public IActionResult GetAllFollower([FromQuery] string userId)
        {
            return Ok(_followService.GetAllFollower(userId));
        }

        [AllowAnonymous]
        [HttpGet("following")]
        public IActionResult GetAllFollowing([FromQuery] string userId)
        {
            return Ok(_followService.GetAllFollowing(userId));
        }

        [AllowAnonymous]
        [HttpGet("followerids")]
        public IActionResult GetAllFollowerId([FromQuery] string userId)
        {
            
            return Ok(_followService.GetAllFollowerId(userId));
        }

        [AllowAnonymous]
        [HttpGet("followingids")]
        public IActionResult GetAllFollowingId([FromQuery] string userId)
        {
            
            return Ok(_followService.GetAllFollowingId(userId));
        }
    }
}
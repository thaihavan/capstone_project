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
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService = null;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "member")]
        [HttpPost("follow")]
        public IActionResult Follow([FromBody] Follow param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_Id").Value;
            param.Follower = ObjectId.Parse(userId);
            if (_userService.AddFollows(param) != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
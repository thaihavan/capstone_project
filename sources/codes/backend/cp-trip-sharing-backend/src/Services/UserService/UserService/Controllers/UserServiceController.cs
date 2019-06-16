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
        

        

        

        //// POST: api/UserServices/addblock
        //[HttpPost("addblock")]
        //public IActionResult AddBlock([FromQuery] string blocked)
        //{
        //    var block = new Block()
        //    {
        //        BlockedId = new BsonObjectId(ObjectId.Parse(blocked))
        //    };
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var userId = identity.FindFirst("user_id").Value;
        //    block.BlockerId = new BsonObjectId(ObjectId.Parse(userId));
        //    if (_userService.Block(block) != null)
        //    {
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        //// DELETE: api/UserServices/unblock
        //[HttpDelete("unblock")]
        //public IActionResult UnBlock([FromQuery] string blocked)
        //{
        //    var block = new Block()
        //    {
        //        BlockedId = new BsonObjectId(ObjectId.Parse(blocked))
        //    };
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var userId = identity.FindFirst("user_id").Value;
        //    block.BlockerId = new BsonObjectId(ObjectId.Parse(userId));
        //    if (_userService.UnBlock(block) != null)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}

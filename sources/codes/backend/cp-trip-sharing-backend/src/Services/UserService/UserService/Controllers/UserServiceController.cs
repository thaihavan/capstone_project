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

    }
}

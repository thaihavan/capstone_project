using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Services.Interfaces;

namespace User.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService = null;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles ="member")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] Models.User userParam)
        {           
            var result = _userService.Add(userParam);
            return Created("", userParam);
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet("All")]
        public IActionResult GetAll([FromBody] Models.User userParam)
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
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
        [HttpPost("register")]
        public IActionResult Register([FromBody] Models.User user)
        {
            //var isRegistered = _userService.GetUserByAccountId(User.Claims.)==null? false :true;
            //if (isRegistered)
            //{
            //    return BadRequest(new { message = "created" });
            //}
            var result = _userService.Add(user);
            return Created("",user);
        }

        [Authorize(Roles = "member")]
        [HttpGet("All")]
        public IActionResult GetAll([FromBody] Models.User user)
        {
            var result = _userService.GetAll();
            return Ok(result);
        }

    }
}
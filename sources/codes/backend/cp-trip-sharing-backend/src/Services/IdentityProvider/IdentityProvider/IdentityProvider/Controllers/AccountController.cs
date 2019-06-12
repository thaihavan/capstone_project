using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityProvider.Models;
using IdentityProvider.Services;
using IdentityProvider.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService = null;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Will be removed, 
        //[Authorize(Roles ="member")]
        //[HttpGet("all")]
        //public IActionResult GetAll()
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var userId = identity.FindFirst("user_id").Value;
        //    return Ok(userId);
        //}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Account accountParam)
        {
            Account account = _accountService.Authenticate(accountParam.Email, accountParam.Password);

            if (account == null||account.Token == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            return Ok(account);
        }

        // Call user service to create new account
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]Account accountParam)
        {
            var result = _accountService.Register(accountParam);
            if (!result)
            {
                return BadRequest(new { message = "Email is in use" });
            }
            Account account = _accountService.Authenticate(accountParam.Email, accountParam.Password);

            return Ok(account.Token);
        }

        [Authorize(Roles = "member")]
        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody] Account accountParam)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            var result =_accountService.ChangePassword(userId, accountParam.Password);
            if (!result)
            {
                return BadRequest(new { message = "Error" });
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody]Account accountParam)
        {
            var result = _accountService.ResetPassword(accountParam.Email);
            if (!result) {
                return BadRequest(new { message = "Error" });
            }
            return Ok();

        }
    }
}
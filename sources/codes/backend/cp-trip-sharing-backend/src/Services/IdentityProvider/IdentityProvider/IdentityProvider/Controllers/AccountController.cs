using System;
using System.Collections.Generic;
using System.Linq;
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
        [Authorize(Roles ="member")]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_accountService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Account accountParam)
        {
            Account account = _accountService.Authenticate(accountParam.Email, accountParam.Password);

            if (account.Token == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
                
            return Ok(account);
        }
    }
}
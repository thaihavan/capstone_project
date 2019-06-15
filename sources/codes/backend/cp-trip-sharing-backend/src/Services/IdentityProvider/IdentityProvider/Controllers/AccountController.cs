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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Account accountParam)
        {
            Account account = _accountService.Authenticate(accountParam.Email, accountParam.Password);

            if (account == null || account.Token == null)
            {
                return BadRequest(new ErrorMessage { Message = "Email or password is incorrect" });
            }
            return Ok(account);
        }

        // Call user service to create new account
        // pub/sub to email service
        //{
        //  "subject":"test send email",
        //  "To":"linhlpse04693@gmail.com",
        //  "Url":"TripSharing.com/account/verify?token=account.token",
        //  "EmailType":"ConfirmEmail"
        //}
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]Account accountParam)
        {
            var result = _accountService.RegisterAsync(accountParam);
            if (result==null)
            {
                return BadRequest(new ErrorMessage { Message = "Email is in use" });
            }

            Account account = _accountService.Authenticate(accountParam.Email, accountParam.Password);

            return Ok(new { Message = "success"});
            
        }

        [Authorize(Roles = "member")]
        [HttpPost("changepassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel param)
        {
            var accountId = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var result = _accountService.ChangePassword(accountId, param.CurrentPassword, param.NewPassword);
            if (!result)
            {
                return BadRequest(new ErrorMessage { Message = "Error" });
            }
            return Ok(new { Message = "change password succes" });
        }

        // pub/sub to email service
        //{
        //  "subject":"test send email",
        //  "To":"linhlpse04693@gmail.com",
        //  "Url":"TripSharing.com/account/resetpassword?token=account.token",
        //  "EmailType":"ResetPasswordEmail"
        //}
        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody]Account accountParam)
        {
            var result = _accountService.GetResetPasswordTokenAsync(accountParam.Email);           
            return Ok();
        }

        [Authorize(Roles = "unverified")]
        [HttpPost("verify")]
        public IActionResult VerifyEmailAddress()
        {
            var  accountId = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            if(_accountService.VerifyEmail(accountId))return Ok(new { Message = "verified"});
            return BadRequest(new ErrorMessage() { Message = "Error" });
        }

        [Authorize(Roles ="forgotpassword")]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody]ResetPasswordModel param)
        {
            var accountId = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            if(!_accountService.ResetPassword(accountId, param.NewPassword))
            {
                return BadRequest(new ErrorMessage() { Message = "Error" });
            }
            return Ok(new { Message = "Success" });
        }

    }
}
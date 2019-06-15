using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EmailService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using EmailService.Services.Interfaces;

namespace EmailService.Controllers
{
    [Route("api/emailservice/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("sendemail")]
        public IActionResult SendEmail([FromBody]Email param)
        {
            var result = _emailService.SendEmail(param);
            if (result.StatusCode == HttpStatusCode.Accepted)
            {
                return Ok();
            }
            return BadRequest(new { message = "error" });
        }
    }
}

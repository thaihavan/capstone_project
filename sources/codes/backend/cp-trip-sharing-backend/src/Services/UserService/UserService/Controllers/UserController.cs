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
using UserServices.Services;
using UserServices.Services.Interfaces;

namespace UserServices.Controllers
{
    [Route("api/userservice/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService = null;
        private readonly IReportService _reportService = null;

        public UserController(IUserService userService, IReportService reportService)
        {
            _reportService = reportService;
            _userService = userService;
        }

        [Authorize(Roles = "member")]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User userParam)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            userParam.Id = userId;
            userParam.Active = true;
            userParam.ContributionPoint = 0;
            userParam.CreatedDate = DateTime.Now;
            userParam.IsFirstTime = false;

            var result = _userService.Add(userParam);
            return Created("", result);
        }

        [Authorize(Roles = "member")]
        [HttpPost("update")]
        public IActionResult Update([FromBody] User userParam)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if(userParam == null)
            {
                return BadRequest("Parameter can not be null.");
            }

            userParam.Id = userId;

            var result = _userService.Update(userParam);
            return Created("", result);
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] string search, [FromQuery] int page)
        {
            if (page < 1)
            {
                page = 1;
            }
            var result = _userService.GetUsers(search, page);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            var result = _userService.GetUserById(userId);
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpGet("check-username")]
        public IActionResult CheckUsername([FromQuery] string username)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            var result = _userService.CheckUsername(userId, username.Trim());
            if (!result)
            {
                return Ok(new {
                    message = "user name existed"
                });
            }
            return Ok(new
            {
                message = "ok"
            });
        }

        [Authorize(Roles = "admin")]         
        [HttpPost("statistics")]
        public IActionResult GetUserStatistics([FromBody] StatisticsFilter filter)
        {
            var result = _userService.GetUserStatistics(filter);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("ban")]
        public IActionResult BanAnUser([FromQuery] string userId)
        {
            var result = _userService.BanAnUser(userId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("unban")]
        public IActionResult UnBanAnUser([FromQuery] string userId)
        {
            var result = _userService.UnBanAnUser(userId);
            if(!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize(Roles ="member")]
        [HttpPost("report")]
        public IActionResult ReportAnUser([FromBody] Report param)
        {
            if (param == null)
            {
                return BadRequest();
            }
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            param.Id = ObjectId.GenerateNewId().ToString();
            param.Date = DateTime.Now;
            param.ReporterId = userId;
            return Ok(_reportService.Add(param));
        }

        [Authorize(Roles = "admin")]
        [HttpPut("report")]
        public IActionResult UpdateReport([FromBody] Report param)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            if (param == null)
            {
                return BadRequest();
            }
            
            return Ok(_reportService.Update(param));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("report")]
        public IActionResult DeleteAReport([FromQuery] string id)
        {
            return Ok(_reportService.DeleteReport(id));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("reports")]
        public IActionResult GetAllReport([FromQuery] int page)
        {
            return Ok(_reportService.GetAll(page));
        }

        [Authorize(Roles = "member")]
        [HttpGet("report/type")]
        public IActionResult GetAllReportType()
        {
            return Ok(_reportService.GetAllReportType());
        }

        [AllowAnonymous]
        [HttpGet("contribution")]
        public IActionResult GetUserContributionPoint([FromQuery] string userId)
        {
            return Ok(_userService.GetUserContributionPoint(userId));
        }
    }
}

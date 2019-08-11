using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PostService.Models;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService = null;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize(Roles ="member")]
        [HttpPost]
        public IActionResult AddNewReport([FromBody] Report param)
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
        [HttpDelete()]
        public IActionResult DeleteAReport([FromQuery] string id)
        {
            return Ok(_reportService.Delete(id));
        }

        [Authorize(Roles = "member")]
        [HttpGet("type")]
        public IActionResult GetAllReportType()
        {
            return Ok(_reportService.GetAllReportType());
        }

        [Authorize(Roles = "admin")]
        [HttpGet("all")]
        public IActionResult GetAllReport([FromQuery] string targetType)
        {
            return Ok(_reportService.GetAllReport(targetType));
        }

        [Authorize(Roles ="admin")]
        [HttpPut()]
        public IActionResult UpdateReport([FromBody] Report param)
        {
            return Ok(_reportService.Update(param));
        }




    }
}
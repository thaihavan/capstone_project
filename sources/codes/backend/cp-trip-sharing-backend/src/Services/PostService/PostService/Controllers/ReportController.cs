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
    [Route("api/[controller]")]
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

        [Authorize(Roles ="admin")]
        [HttpGet("article")]
        public IActionResult GetAllArticleReports([FromQuery] int page)
        {
            return Ok(_reportService.GetAllArticleReports(page));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("comment")]
        public IActionResult GetAllCommentReports([FromQuery] int page)
        {
            return Ok(_reportService.GetAllCommentReports(page));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("companion-post")]
        public IActionResult GetAllCompanionPostReports([FromQuery] int page)
        {
            return Ok(_reportService.GetAllCompanionPostReports(page));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("virtual-trip")]
        public IActionResult GetAllVirtualTripReports([FromQuery] int page)
        {
            return Ok(_reportService.GetAllVirtualTripReports(page));
        }
    }
}
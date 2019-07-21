using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Services.Interfaces;
using PostService.Services;
using Microsoft.AspNetCore.Authorization;
using PostService.Models;
using MongoDB.Bson;
using System.Security.Claims;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService = null;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _postService.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetById([FromQuery]string postId)
        {
            var result = _postService.GetById(postId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("statistics")]
        public IActionResult GetPostStatistics([FromBody] StatisticsFilter filter)
        {
            if (filter == null)
            {
                filter.From = new DateTime(0);
                filter.To = DateTime.Now;
            }
            var result = _postService.GetAllPostStatistics(filter);
            return Ok(result);
        }
    }
}
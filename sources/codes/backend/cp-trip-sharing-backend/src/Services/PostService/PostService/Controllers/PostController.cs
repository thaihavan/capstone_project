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

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService=null;

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

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] Post postParam) {
            var result = _postService.Add(postParam);
            if (result == null)
            {
                return BadRequest(new ErrorMessage { Message = "Error" });
            }
            return Created("", result);
        }

        [HttpGet]
        public IActionResult GetById([FromQuery]string postId)
        {
            var result = _postService.GetById(postId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
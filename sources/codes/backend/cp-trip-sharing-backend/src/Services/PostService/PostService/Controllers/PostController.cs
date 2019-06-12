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

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService=null;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [Route("All")]
        public IActionResult GetAll()
        {
            var result = _postService.GetAll();
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [Route("Create")]
        public IActionResult Create([FromBody] Post postParam) {
            var result = _postService.Add(postParam);
            if (result == null)
            {
                return BadRequest(new { message = "error" });
            }
            return Created("", result);
        }

        [AllowAnonymous]
        [Route("test")]
        public IActionResult test()
        {
            return Create(new Post()
            { Title = "test1",
                Content = "test1",
                IsActive = true,
                IsPublic=true,
                PubDate=DateTime.Now,
                PostType="test"
            });
        }
    }
}
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

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] Post postParam) {
            var result = _postService.AddPost(postParam);
            if (result == null)
            {
                return BadRequest(new ErrorMessage { Message = "Error" });
            }
            return Created("", result);
        }

        [HttpGet]
        public IActionResult GetById([FromQuery]string postId)
        {
            var result = _postService.GetPostById(postId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Roles = "member")]
        [HttpPost("create")]
        public IActionResult AddVirtualTrip([FromBody]VirtualTrip trip)
        {
            var result = _postService.AddVirtualTrip(trip);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult test()
        {          
            return Create(new Post()
            {   Title = "test1",
                Content = "test1",
                IsActive = true,
                IsPublic=true,
                PubDate=DateTime.Now,
                PostType="test",
                Author=new Author()
                {
                    AuthorId= new BsonObjectId(ObjectId.Parse("5cfa6d85dad2b82ed0f8eb6f")),
                    AuthorImage="some url",
                    AuthorName="linhlp1"
                },              
            });

        }

    }
}
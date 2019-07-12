using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    [Authorize]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService = null;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var topics = _topicService.GetAll();
            if(topics.Count() > 0)
            {
                return Ok(topics);
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult AddTopic([FromBody] Topic param)
        {
            var temp = _topicService.Add(param);
            if (temp != null)
            {
                return Ok(param);
            }
            return NoContent();
        }

        [Authorize(Roles ="admin")]
        [HttpDelete()]
        public IActionResult DeleteTopic([FromQuery] string id)
        {
            return Ok(_topicService.Delete(id));
        }

    }
}
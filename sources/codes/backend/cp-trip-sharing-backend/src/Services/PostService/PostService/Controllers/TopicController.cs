using System;
using System.Collections.Generic;
using System.Linq;
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

        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public IActionResult AddTopic([FromBody] Topic param)
        {
            param.Id = ObjectId.GenerateNewId().ToString();
            param.IsActive = true;
            var temp = _topicService.Add(param);
            if (temp != null)
            {
                return Ok(param);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("insert-or-update")]
        public IActionResult InsertOrUpdate([FromBody] Topic param)
        {
            if (param.Id == null || param.Id == "")
            {
                param.Id = ObjectId.GenerateNewId().ToString();
            }
            param.IsActive = true;
            var temp = _topicService.InsertOrUpdate(param);
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
            var result = _topicService.Delete(id);

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("delete")]
        public IActionResult DeleteTopics([FromBody] List<string> topics)
        {
            var result = _topicService.DeleteMany(topics);

            return Ok(result);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatService.Models;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ChatService.Controllers
{
    [Route("api/chatservice/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService = null;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("messages")]
        public IActionResult GetMessageByConversation([FromQuery] string conversationId)
        {
            var messages = _chatService.GetByConversationId(conversationId);
            if (messages != null)
            {
                return Ok(messages);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("conversations")]
        public IActionResult GetAllConversation([FromQuery] string userId)
        {
            var conversations = _chatService.GetByUserId(userId);
            if (conversations != null)
            {
                return Ok(conversations);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("message")]
        [Authorize(Roles = "member")]
        public IActionResult SendMessage([FromQuery] string receiverId, [FromBody] MessageDetail message)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            message.Id = ObjectId.GenerateNewId().ToString();
            message.SeenId = new List<string>();
            message.Time = DateTime.Now;
            message.FromUserId = userId;
            var result = _chatService.AddMessage(receiverId, message);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
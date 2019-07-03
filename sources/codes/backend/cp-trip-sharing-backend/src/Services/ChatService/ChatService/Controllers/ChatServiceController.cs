using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers
{
    [Route("api/ChatService/[controller]")]
    [ApiController]
    public class ChatServiceController : ControllerBase
    {
        private readonly IChatService _chatService = null;

        public ChatServiceController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("getAllMessage")]
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

        [HttpGet("getAllConversation")]
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
    }
}
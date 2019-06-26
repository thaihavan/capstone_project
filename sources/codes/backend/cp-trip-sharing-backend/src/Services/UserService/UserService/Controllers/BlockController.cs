using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserServices.Controllers
{
    [Route("api/userservice/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockService _blockService = null;

        public BlockController(IBlockService blockService)
        {
            _blockService = blockService;
        }

        // POST: api/UserServices/addblock
        [Authorize(Roles = "member")]
        [HttpPost("addblock")]
        public IActionResult AddBlock([FromQuery] string blocked)
        {
            var block = new Block()
            {
                BlockedId = (blocked)
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = (userId);
            if (_blockService.Block(block) != null)
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE: api/UserServices/unblock
        [Authorize(Roles = "member")]
        [HttpDelete("unblock")]
        public IActionResult UnBlock([FromQuery] string blocked)
        {
            var block = new Block()
            {
                BlockedId = blocked
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            block.BlockerId = userId;
            if (_blockService.UnBlock(block) != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
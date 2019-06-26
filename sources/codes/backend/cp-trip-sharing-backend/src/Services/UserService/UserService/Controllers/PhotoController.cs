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
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService = null;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // GET: api/UserServices/allphoto
        [Authorize(Roles = "member")]
        [HttpGet("allphoto")]
        public IActionResult GetAllPhoto()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            var photos = _photoService.GetAllPhoto(userId);
            return new OkObjectResult(photos);
        }

        // POST: api/UserServices/addphoto
        [Authorize(Roles = "member")]
        [HttpPost("addphoto")]
        public IActionResult AddPhoto([FromQuery] string url, [FromQuery] DateTime date)
        {
            var photo = new Photo()
            {
                Url = url,
                Date = date
            };
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;
            photo.Author = userId;
            if (_photoService.AddPhoto(photo) != null)
            {
                return Ok(photo);
            }
            return BadRequest();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/postservice/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private IUploadFileService _uploadFileService = null;

        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        [HttpPost("uploadimage")]
        public IActionResult UploadImage([FromBody] ImageParam imageParam)
        {
            if(imageParam.Type == null || imageParam.Type.Trim() == "")
            {
                return BadRequest("Image doesn't recognized!");
            }

            string imageUrl = _uploadFileService.UploadImage(imageParam);

            return Ok(new { Image = imageUrl });
        }
    }
}
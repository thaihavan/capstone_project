using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult UploadImage()
        {
            string imageUrl = _uploadFileService.UploadImage();
            return Ok(imageUrl);
        }
    }
}
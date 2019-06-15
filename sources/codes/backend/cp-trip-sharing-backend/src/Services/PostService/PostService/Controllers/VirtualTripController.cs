using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Services.Interfaces;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualTripController : ControllerBase
    {
        private IVirtualTripService _virtualTripService = null;

        public VirtualTripController(IVirtualTripService virtualTripService)
        {
            _virtualTripService = virtualTripService;
        }

        [HttpGet("all")]
        public IActionResult GetAllTripWithPost()
        {
            var trips = _virtualTripService.GetAllTripWithPost();
            return Ok(trips);
        }


    }
}
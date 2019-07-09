using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;
using NotificationService.Services.Interfaces;

namespace NotificationService.Controllers
{
    [Route("api/notificationservice/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        public readonly INotificationService _notificationService = null;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public IActionResult GetNotifications()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            var result = _notificationService.GetNotifications(userId);

            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddNotification([FromBody] Notification notification)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst("user_id").Value;

            notification.Seen = new List<string>();
            notification.Date = DateTime.Now;

            var result = _notificationService.Add(notification);
            return Ok(result);
        }
    }
}
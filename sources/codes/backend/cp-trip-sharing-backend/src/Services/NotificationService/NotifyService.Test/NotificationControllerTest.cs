using Microsoft.AspNetCore.Http;
using Moq;
using NotificationService.Controllers;
using NotificationService.Models;
using NotificationService.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace NotifyService.Test
{
    [TestFixture]
    class NotificationControllerTest
    {
        Mock<INotificationService> mockNotificationService;
        ClaimsIdentity claims = null;
        Notification notification,notificationSecond = null;
        List<Notification> notifications = new List<Notification>();

        [SetUp]
        public void Config()
        {
            List<string> list_receivers = new List<string>();
            list_receivers.Add("afaf7fa6fas6fas5f5af");
            list_receivers.Add("as7fa7f6afaf5a4sf4asf");
            List<string> list_seenIds = new List<string>();
            list_seenIds.Add("afaf7fa6fas6fas5f5af");
            list_seenIds.Add("as7fa7f6afaf5a4sf4asf");

            mockNotificationService = new Mock<INotificationService>();
            claims = new ClaimsIdentity(new Claim[]
             {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","as7fa7f6afaf5a4sf4asf")
             });

            notification = new Notification()
            {
                Id = "afaf7af6af6afa5faf",
                Content = "have notification",
                Date = DateTime.Now,
                Url = "",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };

            notificationSecond = new Notification()
            {
                Id = "as7fa7f6afaf5a4sf4asf",
                Content = "have notification",
                Date = DateTime.Now,
                Url = "",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };

            notifications.Add(notification);
            notifications.Add(notificationSecond);
        }

        [TestCase]
        public void TestGetNotifications()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            IEnumerable<Notification> _ienumerable = notifications;
            mockNotificationService.Setup(x => x.GetNotifications(It.IsAny<string>())).Returns(_ienumerable);
            var _notificationController = new NotificationController(mockNotificationService.Object);
            _notificationController.ControllerContext.HttpContext = contextMock.Object;
            var accountReturn = _notificationController.GetNotifications();
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddNotification()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockNotificationService.Setup(x => x.Add(It.IsAny<Notification>())).Returns(notification);
            var _notificationController = new NotificationController(mockNotificationService.Object);
            _notificationController.ControllerContext.HttpContext = contextMock.Object;
            var accountReturn = _notificationController.AddNotification(notification);
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

using Moq;
using NotificationService.Models;
using NotificationService.Repositories;
using NotificationService.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyService.Test
{
    [TestFixture]
    class NotificationServiceTest
    {
        Mock<INotificationRepository> mockNotificationRepository;
        Notification notificationObject = null;

        [SetUp]
        public void Config()
        {
            List<string> list_receivers = new List<string>();
            list_receivers.Add("afaf7fa6fas6fas5f5af");
            list_receivers.Add("as7fa7f6afaf5a4sf4asf");
            List<string> list_seenIds = new List<string>();
            list_seenIds.Add("afaf7fa6fas6fas5f5af");
            list_seenIds.Add("as7fa7f6afaf5a4sf4asf");

            notificationObject = new Notification()
            {
                Id = "afaf7af6af6afa5faf",
                Content = "have notification",
                Date = DateTime.Now,
                Url = "",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };

            mockNotificationRepository = new Mock<INotificationRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockNotificationRepository.Setup(x => x.Add(It.IsAny<Notification>())).Returns(notificationObject);
            var notificationService = new NotificationService.Services.NotificationService(mockNotificationRepository.Object);
            Notification notification = notificationService.Add(notificationObject);
            Assert.IsNotNull(notification);
        }

        [TestCase]
        public void TestGetNotifications()
        {
            IEnumerable<Notification> ienumable = null;
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<string>())).Returns(ienumable);
            var notificationService = new NotificationService.Services.NotificationService(mockNotificationRepository.Object);
            IEnumerable<Notification> notification = notificationService.GetNotifications("asfasfas9af7a7f7asf");
            Assert.IsNull(notification);
        }
    }
}

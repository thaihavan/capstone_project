using Moq;
using NotificationService.Models;
using NotificationService.Repositories;
using NotificationService.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotifyService.Test
{
    [TestFixture]
    class NotificationServiceTest
    {
        Mock<INotificationRepository> mockNotificationRepository;
        Notification notificationObject, notificationSecond = null;
        List<Notification> notifications = new List<Notification>();

        [SetUp]
        public void Config()
        {
            List<string> list_receivers = new List<string>();
            list_receivers.Add("5d4d2143523376b00013a8986");
            list_receivers.Add("5d4d012613376b00013a8911");
            List<string> list_seenIds = new List<string>();
            list_seenIds.Add("5d4d012613376b00013a8912");
            list_seenIds.Add("5d4d012613376b00013a89as");

            notificationObject = new Notification()
            {
                Id = "5d4d01261337124g23528986",
                Content = "have notification",
                Date = DateTime.Now,
                Url = "",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };

            notificationSecond = new Notification()
            {
                Id = "5d4d012613376b00013a898s",
                Content = "have notification",
                Date = DateTime.Now,
                Url = "",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };

            notifications.Add(notificationObject);
            notifications.Add(notificationSecond);

            mockNotificationRepository = new Mock<INotificationRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockNotificationRepository.Setup(x => x.Add(It.IsAny<Notification>())).Returns(notificationObject);
            var notificationService = new NotificationService.Services.NotificationService(mockNotificationRepository.Object);
            Notification notificationActual = notificationService.Add(notificationObject);
            Assert.AreEqual(notificationActual,notificationObject);
        }

        [TestCase]
        public void TestGetNotifications()
        {
            IEnumerable<Notification> ienumable = notifications;
            mockNotificationRepository.Setup(x => x.GetNotifications(It.IsAny<string>())).Returns(ienumable);
            var notificationService = new NotificationService.Services.NotificationService(mockNotificationRepository.Object);
            IEnumerable<Notification> iEnumerablenotification = notificationService.GetNotifications("5d4d012613376b00013a8986");
            Notification notificationActual = iEnumerablenotification.FirstOrDefault();
            Assert.AreEqual(notificationActual.Id, "5d4d01261337124g23528986");
        }
    }
}

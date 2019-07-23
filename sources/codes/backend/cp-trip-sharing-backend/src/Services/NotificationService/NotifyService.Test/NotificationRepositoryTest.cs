using Microsoft.Extensions.Options;
using NotificationService.Helpers;
using NotificationService.Models;
using NotificationService.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyService.Test
{
    [TestFixture]
    class NotificationRepositoryTest
    {
        NotificationRepository _notificationRepository;
        Notification notificationObject = null;

        [SetUp]
        public void Config()
        {
            List<string> list_receivers = new List<string>();
            list_receivers.Add("5d2843264168004938e6ff9a");
            list_receivers.Add("5d2843264168004938e6ff9b");
            List<string> list_seenIds = new List<string>();
            list_seenIds.Add("5d2843264168004938e6ff9c");
            list_seenIds.Add("5d2843264168004938e6ff9d");

            notificationObject = new Notification()
            {
                Id = "5d2843264168004938e6ff90",
                Content = "<b>Thái Thái</b> đã thêm một bình luận",
                Date = DateTime.Now,
                Url = "http://trip-sharing.net/bai-viet/5d25d4a91b385b00015977a1",
                DisplayImage = "",
                Receivers = list_receivers,
                SeenIds = list_seenIds
            };
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-Identity"
            };
            _notificationRepository = new NotificationRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAdd()
        {
            Notification notification = _notificationRepository.Add(notificationObject);
            Assert.IsNotNull(notification);
        }

        [TestCase]
        public void TestAddToSeenIds()
        {
            bool checkAddToSeenIds = _notificationRepository.AddToSeenIds("5d2843264168004938e6ff90", "5d2843264168004938e6ff91");
            Assert.IsTrue(checkAddToSeenIds);
        }

        [TestCase]
        public void TestGetAllReceivers()
        {
            IEnumerable<User> ienumableUser = _notificationRepository.GetAllReceivers("5d2843264168004938e6ff90");
            Assert.IsEmpty(ienumableUser);
        }

        [TestCase]
        public void TestGetNotifications()
        {
            IEnumerable<Notification> ienumableUser = _notificationRepository.GetNotifications("5d2843264168004938e6ff91");
            Assert.IsEmpty(ienumableUser);
        }
    }
}

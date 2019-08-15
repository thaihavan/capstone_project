//using ChatService.Helpers;
//using ChatService.Models;
//using ChatService.Repositories;
//using Microsoft.Extensions.Options;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ChatService.Test
//{
//    [TestFixture]
//    class MessageRepositoryTest
//    {
//        MessageRepository messageRepository;
//        AppSettings _settings = null;
//        MessageDetail messageDetail = null;

//        [SetUp]
//        public void Config()
//        {
//            messageDetail = new MessageDetail()
//            {
//                Id = "5d2991b86fde0d04fc9aa69c",
//                ConversationId = "5d0a17701a0a4200017de69c",
//                FromUserId = "5d28411283b135478015c69c",
//                Time = DateTime.Now,
//                Content = "Hello",
//            };

//            _settings = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-PostService"
//            };
//            messageRepository = new MessageRepository(Options.Create(_settings));
//        }

//        [TestCase]
//        public void TestAdd()
//        {
//            MessageDetail messageDetailRereturn = messageRepository.Add(messageDetail);
//            Assert.IsNotNull(messageDetailRereturn);
//        }
//    }
//}

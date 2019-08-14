//using Microsoft.Extensions.Options;
//using NUnit.Framework;
//using PostService.Helpers;
//using PostService.Models;
//using PostService.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PostService.Test
//{
//    [TestFixture]
//    class TopicRepositoryTest
//    {
//        TopicRepository _topicRepository = null;
//        AppSettings _settings = null;
//        Topic topic = null;

//        [SetUp]
//        public void Config()
//        {
//            topic = new Topic()
//            {
//                Id = "ads6safasf5f5asf4asf4",
//                Name = "Thien nhien",
//                ImgUrl = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907201619337977-3i9uvrioa3noa7c3.jpg"
//            };

//            _settings = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-PostService"
//            };
//            _topicRepository = new TopicRepository(Options.Create(_settings));
//        }

//        [TestCase]
//        public void TestAdd()
//        {
//            Topic topicReturn = _topicRepository.Add(topic);
//            Assert.IsNotNull(topicReturn);
//        }

//        [TestCase]
//        public void TestDelete()
//        {
//            bool isDelete = _topicRepository.Delete("asdf6afaf5f6afsfas8f8a6");
//            Assert.IsTrue(isDelete);
//        }

//        [TestCase]
//        public void TestDeleteMany()
//        {
//            List<string> listTopicId = new List<string>();
//            listTopicId.Add("ads6safasf5f5asf4asf4");
//            bool isDelete = _topicRepository.DeleteMany(listTopicId);
//            Assert.IsTrue(isDelete);
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            IEnumerable<Topic> topics = null;
//            topics = _topicRepository.GetAll();
//            Assert.IsNotNull(topics);
//        }


//    }
//}

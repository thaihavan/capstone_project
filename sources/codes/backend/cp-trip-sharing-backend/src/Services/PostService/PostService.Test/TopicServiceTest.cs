using Moq;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class TopicServiceTest
    {
        Topic topic = null;
        Mock<ITopicRepository> mockTopicRepository;

        [SetUp]
        public void Config()
        {
            topic = new Topic()
            {
                Id = "ads6safasf5f5asf4asf4",
                Name = "Thien nhien",
                ImgUrl = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907201619337977-3i9uvrioa3noa7c3.jpg"
            };

            mockTopicRepository = new Mock<ITopicRepository>();                
        }

        [TestCase]
        public void TestAdd()
        {
            mockTopicRepository.Setup(x => x.Add(It.IsAny<Topic>())).Returns(topic);
            var topicService = new TopicService(mockTopicRepository.Object);
            Topic topicReturn = topicService.Add(topic);
            Assert.IsNotNull(topicReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            mockTopicRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var topicService = new TopicService(mockTopicRepository.Object);
            bool isDeleted = topicService.Delete("aasf65asfa44fafsf5");
            Assert.IsTrue(isDeleted);
        }

        [TestCase]
        public void TestDeleteMany()
        {
            List<string> listTopicId = new List<string>();
            listTopicId.Add("da8f7af6sffa5af67asf7");
            listTopicId.Add("da8f7af6sffa5af67asf3");
            mockTopicRepository.Setup(x => x.DeleteMany(listTopicId)).Returns(true);
            var topicService = new TopicService(mockTopicRepository.Object);
            bool isDeleted = topicService.DeleteMany(listTopicId);
            Assert.IsTrue(isDeleted);
        }

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<Topic> topics = new List<Topic>()
            {
                new Topic()
                {
                    Id = "ads6safasf5f5asf4asf5",
                    Name = "Thien nhien",
                    ImgUrl = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907201619337977-3i9uvrioa3noa7c3.jpg"
                }
        };
            mockTopicRepository.Setup(x => x.GetAll()).Returns(topics);
            var topicService = new TopicService(mockTopicRepository.Object);
            IEnumerable<Topic> list_topics = topicService.GetAll();
            Assert.IsNotNull(list_topics);
        }
    }
}

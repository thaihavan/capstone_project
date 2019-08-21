using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class TopicControllerTest
    {
        Mock<ITopicService> mockTopicService;
        Topic topic = null;

        [SetUp]
        public void Config()
        {
            topic = new Topic()
            {
                Id = "ads6safasf5f5asf4asf4",
                Name = "Thien nhien",
                ImgUrl = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907201619337977-3i9uvrioa3noa7c3.jpg"
            };

            mockTopicService = new Mock<ITopicService>();
        }

        [TestCase]
        public void TestGetAll()
        {
            List<Topic> listTopic = new List<Topic>();
            listTopic.Add(topic);
            IEnumerable<Topic> iEnumerableTopic = listTopic;
            mockTopicService.Setup(x => x.GetAll()).Returns(iEnumerableTopic);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult getAllTopic = topicController.GetAll();
            var type = getAllTopic.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllReturnNoContent()
        {
            List<Topic> listTopic = new List<Topic>();
            IEnumerable<Topic> iEnumerableTopic = listTopic;
            mockTopicService.Setup(x => x.GetAll()).Returns(iEnumerableTopic);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult getAllTopic = topicController.GetAll();
            var type = getAllTopic.GetType();
            Assert.AreEqual(type.Name, "NoContentResult");
        }

        [TestCase]
        public void TestAddTopic()
        {
            mockTopicService.Setup(x => x.Add(It.IsAny<Topic>())).Returns(topic);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult getAllTopic = topicController.AddTopic(topic);
            var type = getAllTopic.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddTopicReturnNoContent()
        {
            Topic topicNull = null;
            mockTopicService.Setup(x => x.Add(It.IsAny<Topic>())).Returns(topicNull);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult getAllTopic = topicController.AddTopic(topic);
            var type = getAllTopic.GetType();
            Assert.AreEqual(type.Name, "NoContentResult");
        }

        [TestCase]
        public void TestInsertOrUpdate()
        {
            mockTopicService.Setup(x => x.InsertOrUpdate(It.IsAny<Topic>())).Returns(topic);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult isDelete = topicController.InsertOrUpdate(topic);
            var type = isDelete.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestInsertOrUpdateReturnNoContent()
        {
            topic.Id = null;
            Topic topicNull = null;
            mockTopicService.Setup(x => x.InsertOrUpdate(It.IsAny<Topic>())).Returns(topicNull);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult isDelete = topicController.InsertOrUpdate(topic);
            var type = isDelete.GetType();
            Assert.AreEqual(type.Name, "NoContentResult");
        }

        [TestCase]
        public void TestDeleteTopic()
        {
            mockTopicService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult isDelete = topicController.DeleteTopic("asf5asfasfasfa3asdsd4");
            var type = isDelete.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestDeleteTopics()
        {
            List<string> listTopicId = new List<string>();
            listTopicId.Add("da8f7af6sffa5af67asf7");
            listTopicId.Add("da8f7af6sffa5af67asf3");
            mockTopicService.Setup(x => x.DeleteMany(listTopicId)).Returns(true);
            var topicController = new TopicController(mockTopicService.Object);
            IActionResult isDelete = topicController.DeleteTopics(listTopicId);
            var type = isDelete.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

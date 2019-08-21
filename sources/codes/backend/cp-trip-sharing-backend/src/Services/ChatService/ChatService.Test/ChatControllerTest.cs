using ChatService.Models;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChatService.Test
{
    [TestFixture]
    class ChatControllerTest
    {
        Mock<IChatService> mockChatService;
        Conversation conversation, conversationSecond = null;
        MessageDetail messageDetail, messageDetailSecond = null;
        User user, userSecond = null;
        List<User> listUsers = new List<User>();
        List<MessageDetail> listMessageDetails = new List<MessageDetail>();
        List<Conversation> listConversations = new List<Conversation>();
        ClaimsIdentity claims = null;

        [SetUp]
        public void Config()
        {        
            user = new User()
            {
                Id = "5d4d012613376b00013a8908",
                DisplayName = "PhongTV",
                ProfileImage = ""
            };

            userSecond = new User()
            {
                Id = "5d4d012613376b00013a8",
                DisplayName = "MinhNH",
                ProfileImage = ""
            };
            List<string> listReceivers = new List<string>();
            listReceivers.Add("5d4d012613376b00013a8986");
            listReceivers.Add("5d4d012613376b00013a898x");

            List<string> listSeenIds = new List<string>();
            listSeenIds.Add("5d4d012613376b00013a8986");
            listSeenIds.Add("5d4d012613376b00013a898x");


            listUsers.Add(user);
            listUsers.Add(userSecond);

            messageDetail = new MessageDetail()
            {
                Id = "5d4d012613376b00013a892x",
                Content = "Message Content",
                ConversationId = "534d012613376b00013a898x",
                FromUserId = "5d4d0x2613376b00013a898x",
                Time = DateTime.Now
            };

            messageDetailSecond = new MessageDetail()
            {
                Id = "5d4d012613376b00013a892z",
                Content = "Message Content Second",
                ConversationId = "534d012613376b00013a898z",
                FromUserId = "5d4d0x2613376b00013a898z",
                Time = DateTime.Now
            };

            listMessageDetails.Add(messageDetail);
            listMessageDetails.Add(messageDetailSecond);

            conversation = new Conversation()
            {
                Id = "5d4d0x2613376b00013a8909",
                Avatar = "",
                CreatedDate = DateTime.Now,
                GroupAdmin = "admin",
                LastMessage = messageDetail,
                Name = "Conversation",
                Type = "conversation",
                Messages = listMessageDetails,
                Receivers = listReceivers,
                SeenIds = listSeenIds,
                Users = listUsers
            };

            conversationSecond = new Conversation()
            {
                Id = "5d4d0x2613376b00013a8911",
                Avatar = "",
                CreatedDate = DateTime.Now,
                GroupAdmin = "admin",
                LastMessage = messageDetail,
                Name = "Conversation",
                Type = "conversation",
                Messages = listMessageDetails,
                Receivers = listReceivers,
                SeenIds = listSeenIds,
                Users = listUsers
            };

            listConversations.Add(conversation);
            listConversations.Add(conversationSecond);

            claims = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
              });

            mockChatService = new Mock<IChatService>();
        }

        [TestCase]
        public void TestGetMessageByConversationReturnBadRequest()
        {
            IEnumerable<MessageDetail> ienumableMessageDetail = null;
            mockChatService.Setup(x => x.GetByConversationId(It.IsAny<string>())).Returns(ienumableMessageDetail);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getMessageByConversation = chatController.GetMessageByConversation("aasfafas7afaf6a6fs");
            var type = getMessageByConversation.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetMessageByConversation()
        {
            IEnumerable<MessageDetail> _iEnumableMessageDetail = listMessageDetails;
            mockChatService.Setup(x => x.GetByConversationId(It.IsAny<string>())).Returns(_iEnumableMessageDetail);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getMessageByConversation = chatController.GetMessageByConversation("aasfafas7afaf6a6fs");
            var type = getMessageByConversation.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllConversations()
        {
            IEnumerable<Conversation> _iEnumerableConversation = listConversations;
            mockChatService.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(_iEnumerableConversation);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getAllConversations = chatController.GetAllConversations("aasfafas7afaf6a6fs");
            var type = getAllConversations.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllConversationsReturnBadRequest()
        {
            IEnumerable<Conversation> iEnumerableConversation = null;
            mockChatService.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(iEnumerableConversation);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getAllConversations = chatController.GetAllConversations("aasfafas7afaf6a6fs");
            var type = getAllConversations.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestSendMessage()
        {
            mockChatService.Setup(x => x.AddMessage(It.IsAny<string>(), It.IsAny<MessageDetail>())).Returns(messageDetail);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            var contextMock = new Mock<HttpContext>();   
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            chatController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult sendMessage = chatController.SendMessage("asf6asf6asf5asf5asf5", messageDetail);
            var type = sendMessage.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestSendMessageReturnBadRequest()
        {
            MessageDetail message = null;
            mockChatService.Setup(x => x.AddMessage(It.IsAny<string>(), It.IsAny<MessageDetail>())).Returns(message);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            chatController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult sendMessage = chatController.SendMessage("asf6asf6asf5asf5asf5", messageDetail);
            var type = sendMessage.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestCreateGroupChat()
        {
            mockChatService.Setup(x => x.CreateGroupChat(It.IsAny<Conversation>())).Returns(conversation);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            chatController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult createGroupChat = chatController.CreateGroupChat(conversation);
            var type = createGroupChat.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddUserToGroupChat()
        {
            mockChatService.Setup(x => x.AddUserToGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult addUserToGroupChat = chatController.AddUserToGroupChat("af5af5asf5af5a5f","asf6af7g7g7fg8f8gfgsd");
            var type = addUserToGroupChat.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddUserToGroupChatReturnBadRequest()
        {
            User userNull = null;
            mockChatService.Setup(x => x.AddUserToGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(userNull);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult addUserToGroupChat = chatController.AddUserToGroupChat("af5af5asf5af5a5f", "asf6af7g7g7fg8f8gfgsd");
            var type = addUserToGroupChat.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestRemoveUserToGroupChat()
        {
            mockChatService.Setup(x => x.RemoveUserFromGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult removeUserFromGroupChat = chatController.RemoveUserToGroupChat("af5af5asf5af5a5f", "asf6af7g7g7fg8f8gfgsd");
            var type = removeUserFromGroupChat.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestRemoveUserToGroupChatReturnBadRequest()
        {
            mockChatService.Setup(x => x.RemoveUserFromGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult removeUserFromGroupChat = chatController.RemoveUserToGroupChat("af5af5asf5af5a5f", "asf6af7g7g7fg8f8gfgsd");
            var type = removeUserFromGroupChat.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestAddToSeenIds()
        {
            mockChatService.Setup(x => x.AddToSeenIds(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            chatController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addToSeenIds = chatController.AddToSeenIds("adasfa8fa7fasf6a");
            var type = addToSeenIds.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddToSeenIdsReturnBadRequest()
        {
            mockChatService.Setup(x => x.AddToSeenIds(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            chatController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addToSeenIds = chatController.AddToSeenIds("adasfa8fa7fasf6a");
            var type = addToSeenIds.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetMembers()
        {
            IEnumerable<User> _iEnumerableUser = listUsers;
            mockChatService.Setup(x => x.GetAllMember(It.IsAny<string>())).Returns(_iEnumerableUser);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getMembers = chatController.GetMembers("af5af5asf6af7g7g7fg8f8gfgsd");
            var type = getMembers.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }


        [TestCase]
        public void TestGetMembersReturnBadRequest()
        {
            IEnumerable<User> ienumableUser = null;
            mockChatService.Setup(x => x.GetAllMember(It.IsAny<string>())).Returns(ienumableUser);
            var chatController = new ChatService.Controllers.ChatController(mockChatService.Object);
            IActionResult getMembers = chatController.GetMembers("af5af5asf6af7g7g7fg8f8gfgsd");
            var type = getMembers.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }
    }
}

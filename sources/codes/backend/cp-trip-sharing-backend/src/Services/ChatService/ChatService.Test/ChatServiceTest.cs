using ChatService.Models;
using ChatService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatService.Test
{
    [TestFixture]
    class ChatServiceTest
    {
        Mock<IMessageRepository> mockMessageRepository;
        Mock<IConversationRepository> mockConversationRepository;
        Conversation conversation, conversationSecond = null;
        MessageDetail messageDetail, messageDetailSecond = null;
        User user, userSecond = null;
        List<User> listUsers = new List<User>();
        List<MessageDetail> listMessageDetails = new List<MessageDetail>();
        List<Conversation> listConversations = new List<Conversation>();

        [SetUp]
        public void Config()
        {
            mockConversationRepository = new Mock<IConversationRepository>();
            mockMessageRepository = new Mock<IMessageRepository>();

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
        }        

        [TestCase]
        public void TestAddMessage()
        {
            mockConversationRepository.Setup(x => x.FindPrivateConversationByReceiverId(It.IsAny<string>())).Returns(conversation);
            mockConversationRepository.Setup(x => x.Update(It.IsAny<Conversation>())).Returns(conversation);
            mockMessageRepository.Setup(x => x.Add(It.IsAny<MessageDetail>())).Returns(messageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            MessageDetail messageDetailActual = chatService.AddMessage("5d4d0x2613376b00013a898z", messageDetail);
            Assert.AreEqual(messageDetailActual, messageDetail);
        }

        [TestCase]
        public void TestAddMessageIfReturnNull()
        {
            Conversation conversationNull = null;
            mockConversationRepository.Setup(x => x.FindPrivateConversationByReceiverId(It.IsAny<string>())).Returns(conversationNull);
            mockConversationRepository.Setup(x => x.Add(It.IsAny<Conversation>())).Returns(conversation);
            mockMessageRepository.Setup(x => x.Add(It.IsAny<MessageDetail>())).Returns(messageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            MessageDetail messageDetailActual = chatService.AddMessage("5d4d0x2613376b00013a898z", messageDetail);
            Assert.AreEqual(messageDetailActual, messageDetail);
        }

        [TestCase]
        public void TestAddToSeenIds()
        {
            mockConversationRepository.Setup(x => x.AddToSeenIds(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            bool checkAddToSeenIds = chatService.AddToSeenIds("asf6af5asf4s4af3afa5f", "asf6af5asf4s4af3afa5f");
            Assert.IsTrue(checkAddToSeenIds);
        }

        [TestCase]
        public void TestAddUserToGroupChat()
        {
            mockConversationRepository.Setup(x => x.AddUserToGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            User userActual = chatService.AddUserToGroupChat("asf6af5asf4s4af3afa5f", "asf6af5asf4s4af3afa5f");
            Assert.AreEqual(userActual,user);
        }

        [TestCase]
        public void TestCreateGroupChat()
        {
            mockConversationRepository.Setup(x => x.Add(It.IsAny<Conversation>())).Returns(conversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            Conversation conversationActual = chatService.CreateGroupChat(conversation);
            Assert.AreEqual(conversationActual,conversation);
        }

        [TestCase]
        public void TestGetAllMember()
        {
            IEnumerable<User> _iEnumerableUser = listUsers;
            mockConversationRepository.Setup(x => x.GetAllUserInConversation(It.IsAny<string>())).Returns(_iEnumerableUser);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<User> iEnumerableGetAllMemberActual = chatService.GetAllMember("asfas9fdsa8fa7f6sf6as");
            User userActual = iEnumerableGetAllMemberActual.FirstOrDefault();
            Assert.AreEqual(userActual.Id, "5d4d012613376b00013a8908");
        }

        [TestCase]
        public void TestGetByConversationId()
        {
            IEnumerable<MessageDetail> _iEnumableMessageDetail = listMessageDetails;
            mockConversationRepository.Setup(x => x.GetMessageByConversationId(It.IsAny<string>())).Returns(_iEnumableMessageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<MessageDetail> _iEnumerableGetByConversationId = chatService.GetByConversationId("asfas9fdsa8fa7f6sf6as");
            MessageDetail messageDetailActual = _iEnumerableGetByConversationId.FirstOrDefault();
            Assert.AreEqual(messageDetailActual.Id, "5d4d012613376b00013a892x");
        }

        [TestCase]
        public void TestGetById()
        {
            mockConversationRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(conversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            Conversation conversationActual = chatService.GetById("asfas9fdsa8fa7f6sf6as");
            Assert.AreEqual(conversationActual,conversation);
        }

        [TestCase]
        public void TestGetByUserId()
        {
            IEnumerable<Conversation> _iEnumerableConversation = listConversations;         
            mockConversationRepository.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(_iEnumerableConversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<Conversation> ienumableGetByUserId = chatService.GetByUserId("asfas9fdsa8fa7f6sf6as");
            Conversation conversationActual = ienumableGetByUserId.FirstOrDefault();
            Assert.AreEqual(conversationActual.Id, "5d4d0x2613376b00013a8909");
        }

        [TestCase]
        public void TestRemoveUserFromGroupChat()
        {
            mockConversationRepository.Setup(x => x.RemoveUserFromGroupChat(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            bool checkRemove = chatService.RemoveUserFromGroupChat("asfas9fdsa8fa7f6sf6as", "asfas9fdsa8fa7f6sf6ass");
            Assert.IsTrue(checkRemove);
        }
    }
}

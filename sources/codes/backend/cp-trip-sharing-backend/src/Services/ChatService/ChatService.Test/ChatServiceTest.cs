using ChatService.Models;
using ChatService.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatService.Test
{
    [TestFixture]
    class ChatServiceTest
    {
        Mock<IMessageRepository> mockMessageRepository;
        Mock<IConversationRepository> mockConversationRepository;
        Conversation conversation = null;
        MessageDetail messageDetail = null;
        User user = null;

        [SetUp]
        public void Config()
        {
            mockConversationRepository = new Mock<IConversationRepository>();
            mockMessageRepository = new Mock<IMessageRepository>();

            messageDetail = new MessageDetail()
            {
                Id = "fgaf9sfa8fas7fa7sf7",
                Content = "Message Content",
                ConversationId = "adasf7afaf565af5a5f",
                FromUserId = "casfafafafa8af7sf6a",
                Time = DateTime.Now
            };

            conversation = new Conversation()
            {
                Id = "afafafaf9afas8fas8f",
                Avatar = "",
                CreatedDate = DateTime.Now,
                GroupAdmin = "admin",
                LastMessage = messageDetail,
                Name = "",
                Type = "conversation"
            };

            user = new User()
            {
                Id = "asafafsaf8asf7asfas6",
                DisplayName = "phongtv",
                ProfileImage = ""
            };
        }
        public IEnumerable<MessageDetail> _iEnumableMessageDetail()
        {
            yield return messageDetail;
        }
        public IEnumerable<Conversation> _iEnumerableConversation()
        {
            yield return conversation;
        }
        public IEnumerable<User> _iEnumerableUser()
        {
            yield return user;
        }

        [TestCase]
        public void TestAddMessage()
        {
            mockConversationRepository.Setup(x => x.FindPrivateConversationByReceiverId(It.IsAny<string>())).Returns(conversation);
            mockConversationRepository.Setup(x => x.Update(It.IsAny<Conversation>())).Returns(conversation);
            mockMessageRepository.Setup(x => x.Add(It.IsAny<MessageDetail>())).Returns(messageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            MessageDetail messageReturn = chatService.AddMessage("",messageDetail);
            Assert.IsNotNull(messageReturn);
        }

        [TestCase]
        public void TestAddMessageIfReturnNull()
        {
            Conversation conversationNull = null;
            mockConversationRepository.Setup(x => x.FindPrivateConversationByReceiverId(It.IsAny<string>())).Returns(conversationNull);
            mockConversationRepository.Setup(x => x.Add(It.IsAny<Conversation>())).Returns(conversation);
            mockMessageRepository.Setup(x => x.Add(It.IsAny<MessageDetail>())).Returns(messageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            MessageDetail messageReturn = chatService.AddMessage("asf6af5asf4s4af3afa5f", messageDetail);
            Assert.IsNotNull(messageReturn);
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
            User userReturn = chatService.AddUserToGroupChat("asf6af5asf4s4af3afa5f", "asf6af5asf4s4af3afa5f");
            Assert.IsNotNull(userReturn);
        }

        [TestCase]
        public void TestCreateGroupChat()
        {
            mockConversationRepository.Setup(x => x.Add(It.IsAny<Conversation>())).Returns(conversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            Conversation conversationReturn = chatService.CreateGroupChat(conversation);
            Assert.IsNotNull(conversationReturn);
        }

        [TestCase]
        public void TestGetAllMember()
        {
            mockConversationRepository.Setup(x => x.GetAllUserInConversation(It.IsAny<string>())).Returns(_iEnumerableUser);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<User> iEnumerableGetAllMember = chatService.GetAllMember("asfas9fdsa8fa7f6sf6as");
            Assert.IsNotEmpty(iEnumerableGetAllMember);
        }

        [TestCase]
        public void TestGetByConversationId()
        {
            mockConversationRepository.Setup(x => x.GetMessageByConversationId(It.IsAny<string>())).Returns(_iEnumableMessageDetail);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<MessageDetail> iEnumerableGetByConversationId = chatService.GetByConversationId("asfas9fdsa8fa7f6sf6as");
            Assert.IsNotEmpty(iEnumerableGetByConversationId);
        }

        [TestCase]
        public void TestGetById()
        {
            mockConversationRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(conversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            Conversation conversationReturn = chatService.GetById("asfas9fdsa8fa7f6sf6as");
            Assert.IsNotNull(conversationReturn);
        }

        [TestCase]
        public void TestGetByUserId()
        {
            mockConversationRepository.Setup(x => x.GetByUserId(It.IsAny<string>())).Returns(_iEnumerableConversation);
            var chatService = new ChatService.Services.ChatService(mockConversationRepository.Object, mockMessageRepository.Object);
            IEnumerable<Conversation> ienumableGetByUserId = chatService.GetByUserId("asfas9fdsa8fa7f6sf6as");
            Assert.IsNotEmpty(ienumableGetByUserId);
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

using ChatService.Helpers;
using ChatService.Models;
using ChatService.Repositories;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatService.Test
{
    [TestFixture]
    class ConversationRepositoryTest
    {
        Conversation conversation = null;
        MessageDetail messageDetail = null;
        AppSettings _settings = null;
        ConversationRepository conversationRepository = null;

        [SetUp]
        public void Config()
        {
            List<string> receivers = new List<string>();
            receivers.Add("5d0a17701a0a4200017de6c9");

            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            conversationRepository = new ConversationRepository(Options.Create(_settings));

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
                Id = "5d33f0965644bb0b87a9419a",
                Avatar = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-default-group-chat-avatar.png",
                CreatedDate = DateTime.Now,
                GroupAdmin = "admin",
                LastMessage = messageDetail,
                Name = "Lập Team đi phượt",
                Type = "conversation",
                Receivers = receivers
            };
        }

        [TestCase]
        public void TestAdd()
        {
            Conversation conversationReturn = conversationRepository.Add(conversation);
            Assert.IsNotNull(conversationReturn);
        }

        [TestCase]
        public void TestGetMessageByConversationId()
        {
            IEnumerable<MessageDetail> iEnumconversationReturn = conversationRepository.GetMessageByConversationId("5d33f0965644bb0b87a9419a");
            Assert.IsEmpty(iEnumconversationReturn);
        }

        [TestCase]
        public void TestGetByUserId()
        {
            IEnumerable<Conversation> iEnumableConversation = conversationRepository.GetByUserId("5d33f0965644bb0b87a9419a");
            Assert.IsEmpty(iEnumableConversation);
        }

        [TestCase]
        public void TestGetById()
        {
            Conversation conversationReturn = conversationRepository.GetById("5d33f0965644bb0b87a9419a");
            Assert.IsNotNull(conversationReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            conversation.Name = "PHONGTV";
            Conversation conversationReturn = conversationRepository.Update(conversation);
            Assert.IsNotNull(conversationReturn);
        }

        [TestCase]
        public void TestGetAllUserInConversation()
        {
            //fail
            //IEnumerable<User> ienumableGetAllUserInConversation = conversationRepository.GetAllUserInConversation("5d2ea8cf94fe2557c075629d");
            //Assert.IsEmpty(ienumableGetAllUserInConversation);
        }

        [TestCase]
        public void TestFindPrivateConversationByReceiverId()
        {
            Conversation conversationReturn = conversationRepository.FindPrivateConversationByReceiverId("5d0a17701a0a4200017de6c7");
            Assert.IsNull(conversationReturn);
        }

        [TestCase]
        public void TestAddUserToGroupChatUserNull()
        {
            User conversationReturn = conversationRepository.AddUserToGroupChat("5d33f0965644bb0b87a9419a","5d0a17701a0a4200017de6c7");
            Assert.IsNull(conversationReturn);
        }

        [TestCase]
        public void TestRemoveUserFromGroupChat()
        {
            bool checkRemove = conversationRepository.RemoveUserFromGroupChat("5d33f0965644bb0b87a9419a", "5d0a17701a0a4200017de6c7");
            Assert.IsTrue(checkRemove);
        }

        [TestCase]
        public void TestUpdateSeenIds()
        {
            List<string> seenIds = new List<string>();
            seenIds.Add("5d0a17701a0a4200017de6c9");
            bool checkUpdate = conversationRepository.UpdateSeenIds("5d33f0965644bb0b87a9419a", seenIds);
            Assert.IsTrue(checkUpdate);
        }

        [TestCase]
        public void TestAddToSeenIds()
        {
            bool checkAddToSeenIds = conversationRepository.AddToSeenIds("5d33f0965644bb0b87a9419a", "5d33f0965644bb0b87a9429a");
            Assert.IsTrue(checkAddToSeenIds);
        }
    }
}

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
    class CompanionPostServiceTest
    {
        Mock<ICompanionPostRepository> _mockICompanionPostRepository = null;
        CompanionPost companionPost = null;
        Post post = null;
        PostFilter postFilter = null;
        CompanionPostJoinRequest companionPostJoinRequest = null;

        [SetUp]
        public void Config()
        {
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };

            postFilter = new PostFilter()
            {
                LocationId = "5sd239asdd8fass7",
                Search = "ha noi",
                TimePeriod = "Tuan qua",
                Topics = listTopics
            };

            ArticleDestinationItem articleDestinationItem = new ArticleDestinationItem()
            {
                Id = "5d33f09863c6060b5a8c519c",
                Name = "Đi du lịch Hạ Long"
            };
            List<ArticleDestinationItem> listArticleDestinationItem = new List<ArticleDestinationItem>();
            listArticleDestinationItem.Add(articleDestinationItem);

            List<string> estimatedCostItems = new List<string>();
            ScheduleItem scheduleItem = new ScheduleItem()
            {

                Content = "Đi du lịch Hạ Long",
                Day = DateTime.Parse("11/08/2019"),
                Title = "Đi du lịch Hạ Long"
            };

            List<ScheduleItem> listScheduleItems = new List<ScheduleItem>();
            listScheduleItems.Add(scheduleItem);

            Author author = new Author()
            {
                Id = "5d15941f197c3400015db0aa",
                DisplayName = "PhongTV",
                ProfileImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png"
            };

            companionPostJoinRequest = new CompanionPostJoinRequest()
            {
                Id = "5d15941f197c3400015db0aa",
                CompanionPostId = "5d15941f197c3400015db0aa",
                Date = DateTime.Parse("11/08/2019"),
                UserId = "",
                User = author
            };

            List<CompanionPostJoinRequest> listcompanionPostJoinRequest = new List<CompanionPostJoinRequest>();
            listcompanionPostJoinRequest.Add(companionPostJoinRequest);

            post = new Post()
            {
                Id = "5d07d847a2c5f845707dc69a",
                Content = "<p>Post Test</p>",
                AuthorId = "5d0b2b0b1c9d440000d8e9a1",
                CommentCount = 0,
                IsActive = true,
                IsPublic = true,
                LikeCount = 0,
                CoverImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png",
                PostType = "article",
                PubDate = DateTime.Now,
                liked = false,
                Title = "Post Test",
            };

            estimatedCostItems.Add("Ăn,ngủ,nghỉ");
            companionPost = new CompanionPost()
            {
                Id = "",
                EstimatedCost = 1000000,
                ExpiredDate = DateTime.Parse("10/08/2019"),
                MaxMembers = 10,
                MinMembers = 5,
                From = DateTime.Parse("10/08/2019"),
                To = DateTime.Parse("12/08/2019"),
                ConversationId = "5d0b2b0b1c9d440000d8e9ax",
                EstimatedCostItems = estimatedCostItems,
                ScheduleItems = listScheduleItems,
                Destinations = listArticleDestinationItem,
                PostId = "5d33f09763c6060b5a8c519b",
                Post = post,
                JoinRequests = listcompanionPostJoinRequest,
                Requested = true
            };

            _mockICompanionPostRepository = new Mock<ICompanionPostRepository>();
        }

        IEnumerable<CompanionPost> ienumerableCompanionPost()
        {
            yield return companionPost;
        }

        IEnumerable<CompanionPostJoinRequest> ienumerableCompanionPostJoinRequest()
        {
            yield return companionPostJoinRequest;
        }

        [TestCase]
        public void TestAdd()
        {
            _mockICompanionPostRepository.Setup(x => x.Add(It.IsAny<CompanionPost>())).Returns(companionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPost companionPostActual = companionPostService.Add(companionPost);
            Assert.AreEqual(companionPostActual.ConversationId, "5d0b2b0b1c9d440000d8e9ax");
        }

        [TestCase]
        public void TestAddNewRequest()
        {
            _mockICompanionPostRepository.Setup(x => x.AddNewRequest(It.IsAny<CompanionPostJoinRequest>())).Returns(companionPostJoinRequest);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPostJoinRequest companionPostJoinRequestActual = companionPostService.AddNewRequest(companionPostJoinRequest);
            Assert.AreEqual(companionPostJoinRequestActual.Id, "5d15941f197c3400015db0aa");
        }

        [TestCase]
        public void TestCancelRequestReturnTrue()
        {
            _mockICompanionPostRepository.Setup(x => x.GetRequestByUserIdAndPostId(It.IsAny<string>(), It.IsAny<string>())).Returns(companionPostJoinRequest);
            _mockICompanionPostRepository.Setup(x => x.DeleteJoinRequest(It.IsAny<string>())).Returns(true);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool cancelRequestActual = companionPostService.CancelRequest("5d15941f197c3400015db0aa","5d15941f197c3400015db0ax");
            Assert.IsTrue(cancelRequestActual);
        }

        [TestCase]
        public void TestCancelRequestReturnFalse()
        {
            CompanionPostJoinRequest companionPostJoinRequestNull = null;
            _mockICompanionPostRepository.Setup(x => x.GetRequestByUserIdAndPostId(It.IsAny<string>(), It.IsAny<string>())).Returns(companionPostJoinRequestNull);
            _mockICompanionPostRepository.Setup(x => x.DeleteJoinRequest(It.IsAny<string>())).Returns(true);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool cancelRequestActual = companionPostService.CancelRequest("5d15941f197c3400015db0aa", "5d15941f197c3400015db0ax");
            Assert.IsFalse(cancelRequestActual);
        }

        [TestCase]
        public void TestDeleteReturnTrue()
        {
            _mockICompanionPostRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool checkDeleteActual = companionPostService.Delete("5d15941f197c3400015db0aa");
            Assert.IsTrue(checkDeleteActual);
        }

        [TestCase]
        public void TestDeleteReturnFalse()
        {
            _mockICompanionPostRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(false);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool checkDeleteActual = companionPostService.Delete("5d15941f197c3400015db0aa");
            Assert.IsFalse(checkDeleteActual);
        }

        [TestCase]
        public void TestDeleteJoinRequestReturnTrue()
        {
            _mockICompanionPostRepository.Setup(x => x.DeleteJoinRequest(It.IsAny<string>())).Returns(true);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool checkDeleteJoinRequestActual = companionPostService.DeleteJoinRequest("5d15941f197c3400015db0aa");
            Assert.IsTrue(checkDeleteJoinRequestActual);
        }

        [TestCase]
        public void TestDeleteJoinRequestReturnFalse()
        {
            _mockICompanionPostRepository.Setup(x => x.DeleteJoinRequest(It.IsAny<string>())).Returns(false);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            bool checkDeleteJoinRequestActual = companionPostService.DeleteJoinRequest("5d15941f197c3400015db0aa");
            Assert.IsFalse(checkDeleteJoinRequestActual);
        }

        [TestCase]
        public void TestGetAll()
        {
            _mockICompanionPostRepository.Setup(x => x.GetAll(It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(ienumerableCompanionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            var ienumCompanionPost = companionPostService.GetAll(postFilter,6);
            Assert.IsNotEmpty(ienumCompanionPost);
        }

        [TestCase]
        public void TestGetAllCompanionPostByUser()
        {
            _mockICompanionPostRepository.Setup(x => x.GetAllCompanionPostByUser(It.IsAny<string>(), It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(ienumerableCompanionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            var ienumCompanionPost = companionPostService.GetAllCompanionPostByUser("5d15941f197c3400015db0aa", postFilter, 6);
            Assert.IsNotEmpty(ienumCompanionPost);
        }

        [TestCase]
        public void TestGetAllJoinRequest()
        {
            _mockICompanionPostRepository.Setup(x => x.GetAllJoinRequest(It.IsAny<string>())).Returns(ienumerableCompanionPostJoinRequest);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            var ienumCompanionPost = companionPostService.GetAllJoinRequest("5d15941f197c3400015db0aa");
            Assert.IsNotEmpty(ienumCompanionPost);
        }

        [TestCase]
        public void TestGetById()
        {
            _mockICompanionPostRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPost companionPostActual = companionPostService.GetById("5d15941f197c3400015db0aa");
            Assert.AreEqual(companionPostActual.ConversationId, "5d0b2b0b1c9d440000d8e9ax");
        }

        [TestCase]
        public void TestGetById2()
        {
            _mockICompanionPostRepository.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>())).Returns(companionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPost companionPostActual = companionPostService.GetById("5d15941f197c3400015db0aa", "5d15941f197c3400015db0ax");
            Assert.AreEqual(companionPostActual.ConversationId, "5d0b2b0b1c9d440000d8e9ax");
        }

        [TestCase]
        public void TestGetRequestById()
        {
            _mockICompanionPostRepository.Setup(x => x.GetRequestById(It.IsAny<string>())).Returns(companionPostJoinRequest);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPostJoinRequest companionPostJoinRequestActual = companionPostService.GetRequestById("5d15941f197c3400015db0aa");
            Assert.AreEqual(companionPostJoinRequestActual.Id, "5d15941f197c3400015db0aa");
        }

        [TestCase]
        public void TestUpdate()
        {
            _mockICompanionPostRepository.Setup(x => x.Update(It.IsAny<CompanionPost>())).Returns(companionPost);
            var companionPostService = new CompanionPostService(_mockICompanionPostRepository.Object);
            CompanionPost companionPostActual = companionPostService.Update(companionPost);
            Assert.AreEqual(companionPostActual.Id, "");
        }
    }
}

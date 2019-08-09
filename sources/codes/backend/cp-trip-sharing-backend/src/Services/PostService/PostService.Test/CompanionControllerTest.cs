using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class CompanionControllerTest
    {
        public Mock<ICompanionPostService> _mockCompanionPostService = null;
        public Mock<IPostService> _mockPostService = null;
        ClaimsIdentity claims = null;
        CompanionPost companionPost = null;
        Post post = null;
        PostFilter postFilter = null;
        CompanionPostJoinRequest companionPostJoinRequest = null;

        [SetUp]
        public void Config()
        {
            _mockCompanionPostService = new Mock<ICompanionPostService>();
            _mockPostService = new Mock<IPostService>();

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
                ConversationId = "",
                EstimatedCostItems = estimatedCostItems,
                ScheduleItems = listScheduleItems,
                Destinations = listArticleDestinationItem,
                PostId = "5d33f09763c6060b5a8c519b",
                Post = post,
                JoinRequests = listcompanionPostJoinRequest,
                Requested = true
            };
            claims = new ClaimsIdentity(new Claim[]
           {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
           });
        }

        IEnumerable<CompanionPost> ienumerableCompanionPost()
        {
            yield return companionPost;
        }

        [TestCase]
        public void TestCreateAsync()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            //mockLikeService.Setup(x => x.Add(It.IsAny<Like>())).Returns(like);
            //var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            //_likeController.ControllerContext.HttpContext = contextMock.Object;
        }

        [TestCase]
        public void TestUpdate()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockPostService.Setup(x => x.Add(It.IsAny<Post>())).Returns(post);
            _mockCompanionPostService.Setup(x => x.Add(It.IsAny<CompanionPost>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.Update(companionPost);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetCompanionPostById()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>(), It.IsAny<string>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.GetCompanionPostById("5d0b2b0b1c9d440000d8e9a1");
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllCompanionPost()
        {
            _mockCompanionPostService.Setup(x => x.GetAll(It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(ienumerableCompanionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            var resultActual = _companionController.GetAllCompanionPost(postFilter, 6);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestDeleteCompanionPostReturnUnauthorizedResult()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.DeleteCompanionPost("5d0b2b0b1c9d440000d8e9a1");
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestDeleteCompanionPostReturnOkObjectResult()
        {

        }

        [TestCase]
        public void TestJoinCompanion()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.AddNewRequest(It.IsAny<CompanionPostJoinRequest>())).Returns(companionPostJoinRequest);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.JoinCompanion(companionPostJoinRequest);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllRequestReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.GetAllRequest("5d0b2b0b1c9d440000d8e9a1");
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestGetAllRequestReturnOkObjectResult()
        {
             var new_claims = new ClaimsIdentity(new Claim[]
             {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d0b2b0b1c9d440000d8e9a1")
             });
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new_claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.GetAllRequest("5d0b2b0b1c9d440000d8e9a1");
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        } 

        [TestCase]
        public void TestDeleteRequestReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.DeleteRequest(companionPostJoinRequest);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestDeleteRequestReturnOkobjectResult()
        {
            var new_claims = new ClaimsIdentity(new Claim[]
           {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d0b2b0b1c9d440000d8e9a1")
           });
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(new_claims));
            _mockCompanionPostService.Setup(x => x.GetById(It.IsAny<string>())).Returns(companionPost);
            _mockCompanionPostService.Setup(x => x.DeleteJoinRequest(It.IsAny<string>())).Returns(true);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.DeleteRequest(companionPostJoinRequest);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestCancelRequest()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.CancelRequest(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.CancelRequest("5d0b2b0b1c9d440000d8e9a1");
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllCompanionPostByUser()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockCompanionPostService.Setup(x => x.GetAllCompanionPostByUser(It.IsAny<string>(), It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(ienumerableCompanionPost);
            var _companionController = new CompanionController(_mockCompanionPostService.Object, _mockPostService.Object);
            _companionController.ControllerContext.HttpContext = contextMock.Object;
            var resultActual = _companionController.GetAllCompanionPostByUser(postFilter, "5d0b2b0b1c9d440000d8e9a1", 6);
            var type = resultActual.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

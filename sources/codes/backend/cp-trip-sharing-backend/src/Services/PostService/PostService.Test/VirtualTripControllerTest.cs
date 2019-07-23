using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    class VirtualTripControllerTest
    {
        Mock<IVirtualTripService> mockVirtualTripService;
        Mock<IPostService> mockPostService;
        PostFilter postFilter = null;
        VirtualTrip virtualTrip = null;
        Post post = null;

        [SetUp]
        public void Config()
        {
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
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
            postFilter = new PostFilter()
            {
                LocationId = "5sd239asdd8fass7",
                Search = "ha noi",
                TimePeriod = "Tuan qua",
                Topics = listTopics
            };
            virtualTrip = new VirtualTrip()
            {
                Id= "a6sfa7fafaf65fa58fa7f",
                PostId = "asf7f6af9sfafaf7af0aaf",
                Post = post 
            };

            mockVirtualTripService = new Mock<IVirtualTripService>();
            mockPostService = new Mock<IPostService>();
        }

        [TestCase]
        public void TestGetAllVirtualTrips()
        {
            IEnumerable<VirtualTrip> virtualTrips = new List<VirtualTrip>()
            {
                virtualTrip
            };

            mockVirtualTripService.Setup(x => x.GetAllVirtualTrips(It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(virtualTrips);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object,mockPostService.Object);
            IActionResult getAllVirtualTrips = virtualTripControler.GetAllVirtualTrips(postFilter, 6);
            var type = getAllVirtualTrips.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllVirtualTripsByUser()
        {
            IEnumerable<VirtualTrip> virtualTrips = new List<VirtualTrip>()
            {
                virtualTrip
            };

            mockVirtualTripService.Setup(x => x.GetAllVirtualTripsByUser(It.IsAny<string>(), It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(virtualTrips);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            IActionResult getAllVirtualTrips = virtualTripControler.GetAllVirtualTrips("assfa8a6fasfa5sffa6sf",postFilter, 6);
            var type = getAllVirtualTrips.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetVirtualTrip()
        {
            mockVirtualTripService.Setup(x => x.GetVirtualTrip(It.IsAny<string>())).Returns(virtualTrip);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            IActionResult getAllVirtualTrips = virtualTripControler.GetVirtualTrip("assfa8a6fasfa5sffa6sf");
            var type = getAllVirtualTrips.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestCreateVirtualTrip()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Add(It.IsAny<Post>())).Returns(post);
            mockVirtualTripService.Setup(x => x.Add(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            virtualTripControler.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addedVirtualTrip = virtualTripControler.CreateVirtualTrip(virtualTrip);
            var type = addedVirtualTrip.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUpdateVirtualTripReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            mockVirtualTripService.Setup(x => x.Update(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            virtualTripControler.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addedVirtualTrip = virtualTripControler.UpdateVirtualTrip(virtualTrip);
            var type = addedVirtualTrip.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestUpdateVirtualTripReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d0b2b0b1c9d440000d8e9a1")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            mockVirtualTripService.Setup(x => x.Update(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            virtualTripControler.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addedVirtualTrip = virtualTripControler.UpdateVirtualTrip(virtualTrip);
            var type = addedVirtualTrip.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestUpdateVirtualTripReturnSuccess()
        {
            virtualTrip.Post.Id = "asf7f6af9sfafaf7af0aaf";
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d0b2b0b1c9d440000d8e9a1")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPostService.Setup(x => x.Update(It.IsAny<Post>())).Returns(post);
            mockVirtualTripService.Setup(x => x.Update(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripControler = new VirtualTripController(mockVirtualTripService.Object, mockPostService.Object);
            virtualTripControler.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addedVirtualTrip = virtualTripControler.UpdateVirtualTrip(virtualTrip);
            var type = addedVirtualTrip.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

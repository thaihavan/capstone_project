using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UserServices.Controllers;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserService.Test
{
    [TestFixture]
    class FollowControllerTest
    {
        Follow follow = null;
        Mock<IFollowService> mockfollowService;
        ClaimsIdentity claims;
        User user, userSecond = null;
        List<string> getAllFollowerIds = new List<string>();
        List<string> getAllFollowingIds = new List<string>();

        [SetUp]
        public void Config()
        {
            follow = new Follow()
            {
                Id = "5d2e0e4cbb113a00010368c3",
                Follower = "5d0a17701a0a4200017de6c7",
                Following = "5d111299f3b75e0001f4ed78"
            };

            claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d2e0e4cbb113a0001036a12")
            });

            user = new User()
            {
                Id = "5d027ea59b358d247cd21aa3",
                AccountId = "5d027ea59b358d247cd12re78",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "",
                ContributionPoint = 0,
                CreatedDate = DateTime.Now,
                DisplayName = "PhongTv",
                Dob = DateTime.Parse("02/01/1997"),
                FirstName = "Tran",
                FollowerCount = 0,
                FollowingCount = 34,
                Gender = true,
                Interested = null,
                IsFirstTime = false,
                LastName = "phong",
                UserName = "phongtv"
            };

            userSecond = new User()
            {
                Id = "5d027ea59b358d212o3iu456b",
                AccountId = "5d027ea59b358d247cd12re12",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "",
                ContributionPoint = 0,
                CreatedDate = DateTime.Now,
                DisplayName = "PhongTv",
                Dob = DateTime.Parse("02/01/1997"),
                FirstName = "Tran",
                FollowerCount = 0,
                FollowingCount = 34,
                Gender = true,
                Interested = null,
                IsFirstTime = false,
                LastName = "phong",
                UserName = "phongtv"
            };

            getAllFollowerIds.Add("5d111299f3b75e0001f4ed78");
            getAllFollowingIds.Add("5d111299f3b75e0001f4ed78");
            mockfollowService = new Mock<IFollowService>();
        }

        [TestCase]
        public void TestFollow()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.AddFollows(It.IsAny<Follow>())).Returns(follow);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addFollow = followController.Follow("5d0a17701a0a4200017de6c7");
            var type = addFollow.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestFollowReturnBadRequest()
        {
            Follow followNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.AddFollows(It.IsAny<Follow>())).Returns(followNull);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addFollow = followController.Follow("5d0a17701a0a4200017de6c7");
            var type = addFollow.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestUnfollow()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.Unfollow(It.IsAny<Follow>())).Returns(follow);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult unFollow = followController.Unfollow("5d0a17701a0a4200017de6c7");
            var type = unFollow.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUnfollowReturnBadRequest()
        {
            Follow followNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.Unfollow(It.IsAny<Follow>())).Returns(followNull);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult unFollow = followController.Unfollow("5d0a17701a0a4200017de6c7");
            var type = unFollow.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestGetCurrentUserFollowedReturnNoContent()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.IsFollowed(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getCurrentUserFollowed = followController.GetCurrentUserFollowed("5d0a17701a0a4200017de6c7");
            var type = getCurrentUserFollowed.GetType();
            Assert.AreEqual(type.Name, "NoContentResult");
        }

        [TestCase]
        public void TestGetCurrentUserFollowed()
        {
            //fail
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockfollowService.Setup(x => x.IsFollowed(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var followController = new FollowController(mockfollowService.Object);
            followController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getCurrentUserFollowed = followController.GetCurrentUserFollowed("5d0a17701a0a4200017de6c7");
            var type = getCurrentUserFollowed.GetType();
            Assert.AreEqual(type.Name, "NoContentResult");
        }

        [TestCase]
        public void TestGetAllFollower()
        {
            IEnumerable<User> ienumableUser = new List<User>() { user, userSecond };
            mockfollowService.Setup(x => x.GetAllFollower(It.IsAny<string>())).Returns(ienumableUser);
            var followController = new FollowController(mockfollowService.Object);
            IActionResult getAllFollower = followController.GetAllFollower("5d0a17701a0a4200017de6c7");
            var type = getAllFollower.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllFollowing()
        {
            IEnumerable<User> ienumableUser = new List<User>() { user, userSecond };
            mockfollowService.Setup(x => x.GetAllFollower(It.IsAny<string>())).Returns(ienumableUser);
            var followController = new FollowController(mockfollowService.Object);
            IActionResult getAllFollowing = followController.GetAllFollowing("5d0a17701a0a4200017de6c7");
            var type = getAllFollowing.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllFollowerId()
        {
            mockfollowService.Setup(x => x.GetAllFollowerId(It.IsAny<string>())).Returns(getAllFollowerIds);
            var followController = new FollowController(mockfollowService.Object);
            IActionResult getAllFollowerId = followController.GetAllFollowerId("5d0a17701a0a4200017de6c7");
            var type = getAllFollowerId.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllFollowingId()
        {
            mockfollowService.Setup(x => x.GetAllFollowingId(It.IsAny<string>())).Returns(getAllFollowingIds);
            var followController = new FollowController(mockfollowService.Object);
            IActionResult getAllFollowingId = followController.GetAllFollowingId("5d0a17701a0a4200017de6c7");
            var type = getAllFollowingId.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

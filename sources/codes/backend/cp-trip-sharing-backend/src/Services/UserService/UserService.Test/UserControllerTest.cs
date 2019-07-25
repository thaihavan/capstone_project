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
    class UserControllerTest
    {
        Mock<IUserService> mockUserService;
        User user = null;
        ClaimsIdentity claims = null;
        StatisticsFilter statisticsFilter = null;

        [SetUp]        
        public void Config()
        {
            user = new User()
            {
                Id = "5d300f07a346270001a5bef4",
                AccountId = "5d300f07a346270001a5bef2",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-default-user-avatar.png",
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

            claims = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","afa5fafaf4aga4g")
               });

            statisticsFilter = new StatisticsFilter()
            {
                From = DateTime.Parse("01/01/2019"),
                To = DateTime.Now
            };

            mockUserService = new Mock<IUserService>();
        }

        IEnumerable<User> ienumerableUser()
        {
            yield return user;
        }

        [TestCase]
        public void TestRegister()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockUserService.Setup(x => x.Add(It.IsAny<User>())).Returns(user);
            var userController = new UserController(mockUserService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult register = userController.Register(user);
            var type = register.GetType();
            Assert.AreEqual(type.Name, "CreatedResult");
        }

        [TestCase]
        public void TestUpdate()
        {
            user.UserName = "PHONGTV UPDATE";
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockUserService.Setup(x => x.Update(It.IsAny<User>())).Returns(user);
            var userController = new UserController(mockUserService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult update = userController.Update(user);
            var type = update.GetType();
            Assert.AreEqual(type.Name, "CreatedResult");
        }

        [TestCase]
        public void TestUpdateReturnBadRequest()
        {
            User userNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockUserService.Setup(x => x.Update(It.IsAny<User>())).Returns(userNull);
            var userController = new UserController(mockUserService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult update = userController.Update(userNull);
            var type = update.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestGetAll()
        {
            mockUserService.Setup(x => x.GetUsers(It.IsAny<string>())).Returns(ienumerableUser);
            var userController = new UserController(mockUserService.Object);
            IActionResult getAllUser = userController.GetAll("as8fa7fa6fas5f5asf5");
            var type = getAllUser.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetUserById()
        {
            mockUserService.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(user);
            var userController = new UserController(mockUserService.Object);
            IActionResult getUserById = userController.GetUserById("as8fa7fa6fas5f5asf5");
            var type = getUserById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetUserStatistics()
        {
            mockUserService.Setup(x => x.GetUserStatistics(It.IsAny<StatisticsFilter>())).Returns(statisticsFilter);
            var userController = new UserController(mockUserService.Object);
            IActionResult getUserStatistics = userController.GetUserStatistics(statisticsFilter);
            var type = getUserStatistics.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

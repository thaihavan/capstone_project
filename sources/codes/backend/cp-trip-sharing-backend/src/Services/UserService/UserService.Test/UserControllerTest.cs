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
        Mock<IReportService> mockReportService;
        User user = null;
        ClaimsIdentity claims = null;
        StatisticsFilter statisticsFilter = null;
        Report report = null;
        ReportType reportType = null;

        [SetUp]        
        public void Config()
        {
            reportType = new ReportType()
            {
                Id = "5d027ea59b358d247cd219az",
                Name = "comment"
            };

            report = new Report()
            {
                Id = "5d247a04eff1030d7c5209a0",
                ReportTypeId = "5d247a04eff1030d7c5209a3",
                TargetId = "5d247a04eff1030d7c52034e",
                Content = "vi pham",
                Date = DateTime.Now,
                IsResolved = false,
                ReporterId = "5d247a04eff1030d7c5209a0",
                ReportType = reportType,
                Target = null
            };

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

            mockReportService = new Mock<IReportService>();
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
            var userController = new UserController(mockUserService.Object,mockReportService.Object);
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
            var userController = new UserController(mockUserService.Object,mockReportService.Object);
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
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult update = userController.Update(userNull);
            var type = update.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestGetAll()
        {
            mockUserService.Setup(x => x.GetUsers(It.IsAny<string>(), It.IsAny<int>())).Returns(ienumerableUser);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult getAllUser = userController.GetAll("as8fa7fa6fas5f5asf5",6);
            var type = getAllUser.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetUserById()
        {
            mockUserService.Setup(x => x.GetUserById(It.IsAny<string>())).Returns(user);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult getUserById = userController.GetUserById("as8fa7fa6fas5f5asf5");
            var type = getUserById.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestCheckUsername()
        {
            mockUserService.Setup(x => x.CheckUsername(It.IsAny<string>())).Returns(true);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult checkUserName = userController.CheckUsername("phongtv");
            var type = checkUserName.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestCheckUsernameReturnBadRequest()
        {
            mockUserService.Setup(x => x.CheckUsername(It.IsAny<string>())).Returns(false);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult checkUserName = userController.CheckUsername("phongtv");
            var type = checkUserName.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestGetUserStatistics()
        {
            mockUserService.Setup(x => x.GetUserStatistics(It.IsAny<StatisticsFilter>())).Returns(statisticsFilter);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult getUserStatistics = userController.GetUserStatistics(statisticsFilter);
            var type = getUserStatistics.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestBanAnUser()
        {
            mockUserService.Setup(x => x.BanAnUser(It.IsAny<string>())).Returns(true);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult banUser = userController.BanAnUser("5d300f07a346270001a5bef2");
            var type = banUser.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestBanAnUserReturnBadRequest()
        {
            mockUserService.Setup(x => x.BanAnUser(It.IsAny<string>())).Returns(false);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult banUser = userController.BanAnUser("5d300f07a346270001a5bef2");
            var type = banUser.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestUnBanAnUserReturnBadRequest()
        {
            mockUserService.Setup(x => x.UnBanAnUser(It.IsAny<string>())).Returns(false);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult unbanUser = userController.UnBanAnUser("5d300f07a346270001a5bef2");
            var type = unbanUser.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestUnBanAnUser()
        {
            mockUserService.Setup(x => x.UnBanAnUser(It.IsAny<string>())).Returns(true);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult unbanUser = userController.UnBanAnUser("5d300f07a346270001a5bef2");
            var type = unbanUser.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestReportAnUser()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockReportService.Setup(x => x.Add(It.IsAny<Report>())).Returns(report);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult reportUser = userController.ReportAnUser(report);
            var type = reportUser.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }


        [TestCase]
        public void TestReportAnUserReturnBadRequest()
        {
            Report reportNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockReportService.Setup(x => x.Add(It.IsAny<Report>())).Returns(report);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult reportUser = userController.ReportAnUser(reportNull);
            var type = reportUser.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestUpdateReport()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockReportService.Setup(x => x.Update(It.IsAny<Report>())).Returns(report);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult updateReport = userController.UpdateReport(report);
            var type = updateReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUpdateReportReturnBadRequest()
        {
            Report reportNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            userController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult updateReport = userController.UpdateReport(reportNull);
            var type = updateReport.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestDeleteAReport()
        {           
            mockReportService.Setup(x => x.DeleteReport(It.IsAny<string>())).Returns(report);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult deleteAReport = userController.DeleteAReport("5d300f07a346270001a5bef2");
            var type = deleteAReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllReport()
        {
            List<Report> listReport = new List<Report>();
            listReport.Add(report);
            IEnumerable<Report> iemumerableReport = listReport;           
            mockReportService.Setup(x => x.GetAll(It.IsAny<int>())).Returns(iemumerableReport);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult getAllReport = userController.GetAllReport(6);
            var type = getAllReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllReportType()
        {
            List<ReportType> listReportType = new List<ReportType>();
            listReportType.Add(reportType);
            IEnumerable<ReportType> iemumerableReportType = listReportType;
            List<Report> listReport = new List<Report>();
            listReport.Add(report);
            IEnumerable<Report> iemumerableReport = listReport;
            mockReportService.Setup(x => x.GetAllReportType()).Returns(iemumerableReportType);
            var userController = new UserController(mockUserService.Object, mockReportService.Object);
            IActionResult getAllReportType = userController.GetAllReportType();
            var type = getAllReportType.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

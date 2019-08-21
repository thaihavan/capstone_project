using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class ReportControllerTest
    {
        Mock<IReportService> _mockReportService;
        Report report = null;
        Article article = null;
        Comment cmt = null;
        ReportType reportType = null;
        CompanionPost companionPost = null;
        CompanionPostJoinRequest companionPostJoinRequest = null;
        VirtualTrip virtualTrip = null;
        ClaimsIdentity claims = null;

        [SetUp]
        public void Config()
        {
            claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
            });

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
                TargetType = "TargetType",
                IsResolved = false,
                ReporterId = "5d247a04eff1030d7c5209a0",
                ReportType = reportType,
                Target = null
            };

            _mockReportService = new Mock<IReportService>();
        }
      
        [TestCase]
        public void TestAddNewReportReturnBadRequest()
        {
            Report report = null;
            var reportController = new ReportController(_mockReportService.Object);
            var checkAddNewReport = reportController.AddNewReport(report);
            var type = checkAddNewReport.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }

        [TestCase]
        public void TestAddNewReportReturnOkObjectResult()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            _mockReportService.Setup(x => x.Add(It.IsAny<Report>())).Returns(report);
            var reportController = new ReportController(_mockReportService.Object);
            reportController.ControllerContext.HttpContext = contextMock.Object;
            var checkAddNewReport = reportController.AddNewReport(report);
            var type = checkAddNewReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestDeleteAReport()
        {
            _mockReportService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var reportController = new ReportController(_mockReportService.Object);
            var checkDeleteReport = reportController.DeleteAReport("5d247a04eff1030d7c5209a0");
            var type = checkDeleteReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllReportType()
        {
            IEnumerable<ReportType> ienumerableReportType = new List<ReportType>
            {
                 reportType
            };
            _mockReportService.Setup(x => x.GetAllReportType()).Returns(ienumerableReportType);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllReportType = reportController.GetAllReportType();
            var type = getAllReportType.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllReport()
        {
            IEnumerable<Report> ienumerableReport = new List<Report>
            {
               report
            };
            _mockReportService.Setup(x => x.GetAllReport(It.IsAny<string>())).Returns(ienumerableReport);
            var reportController = new ReportController(_mockReportService.Object);
            IActionResult getAllReport = reportController.GetAllReport("comment");
            var type = getAllReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUpdateReport()
        {
            report.Content = "update content report";
            _mockReportService.Setup(x => x.Update(It.IsAny<Report>())).Returns(report);
            var reportController = new ReportController(_mockReportService.Object);
            IActionResult getAllReport = reportController.UpdateReport(report);
            var type = getAllReport.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

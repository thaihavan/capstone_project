using Microsoft.AspNetCore.Http;
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
            ArticleDestinationItem articleDestinationItem = new ArticleDestinationItem()
            {
                Id = "5d33f09863c6060b5a8c519c",
                Name = "Đi du lịch Hạ Long"
            };
            List<ArticleDestinationItem> listArticleDestinationItem = new List<ArticleDestinationItem>();
            listArticleDestinationItem.Add(articleDestinationItem);
            ScheduleItem scheduleItem = new ScheduleItem()
            {

                Content = "Đi du lịch Hạ Long",
                Day = DateTime.Parse("11/08/2019"),
                Title = "Đi du lịch Hạ Long"
            };
            Author author = new Author()
            {
                Id = "5d247a04eff1030d7c5209a1",
                DisplayName = "authorName",
                ProfileImage = "profileImage"
            };


            List<ScheduleItem> listScheduleItems = new List<ScheduleItem>();
            listScheduleItems.Add(scheduleItem);
            List<string> estimatedCostItems = new List<string>();
            estimatedCostItems.Add("Ăn,ngủ,nghỉ");
            Post post = new Post()
            {
                Id = "5d247a04eff1030d7c5209a1",
                AuthorId = "authorId",
                CommentCount = 0,
                Content = "content",
                IsActive = true,
                IsPublic = true,
                CoverImage = "coverImage",
                LikeCount = 0,
                PostType = "article",
                Title = "title",
                liked = false,
                PubDate = DateTime.Parse("2019-04-05"),
                Author = author
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


            companionPost = new CompanionPost()
            {
                Id = "5d15941f197c3400015db0aa",
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

            reportType = new ReportType()
            {
                Id = "5d027ea59b358d247cd219az",
                Name = "comment"
            };

            cmt = new Comment()
            {
                Id = "5d027ea59b358d247cd219a0",
                AuthorId = "5d15941f197c3400015db0aa",
                PostId = "5d027ea59b358d247cd219a2",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                Active = true,
                Liked = false,
                LikeCount = 0
            };
            List<ArticleDestinationItem> listArticleDestination = new List<ArticleDestinationItem>();
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
            listArticleDestination.Add(new ArticleDestinationItem()
            {
                Id = "articleDestinationItemId",
                Name = "articleDestinationItemName"
            });       
          

            article = new Article()
            {
                Id = "5d247a04eff1030d7c5209a0",
                PostId = "5d247a04eff1030d7c5209a1",
                Destinations = listArticleDestination,
                Post = post,
                Topics = listTopics
            };

            virtualTrip = new VirtualTrip()
            {
                Id = "a6sfa7fafaf65fa58fa7f",
                PostId = "asf7f6af9sfafaf7af0aaf",
                Post = post
            };

            report = new Report()
            {
                Id = "5d247a04eff1030d7c5209a0",
                Article = article,
                Comment = cmt,
                Content = "vi pham",
                Date = DateTime.Now,
                PostId = "5d247a04eff1030d7c5209a0",
                PostType = "article",
                ReporterId = "5d247a04eff1030d7c5209a0",
                ReportType = reportType,
                ReportTypeId = "5d247a04eff1030d7c5209a0",
                CompanionPost = companionPost,
                VirtualTrip = virtualTrip               
            };

            _mockReportService = new Mock<IReportService>();
        }

        public IEnumerable<ReportType> ienumerableReportType()
        {
            yield return reportType;
        }

        public IEnumerable<Report> ienumerableReport()
        {
            yield return report;
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
            _mockReportService.Setup(x => x.GetAllReportType()).Returns(ienumerableReportType);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllReportType = reportController.GetAllReportType();
            var type = getAllReportType.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllArticleReports()
        {
            _mockReportService.Setup(x => x.GetAllArticleReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllArticleReports = reportController.GetAllArticleReports(6);
            var type = getAllArticleReports.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllCommentReports()
        {
            _mockReportService.Setup(x => x.GetAllCommentReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllCommentReports = reportController.GetAllCommentReports(6);
            var type = getAllCommentReports.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllCompanionPostReports()
        {
            _mockReportService.Setup(x => x.GetAllCompanionPostReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllCompanionPostReports = reportController.GetAllCompanionPostReports(6);
            var type = getAllCompanionPostReports.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetAllVirtualTripReports()
        {
            _mockReportService.Setup(x => x.GetAllVirtualTripReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportController = new ReportController(_mockReportService.Object);
            var getAllVirtualTripReports = reportController.GetAllVirtualTripReports(6);
            var type = getAllVirtualTripReports.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

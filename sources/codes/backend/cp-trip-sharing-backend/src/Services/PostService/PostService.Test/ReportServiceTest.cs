using Moq;
using NUnit.Framework;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class ReportServiceTest
    {
        Mock<IReportRepository> _mockReportRepository;
        Report report = null;
        Article article = null;
        Comment cmt = null;
        ReportType reportType = null;
        CompanionPost companionPost = null;
        CompanionPostJoinRequest companionPostJoinRequest = null;
        VirtualTrip virtualTrip = null;

        [SetUp]
        public void Config()
        {
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

            _mockReportRepository = new Mock<IReportRepository>();
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
        public void TestAdd()
        {
            _mockReportRepository.Setup(x => x.Add(It.IsAny<Report>())).Returns(report);
            var reportService = new ReportService(_mockReportRepository.Object);
            Report reportActual = reportService.Add(report);
            Assert.AreEqual(reportActual.Content, "vi pham");
        }

        [TestCase]
        public void TestDelete()
        {
            _mockReportRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var reportService = new ReportService(_mockReportRepository.Object);
            bool checkDelete = reportService.Delete("5d247a04eff1030d7c5209a0");
            Assert.IsTrue(checkDelete);
        }

        [TestCase]
        public void TestGetAllArticleReports()
        {
            _mockReportRepository.Setup(x => x.GetAllArticleReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllArticleReports = reportService.GetAllArticleReports(6);
            var itemActual = getAllArticleReports.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }

        [TestCase]
        public void TestGetAllCommentReports()
        {
            _mockReportRepository.Setup(x => x.GetAllCommentReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllCommentReports = reportService.GetAllCommentReports(6);
            var itemActual = getAllCommentReports.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }

        [TestCase]
        public void TestGetAllCompanionPostReports()
        {
            _mockReportRepository.Setup(x => x.GetAllCompanionPostReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllCompanionPostReports = reportService.GetAllCompanionPostReports(6);
            var itemActual = getAllCompanionPostReports.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }

        [TestCase]
        public void TestGetAllReportType()
        {
            _mockReportRepository.Setup(x => x.GetAllReportType()).Returns(ienumerableReportType);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<ReportType> getAllReportType = reportService.GetAllReportType();
            var itemActual = getAllReportType.FirstOrDefault();
            Assert.AreEqual(itemActual, reportType);
        }

        [TestCase]
        public void TestGetAllVirtualTripReports()
        {
            _mockReportRepository.Setup(x => x.GetAllVirtualTripReports(It.IsAny<int>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllVirtualTripReports = reportService.GetAllVirtualTripReports(6);
            var itemActual = getAllVirtualTripReports.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }
    }
}
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
                TargetType = "TargetType",
                IsResolved = false,
                ReporterId = "5d247a04eff1030d7c5209a0",
                ReportType = reportType,
                Target = null
            };

            _mockReportRepository = new Mock<IReportRepository>();
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
        public void TestGetAllReport()
        {
            IEnumerable<Report> ienumerableReport = new List<Report>
            {
                report
            };
            _mockReportRepository.Setup(x => x.GetAllReport(It.IsAny<string>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllReport = reportService.GetAllReport("comment");
            var itemActual = getAllReport.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }

        [TestCase]
        public void TestGetAllReportType()
        {
            IEnumerable<ReportType> ienumerableReportType = new List<ReportType> { 
                reportType
            };
            _mockReportRepository.Setup(x => x.GetAllReportType()).Returns(ienumerableReportType);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<ReportType> getAllReportType = reportService.GetAllReportType();
            var itemActual = getAllReportType.FirstOrDefault();
            Assert.AreEqual(itemActual, reportType);
        }

        [TestCase]
        public void TestUpdate()
        {
            report.Content = "update content report";
            _mockReportRepository.Setup(x => x.Update(It.IsAny<Report>())).Returns(report);
            var reportService = new ReportService(_mockReportRepository.Object);
            Report reportActual = reportService.Update(report);
            Assert.AreEqual(report, reportActual);
        }

    }
}
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServices.Models;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services;

namespace UserService.Test
{
    [TestFixture]
    class ReportServiceTest
    {
        Mock<IReportRepository> _mockReportRepository;
        Report report = null;
        ReportType reportType, reportTypeSecond = null;

        [SetUp]
        public void Config()
        {
            reportType = new ReportType()
            {
                Id = "5d027ea59b358d247cd219az",
                Name = "comment"
            };

            reportTypeSecond = new ReportType()
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
        public void TestDeleteReport()
        {
            _mockReportRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(report);
            var reportService = new ReportService(_mockReportRepository.Object);
            Report reportActual = reportService.DeleteReport("5d247a04eff1030d7c5209a0");
            Assert.AreEqual(report, reportActual);
        }

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<Report> ienumerableReport = new List<Report>{report};
            _mockReportRepository.Setup(x => x.GetAll(It.IsAny<int>())).Returns(ienumerableReport);
            var reportService = new ReportService(_mockReportRepository.Object);
            IEnumerable<Report> getAllReport = reportService.GetAll(6);
            var itemActual = getAllReport.FirstOrDefault();
            Assert.AreEqual(itemActual, report);
        }

        [TestCase]
        public void TestGetAllReportType()
        {
            IEnumerable<ReportType> ienumerableReportType = new List<ReportType> { reportType, reportTypeSecond };
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

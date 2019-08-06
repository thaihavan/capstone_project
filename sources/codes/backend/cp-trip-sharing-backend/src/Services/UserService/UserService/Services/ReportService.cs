using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository = null;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public ReportService(IOptions<AppSettings> settings)
        {
            _reportRepository = new ReportRepository(settings);
        }

        public Report Add(Report document)
        {
            return _reportRepository.Add(document);
        }

        public Report DeleteReport(string reportId)
        {
            return _reportRepository.Delete(reportId);
        }

        public IEnumerable<Report> GetAll(int page)
        {
            return _reportRepository.GetAll(page);
        }

        public IEnumerable<ReportType> GetAllReportType()
        {
            return _reportRepository.GetAllReportType();
        }
    }
}

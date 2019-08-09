using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;

namespace PostService.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository = null;

        public ReportService(IOptions<AppSettings> settings)
        {
            _reportRepository = new ReportRepository(settings);
        }

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        
        public Report Add(Report param)
        {
            return _reportRepository.Add(param);
        }

        public bool Delete(string id)
        {
            return _reportRepository.Delete(id);
        }

        public IEnumerable<Report> GetAllArticleReports(int page)
        {
            return _reportRepository.GetAllArticleReports(page);
        }

        public IEnumerable<Report> GetAllCommentReports(int page)
        {
            return _reportRepository.GetAllCommentReports(page);
        }

        public IEnumerable<Report> GetAllCompanionPostReports(int page)
        {
            return _reportRepository.GetAllCompanionPostReports(page);
        }

        public IEnumerable<ReportType> GetAllReportType()
        {
            return _reportRepository.GetAllReportType();
        }

        public IEnumerable<Report> GetAllVirtualTripReports(int page)
        {
            return _reportRepository.GetAllVirtualTripReports(page);
        }
    }
}

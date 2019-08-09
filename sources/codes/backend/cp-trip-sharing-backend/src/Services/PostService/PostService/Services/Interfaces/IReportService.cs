using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetAllArticleReports(int page);
        IEnumerable<Report> GetAllCompanionPostReports(int page);
        IEnumerable<Report> GetAllVirtualTripReports(int page);
        IEnumerable<Report> GetAllCommentReports(int page);

        IEnumerable<ReportType> GetAllReportType();

        Report Add(Report param);
        bool Delete(string id);

    }
}

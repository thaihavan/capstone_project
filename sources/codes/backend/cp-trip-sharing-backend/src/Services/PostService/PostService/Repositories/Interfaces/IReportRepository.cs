using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IReportRepository:IRepository<Report>
    {
        IEnumerable<Report> GetAllArticleReports(int page);
        IEnumerable<Report> GetAllCompanionPostReports(int page);
        IEnumerable<Report> GetAllVirtualTripReports(int page);
        IEnumerable<Report> GetAllCommentReports(int page);

        IEnumerable<ReportType> GetAllReportType();
    }
}

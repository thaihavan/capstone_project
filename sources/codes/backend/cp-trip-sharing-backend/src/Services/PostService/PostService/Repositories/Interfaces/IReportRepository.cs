using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IReportRepository:IRepository<Report>
    {
        IEnumerable<Report> GetAllReport(string targetType);

        IEnumerable<ReportType> GetAllReportType();
    }
}

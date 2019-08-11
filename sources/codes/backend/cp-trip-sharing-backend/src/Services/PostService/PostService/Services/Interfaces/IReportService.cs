using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetAllReport(string targetType);

        IEnumerable<ReportType> GetAllReportType();

        Report Add(Report param);
        bool Delete(string id);
        Report Update(Report param);

    }
}

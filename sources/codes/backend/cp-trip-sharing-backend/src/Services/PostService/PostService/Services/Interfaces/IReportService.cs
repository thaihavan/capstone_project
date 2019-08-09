using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetAllReport(int page);

        IEnumerable<ReportType> GetAllReportType();

        Report Add(Report param);
        bool Delete(string id);
        Report Update(Report param);

    }
}

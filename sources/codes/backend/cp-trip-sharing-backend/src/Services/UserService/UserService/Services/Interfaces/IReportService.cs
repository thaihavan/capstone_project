using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IReportService
    {
        Report Add(Report document);

        Report Update(Report document);

        IEnumerable<Report> GetAll(int page);

        IEnumerable<ReportType> GetAllReportType();

        Report DeleteReport(string reportId);
    }
}

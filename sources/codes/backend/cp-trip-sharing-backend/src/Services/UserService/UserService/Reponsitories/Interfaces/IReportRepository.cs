using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IReportRepository:IRepository<Report>
    {
        IEnumerable<Report> GetAll(int page);

        IEnumerable<ReportType> GetAllReportType();

        Report Delete(string reportId);
    }
}

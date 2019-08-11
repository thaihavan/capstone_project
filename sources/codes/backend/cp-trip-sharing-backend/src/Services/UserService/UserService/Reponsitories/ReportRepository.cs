
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class ReportRepository:IReportRepository
    {
        private readonly IMongoCollection<User> _users = null;
        private readonly IMongoCollection<Report> _reports = null;
        private readonly IMongoCollection<ReportType> _reportTypes = null;

        public ReportRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _users = dbContext.Users;
            _reports = dbContext.Reports;
            _reportTypes = dbContext.ReportTypes;
        }

        public Report Add(Report document)
        {
            document.Date = DateTime.Now;
            _reports.InsertOne(document);
            return document;
        }

        public Report Delete(Report document)
        {
            throw new NotImplementedException();
        }

        public Report Delete(string reportId)
        {
            return _reports.FindOneAndDelete(x => x.Id.Equals(reportId));
        }

        public IEnumerable<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> GetAll(int page)
        {
            Func<Report, User, Report> selectReportWithUser =
                (report, user) => { report.Target = user; return report; };
            Func<Report, ReportType, Report> selectReportWithReportType =
                (report, type) => { report.ReportType = type; return report; };

            var reports = _reports.AsQueryable()
                .Join(
                    _users.AsQueryable(),
                    report => report.TargetId,
                    user => user.Id,
                    selectReportWithUser)
                .Join(
                    _reportTypes.AsQueryable(),
                    report => report.ReportTypeId,
                    reportType => reportType.Id,
                    selectReportWithReportType)
                .Where(r => !r.IsResolved)
                .OrderByDescending(x => x.Date)
                .Skip(12 * (page - 1))
                .Take(12);
            return reports;
        }

        public IEnumerable<ReportType> GetAllReportType()
        {
            return _reportTypes.Find(x => true).ToList();
        }

        public Report GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Report Update(Report document)
        {
            var result = _reports.ReplaceOne(r => r.Id.Equals(document.Id), document);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return document;
        }
    }
}

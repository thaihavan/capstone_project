using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;

namespace PostService.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Article> _articles = null;
        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Comment> _comments = null;
        private readonly IMongoCollection<CompanionPost> _companionPosts = null;
        private readonly IMongoCollection<VirtualTrip> _virtualTrips = null;
        private readonly IMongoCollection<Report> _reports = null;
        private readonly IMongoCollection<ReportType> _reportTypes = null;

        public ReportRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _articles = dbContext.Articles;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
            _companionPosts = dbContext.CompanionPosts;
            _virtualTrips = dbContext.VirtualTrips;
            _comments = dbContext.Comments;
            _reports = dbContext.Reports;
            _reportTypes = dbContext.ReportTypes;
        }

        public Report Add(Report param)
        {
            _reports.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            _reports.FindOneAndDelete(x => x.Id.Equals(id));
            return true;
        }

        public IEnumerable<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Report> GetAllReport(int page)
        {
            Func<Report, Post, Report> selectReportedPost =
                ((report, post) => { report.Target = post; return report; });
            Func<Report, Comment, Report> selectReportedComment =
                ((report, Comment) => { report.Target = Comment; return report; });

            var reports= _reports.AsQueryable()
                .Join(_posts.AsQueryable(),
                report=>report.TargetId,
                post=>post.Id,
                selectReportedPost)
                .Join(_comments.AsQueryable(),
                report=>report.TargetId,
                comment=>comment.Id,
                selectReportedComment)
                .OrderByDescending(x=>x.Date)
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

        public Report Update(Report param)
        {
            var result = _reports.ReplaceOne(r => r.Id.Equals(param.Id), param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }
    }
}

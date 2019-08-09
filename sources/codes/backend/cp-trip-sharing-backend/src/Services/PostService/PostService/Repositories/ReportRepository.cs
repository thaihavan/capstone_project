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

        public IEnumerable<Report> GetAllArticleReports(int page)
        {
            Func<Article, Post, Article> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<Article, Author, Article> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });
            Func<Article, Report, Report> SelectPostReportWithArticle =
                ((article, report) => { report.Article = article; return report; });
            Func<Report, ReportType, Report> selectReportType =
                ((report, type) => { report.ReportType = type; return report; });

            var reports = _articles.AsQueryable()
                .Join(
                    _posts.AsQueryable(),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                ).Join(
                    _reports.AsQueryable(),
                    article => article.PostId,
                    report => report.ReporterId,
                    SelectPostReportWithArticle
                ).Join(_reportTypes.AsQueryable(),
                report=>report.ReportTypeId,
                type=>type.Id,
                selectReportType
                ).OrderByDescending(x=>x.Date)
                .Skip(12 * (page - 1))
                .Take(12);
            return reports.ToList();
        }

        public IEnumerable<Report> GetAllCommentReports(int page)
        {
            Func<Comment, Author, Comment> selectCommentWithAuthor =
                ((comment, author) => { comment.Author = author; return comment; });
            Func<Comment, Report, Report> SelectPostReportWithArticle =
               ((article, report) => { report.Comment = article; return report; });
            Func<Report, ReportType, Report> selectReportType =
                ((report, type) => { report.ReportType = type; return report; });

            var reports = _comments.AsQueryable()
                .Join(_authors.AsQueryable(),
                comment => comment.AuthorId,
                author => author.Id,
                selectCommentWithAuthor
                ).Join(_reports.AsQueryable(),
                comment=>comment.Id,
                report=>report.ReporterId,
                SelectPostReportWithArticle
                ).Join(_reportTypes.AsQueryable(),
                report => report.ReportTypeId,
                type => type.Id,
                selectReportType
                ).OrderByDescending(x => x.Date)
                .Skip(12 * (page - 1))
                .Take(12);
            return reports.ToList();
        }

        public IEnumerable<Report> GetAllCompanionPostReports(int page)
        {
            Func<CompanionPost, Post, CompanionPost> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<CompanionPost, Author, CompanionPost> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });
            Func<CompanionPost, Report, Report> SelectPostReportWithArticle =
                ((article, report) => { report.CompanionPost = article; return report; });
            Func<Report, ReportType, Report> selectReportType =
                ((report, type) => { report.ReportType = type; return report; });

            var reports = _companionPosts.AsQueryable()
                .Join(
                    _posts.AsQueryable(),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                ).Join(
                    _reports.AsQueryable(),
                    article => article.PostId,
                    report => report.ReporterId,
                    SelectPostReportWithArticle
                ).Join(_reportTypes.AsQueryable(),
                report => report.ReportTypeId,
                type => type.Id,
                selectReportType
                ).OrderByDescending(x => x.Date)
                .Skip(12 * (page - 1))
                .Take(12);
            return reports.ToList();
        }

        public IEnumerable<ReportType> GetAllReportType()
        {
            return _reportTypes.Find(x => true).ToList();
        }

        public IEnumerable<Report> GetAllVirtualTripReports(int page)
        {
            Func<VirtualTrip, Post, VirtualTrip> SelectArticleWithPost =
                ((article, post) => { article.Post = post; return article; });
            Func<VirtualTrip, Author, VirtualTrip> SelectArticleWithAuthor =
                ((article, author) => { article.Post.Author = author; return article; });
            Func<VirtualTrip, Report, Report> SelectPostReportWithArticle =
                ((article, report) => { report.VirtualTrip = article; return report; });
            Func<Report, ReportType, Report> selectReportType =
                ((report, type) => { report.ReportType = type; return report; });

            var reports = _virtualTrips.AsQueryable()
                .Join(
                    _posts.AsQueryable(),
                    article => article.PostId,
                    post => post.Id,
                    SelectArticleWithPost
                ).Join(
                    _authors.AsQueryable(),
                    article => article.Post.AuthorId,
                    author => author.Id,
                    SelectArticleWithAuthor
                ).Join(
                    _reports.AsQueryable(),
                    article => article.PostId,
                    report => report.ReporterId,
                    SelectPostReportWithArticle
                ).Join(_reportTypes.AsQueryable(),
                report => report.ReportTypeId,
                type => type.Id,
                selectReportType
                ).OrderByDescending(x => x.Date)
                .Skip(12 * (page - 1))
                .Take(12);
            return reports.ToList();
        }

        public Report GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Report Update(Report param)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class CompanionPostRepository : ICompanionPostRepository
    {
        private readonly IMongoCollection<CompanionPost> _companionPosts = null;
        private readonly IMongoCollection<CompanionPostJoinRequest> _companionPostJoinRequests = null;
        private readonly IMongoCollection<Post> _posts = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Like> _likes = null;

        public CompanionPostRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _companionPosts = dbContext.CompanionPosts;
            _companionPostJoinRequests = dbContext.CompanionPostJoinRequests;
            _posts = dbContext.Posts;
            _authors = dbContext.Authors;
            _likes = dbContext.Likes;
        }

        public CompanionPost Add(CompanionPost param)
        {
            _companionPosts.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            var postId = _companionPosts.Find(x => x.Id.Equals(id)).FirstOrDefault().PostId;
            _posts.FindOneAndUpdate(
                Builders<Post>.Filter.Eq(x => x.Id, postId),
                Builders<Post>.Update.Set(x => x.IsActive, false)
                );
            return true;
        }

        public IEnumerable<CompanionPost> GetAll()
        {
            throw new NotImplementedException();
        }

        public CompanionPost GetById(string id)
        {
            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });

            var result = _companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .FirstOrDefault();
            return result;
        }

        public CompanionPost Update(CompanionPost param)
        {
            _companionPosts.FindOneAndReplace(
                Builders<CompanionPost>.Filter.Eq(x => x.Id, param.Id),
                param
                );
            return param;
        }

        public CompanionPost GetById(string id,string userId)
        {
            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post;return companionPost; });
            Func<CompanionPost,Author,CompanionPost> SelectCompanionPostWithAuthor=
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });
            Func<CompanionPost,IEnumerable<Like>,CompanionPost> UpdateLike=
                ((CompanionPost, likes) => { CompanionPost.Post.liked = likes.Count() > 0 ? true : false; return CompanionPost; });

            CompanionPost result;

            if (string.IsNullOrEmpty(userId))
            {
                result=_companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(  
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .FirstOrDefault();
            }
            else
            {
                result = _companionPosts.AsQueryable().Where(x => x.Id.Equals(id))
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .GroupJoin(
                        _likes.AsQueryable().Where(x=>x.UserId.Equals(userId)&&x.ObjectType.Equals("post")),
                        companionPost=>companionPost.PostId,
                        like=>like.ObjectId,
                        UpdateLike)
                    .FirstOrDefault();
            }
             
            return result;
        }

        public IEnumerable<CompanionPost> GetAll(string userId)
        {
            Func<CompanionPost, Post, CompanionPost> SelectCompanionPostWithPost =
                ((companionPost, post) => { companionPost.Post = post; return companionPost; });
            Func<CompanionPost, Author, CompanionPost> SelectCompanionPostWithAuthor =
                ((companionPost, author) => { companionPost.Post.Author = author; return companionPost; });
            Func<CompanionPost, IEnumerable<Like>, CompanionPost> UpdateLike =
                ((CompanionPost, likes) => { CompanionPost.Post.liked = likes.Count() > 0 ? true : false; return CompanionPost; });

            IEnumerable< CompanionPost> result;

            if (string.IsNullOrEmpty(userId))
            {
                result = _companionPosts.AsQueryable()
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .ToList();
            }
            else
            {
                result = _companionPosts.AsQueryable()
                    .Join(
                        _posts.AsQueryable(),
                        companionPost => companionPost.PostId,
                        post => post.Id,
                        SelectCompanionPostWithPost)
                    .Join(
                        _authors.AsQueryable(),
                        companionPostWithPost => companionPostWithPost.Post.AuthorId,
                        author => author.Id,
                        SelectCompanionPostWithAuthor)
                    .GroupJoin(
                        _likes.AsQueryable().Where(x => x.UserId.Equals(userId) && x.ObjectType.Equals("post")),
                        companionPost => companionPost.PostId,
                        like => like.ObjectId,
                        UpdateLike)
                    .ToList();
            }
            return result;
        }

        public IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId)
        {
            return _companionPostJoinRequests
                .Find(Builders<CompanionPostJoinRequest>.Filter.Eq(x => x.CompanionPostId, companionPostId))
                .ToList();
        }

        public CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param)
        {
            _companionPostJoinRequests.InsertOne(param);
            return param;
        }

        public bool DeleteJoinRequest(string requestId)
        {
            _companionPostJoinRequests.FindOneAndDelete(
                Builders<CompanionPostJoinRequest>.Filter.Eq(x => x.Id, requestId));
            return true;
        }
    }
}

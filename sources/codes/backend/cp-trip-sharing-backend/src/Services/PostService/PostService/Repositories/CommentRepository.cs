using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comments = null;
        private readonly IMongoCollection<Author> _authors = null;
        private readonly IMongoCollection<Like> _likes = null;
        private readonly IMongoCollection<Post> _posts = null;

        public CommentRepository(IMongoCollection<Comment> comments, IMongoCollection<Author> authors)
        {
            _comments = comments;
            _authors = authors;
        }

        public CommentRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _comments = dbContext.Comments;
            _authors = dbContext.Authors;
            _likes = dbContext.Likes;
            _posts = dbContext.Posts;
        }

        public Comment Add(Comment param)
        {
            _comments.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            List<Comment> comments = new List<Comment>();
            var postId = _comments.Find(x => x.Id.Equals(id)).FirstOrDefault().PostId;
            var allComment =_comments.Find(x=>x.PostId.Equals(postId)).ToList();
            var dict = allComment.ToDictionary(x => x.Id, x => x);
            foreach (var x in dict)
            {
                if (x.Value.ParentId == null)
                {
                    comments.Add(x.Value);
                }
                else
                {
                    var parent = dict[x.Value.ParentId];
                    parent.Childs.Add(x.Value);
                }
            }
            var comment = dict[id];
            var commentIds = new List<string>();
            GetChild(comment, commentIds);

            return _comments.DeleteMany(Builders<Comment>.Filter.In(x => x.Id, commentIds)).IsAcknowledged;           
        }

        public IEnumerable<Comment> GetAll()
        {
            return _comments.Find(x => true).ToList();
        }

        public Comment GetById(string id)
        {
            return _comments.Find(c => c.Id == id).ToList().FirstOrDefault();
        }

        public Comment Update(Comment param)
        {
            var filter = Builders<Comment>.Filter.Eq(x => x.Id, param.Id);
            var result = _comments.ReplaceOne(filter, param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<Comment> GetCommentByPost(string postId)
        {
            var comments = _comments.AsQueryable().Where(x => x.IsActive)
                .Where(c => c.PostId == postId)
                .Join(
                   _authors.AsQueryable(),
                   comment => comment.AuthorId,
                   author => author.Id,
                   (comment, author) => new Comment
                   {
                       Id = comment.Id,
                       AuthorId = comment.AuthorId,
                       IsActive = comment.IsActive,
                       Content = comment.Content,
                       ParentId = comment.ParentId,
                       PostId = comment.PostId,
                       Date = comment.Date,
                       Author = new Author()
                       {
                           Id = author.Id,
                           DisplayName = author.DisplayName,
                           ProfileImage = author.ProfileImage
                       },
                       LikeCount= comment.LikeCount
                   })
                .OrderByDescending(c => c.Date);

            return comments.ToList();
        }

        public bool IncreaseLikeCount(string commentId)
        {
            _comments.FindOneAndUpdate(
                c => c.Id == commentId,
                Builders<Comment>.Update.Inc("like_count", 1)
                );
            return true;
        }

        public bool DecreaseLikeCount(string commentId)
        {
            _comments.FindOneAndUpdate(
                c => c.Id == commentId,
                Builders<Comment>.Update.Inc("like_count", -1)
                );
            return true;
        }

        public IEnumerable<Comment> GetCommentByPost(string postId, string userId)
        {
            Func<Comment, IEnumerable<Like>, Comment> UpdateLiked =
                ((comment, likes) => { comment.Liked = likes.Count() > 0 ? true : false; return comment; });
            var comments = _comments.AsQueryable()
                .Where(c => c.PostId == postId && c.IsActive)
                .Join(
                   _authors.AsQueryable(),
                   comment => comment.AuthorId,
                   author => author.Id,
                   (comment, author) => new Comment
                   {
                       Id = comment.Id,
                       AuthorId = comment.AuthorId,
                       IsActive = comment.IsActive,
                       Content = comment.Content,
                       ParentId = comment.ParentId,
                       PostId = comment.PostId,
                       Date = comment.Date,
                       Author = new Author()
                       {
                           Id = author.Id,
                           DisplayName = author.DisplayName,
                           ProfileImage = author.ProfileImage
                       },
                       LikeCount = comment.LikeCount
                   }).GroupJoin(
                    _likes.AsQueryable().Where(x=>x.ObjectType=="comment"&&x.UserId==userId),
                    comment=>comment.Id,
                    like=>like.ObjectId,
                    UpdateLiked
                )
                .OrderByDescending(c => c.Date);
            return comments.ToList();
        }

        void GetChild(Comment comment,List<string> commentIds)
        {
            commentIds.Add(comment.Id);
            foreach (Comment cmt in comment.Childs)
            {               
                GetChild(cmt, commentIds);
            }
        }
    }
}

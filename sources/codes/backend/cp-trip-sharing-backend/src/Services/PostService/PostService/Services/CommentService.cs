using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository = null;
        private readonly IPostRepository _postRepository = null;
        private readonly IOptions<AppSettings> _settings = null;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IOptions<AppSettings> settings)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _settings = settings;

        }

        public CommentService(IOptions<AppSettings> settings)
        {
            _commentRepository = new CommentRepository(settings);
            _postRepository = new PostRepository(settings);
        }

        public Comment Add(Comment param)
        {
            _postRepository.IncreaseCommentCount(param.PostId);
            return _commentRepository.Add(param);
        }

        public bool Delete(string id)
        {
            var postId=_commentRepository.GetById(id).PostId;
            _commentRepository.Delete(id);
            _postRepository.DecreaseCommentCount(postId);
            return true;
        }

        public IEnumerable<Comment> GetCommentByPost(string id)
        {
            List<Comment> comments = new List<Comment>();
            var allComment = _commentRepository.GetCommentByPost(id);
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
            return comments;
        }

        public Comment Update(Comment cmt)
        {
            return _commentRepository.Update(cmt);
        }

        public IEnumerable<Comment> GetCommentByPost(string postId, string userId)
        {
            List<Comment> comments = new List<Comment>();
            var allComment = _commentRepository.GetCommentByPost(postId,userId);
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
            return comments;
        }
    }
}

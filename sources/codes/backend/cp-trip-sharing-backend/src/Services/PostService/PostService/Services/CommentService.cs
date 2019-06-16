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

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public CommentService(IOptions<AppSettings> settings)
        {
            _commentRepository = new CommentRepository(settings);
        }

        public Comment Add(Comment param)
        {
            return _commentRepository.Add(param);
        }


        public bool Delete(string id)
        {
            return _commentRepository.Delete(id);
        }

        public IEnumerable<Comment> GetCommentByPost(string id)
        {
            return _commentRepository.CommentDict(id);
        }

        public Comment Update(Comment cmt)
        {
            return _commentRepository.Update(cmt);
        }
    }
}

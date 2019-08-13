using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository = null;
        private readonly IPostRepository _postRepository = null;
        private readonly ICommentRepository _commentRepository = null;
        private readonly IPublishToTopic _publishToTopic = null;

        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public LikeService(IOptions<AppSettings> settings)
        {
            _likeRepository = new LikeRepository(settings);
            _postRepository = new PostRepository(settings);
            _commentRepository = new CommentRepository(settings);
            _publishToTopic = new PublishToTopic();
        }

        public Like Add(Like like)
        {
            switch (like.ObjectType)
            {
                case "post":
                    IncreaseCPFor100LikesAsync(like.ObjectId);
                    _postRepository.IncreaseLikeCount(like.ObjectId);
                    break;
                case "comment":
                    _commentRepository.IncreaseLikeCount(like.ObjectId);
                    break;
            }
            return _likeRepository.Add(like);
        }

        public bool Delete(Like like)
        {
            switch (like.ObjectType)
            {
                case "post":
                    _postRepository.DecreaseLikeCount(like.ObjectId);
                    break;
                case "comment":
                    _commentRepository.DecreaseLikeCount(like.ObjectId);
                    break;
            }
            return _likeRepository.Delete(like.ObjectId, like.UserId);
        }

        private async Task IncreaseCPFor100LikesAsync(string postId)
        {
            var post = _postRepository.GetById(postId);
            int x = post.LikeCount % 100;
            if (x + 1 == 100)
            {
                _publishToTopic.PublishCP(new IncreasingCP()
                {
                    UserId = post.AuthorId,
                    Point = 1
                });
            }
        }
    }
}

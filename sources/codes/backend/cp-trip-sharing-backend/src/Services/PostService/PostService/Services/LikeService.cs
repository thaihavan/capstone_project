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

        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
        }

        public LikeService(IOptions<AppSettings> settings)
        {
            _likeRepository = new LikeRepository(settings);
            _postRepository = new PostRepository(settings);
        }

        public Like Add(Like like)
        {
            switch (like.ObjectType)
            {
                case "post":
                    //_postRepository.IncreaseLikeCount(like.ObjectId);
                    break;
                case "comment":
                    break;
            }
            return _likeRepository.Add(like);
        }

        public bool Delete(Like like)
        {
            switch (like.ObjectType)
            {
                case "post":
                    //_postRepository.DecreaseLikeCount(like.ObjectId);
                    break;
                case "comment":
                    break;
            }
            return _likeRepository.Delete(like.ObjectId, like.UserId);
        }
    }
}

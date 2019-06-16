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

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public LikeService(IOptions<AppSettings> settings)
        {
            _likeRepository = new LikeRepository(settings);
        }

        public Like Add(Like like)
        {
            return _likeRepository.Add(like);
        }

        public bool Delete(string objectId, string userId)
        {
            return _likeRepository.Delete(objectId, userId);
        }

        public IEnumerable<Like> GetLikeWithPost()
        {
            return _likeRepository.GetLikeWithPost();
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services.Interfaces;

namespace UserServices.Services
{
    public class FollowService : IFollowService
    {
        private readonly FollowRepository _followRepository = null;

        public FollowService(FollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public FollowService(IOptions<AppSettings> settings)
        {
            _followRepository = new FollowRepository(settings);
        }

        public Follow AddFollows(Follow follow)
        {

            return _followRepository.Add(follow);
        }

        public Follow Unfollow(Follow follow)
        {
            return _followRepository.Unfollow(follow);
        }
    }
}

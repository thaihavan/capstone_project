using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;

namespace PostService.Services
{
    public class CompanionPostService : ICompanionPostService
    {
        private readonly ICompanionPostRepository _companionPostRepository = null;
        private readonly IOptions<AppSettings> _settings = null;

        public CompanionPostService(IOptions<AppSettings> settings)
        {
            _companionPostRepository = new CompanionPostRepository(settings);
            _settings = settings;
        }

        public CompanionPost Add(CompanionPost post)
        {
            return _companionPostRepository.Add(post);
        }

        public CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param)
        {
            return _companionPostRepository.AddNewRequest(param);
        }

        public bool CancelRequest(string userId, string postId)
        {
            var request = _companionPostRepository.GetRequestByUserIdAndPostId(userId, postId);
            if (request == null) return false;
            return _companionPostRepository.DeleteJoinRequest(request.Id);
        }

        public bool Delete(string id)
        {
            return _companionPostRepository.Delete(id);
        }

        public bool DeleteJoinRequest(string requestId)
        {
            return _companionPostRepository.DeleteJoinRequest(requestId);
        }

        public IEnumerable<CompanionPost> GetAll(PostFilter filter, int page)
        {
            return _companionPostRepository.GetAll(filter, page);
        }

        public IEnumerable<CompanionPost> GetAllCompanionPostByUser(string userId, PostFilter filter, int page)
        {
            return _companionPostRepository.GetAllCompanionPostByUser(userId, filter, page);
        }

        public IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId)
        {
            return _companionPostRepository.GetAllJoinRequest(companionPostId);
        }

        public CompanionPost GetById(string id)
        {
            return _companionPostRepository.GetById(id);
        }

        public CompanionPost GetById(string id, string userId)
        {
            return _companionPostRepository.GetById(id, userId);
        }

        public int GetNumberOfCompanionPost(PostFilter filter)
        {
            return _companionPostRepository.GetNumberOfCompanionPost(filter);
        }

        public CompanionPostJoinRequest GetRequestById(string requestId)
        {
            return _companionPostRepository.GetRequestById(requestId);
        }

        public CompanionPost Update(CompanionPost post)
        {
            return _companionPostRepository.Update(post);
        }
    }
}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;

namespace PostService.Services
{
    public class CompanionPostService:ICompanionPostService
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
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            return _companionPostRepository.Delete(id);
        }

        public bool DeleteJoinRequest(string requestId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanionPost> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanionPost> GetAll(string userId)
        {
            return _companionPostRepository.GetAll(userId);
        }

        public IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId)
        {
            throw new NotImplementedException();
        }

        public CompanionPost GetById(string id)
        {
            return _companionPostRepository.GetById(id);
        }

        public CompanionPost GetById(string id, string userId)
        {
            return _companionPostRepository.GetById(id, userId);
        }

        public CompanionPost Update(CompanionPost post)
        {
            throw new NotImplementedException();
        }
    }
}

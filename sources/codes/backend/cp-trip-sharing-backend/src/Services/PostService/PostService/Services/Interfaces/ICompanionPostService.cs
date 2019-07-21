using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface ICompanionPostService
    {
        CompanionPost Add(CompanionPost post);

        CompanionPost Update(CompanionPost post);

        bool Delete(string id);

        CompanionPost GetById(string id);     
        
        CompanionPost GetById(string id, string userId);

        IEnumerable<CompanionPost> GetAll(PostFilter filter, int page);

        IEnumerable<CompanionPost> GetAllCompanionPostByUser(string userId, PostFilter filter, int page);

        IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId);

        CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param);

        bool DeleteJoinRequest(string requestId);

        CompanionPostJoinRequest GetRequestById(string requestId);

        bool CancelRequest(string userId, string postId);

    }
}

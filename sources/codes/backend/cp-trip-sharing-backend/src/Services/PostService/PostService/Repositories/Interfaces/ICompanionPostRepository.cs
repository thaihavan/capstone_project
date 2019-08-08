using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
   public interface ICompanionPostRepository:IRepository<CompanionPost>
    {
        CompanionPost GetById(string id, string userId);

        IEnumerable<CompanionPost> GetAll(PostFilter filter,int page);

        IEnumerable<CompanionPost> GetAllCompanionPostByUser(string userId, PostFilter filter, int page);

        IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId);

        CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param);

        bool DeleteJoinRequest(string requestId);

        CompanionPostJoinRequest GetRequestById(string requestId);

        CompanionPostJoinRequest GetRequestByUserIdAndPostId(string userId, string postId);

        object GetCompanionPostStatistics(StatisticsFilter filter);
    }
}

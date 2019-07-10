using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    interface ICompanionPostRepository:IRepository<CompanionPost>
    {
        CompanionPost GetById(string id, string userId);
        IEnumerable<CompanionPost> GetAll(string userId);

        IEnumerable<CompanionPostJoinRequest> GetAllJoinRequest(string companionPostId);
        CompanionPostJoinRequest AddNewRequest(CompanionPostJoinRequest param);
        bool DeleteJoinRequest(string requestId);
    }
}

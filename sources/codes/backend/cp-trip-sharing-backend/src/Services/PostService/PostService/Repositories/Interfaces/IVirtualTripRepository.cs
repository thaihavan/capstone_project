using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IVirtualTripRepository : IRepository<VirtualTrip>
    {
        VirtualTrip GetVirtualTrip(string id);

        IEnumerable<VirtualTrip> GetAllVirtualTrips(string userId, PostFilter postFilter, int page);

        IEnumerable<VirtualTrip> GetAllVirtualTrips(PostFilter postFilter, int page);

        IEnumerable<VirtualTrip> GetAllVirtualTripsByUser(string userId, PostFilter postFilter, int page);
    }
}

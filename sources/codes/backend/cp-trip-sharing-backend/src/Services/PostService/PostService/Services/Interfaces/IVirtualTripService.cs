using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IVirtualTripService
    {
        VirtualTrip GetById(string id);

        VirtualTrip Add(VirtualTrip virtualTrip);

        VirtualTrip Update(VirtualTrip virtualTrip);

        VirtualTrip GetVirtualTrip(string id);

        bool Delete(string id);

        IEnumerable<VirtualTrip> GetAllVirtualTrips(PostFilter postFilter, int page);

        IEnumerable<VirtualTrip> GetAllVirtualTripsByUser(string userId, PostFilter postFilter, int page);

        int GetNumberOfVirtualTrip(PostFilter filter);
    }
}

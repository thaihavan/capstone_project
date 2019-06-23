using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IVirtualTripService
    {
        IEnumerable<VirtualTrip> GetAllVirtualTripWithPost();

        VirtualTrip GetById(string id);

        VirtualTrip Add(VirtualTrip virtualTrip);

        VirtualTrip Update(VirtualTrip virtualTrip);

        bool Delete(string id);

        IEnumerable<VirtualTrip> GetAllVirtualTripWithPost(string userId);
    }
}

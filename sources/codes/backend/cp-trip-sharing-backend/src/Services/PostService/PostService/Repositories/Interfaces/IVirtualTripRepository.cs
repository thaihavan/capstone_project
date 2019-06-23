using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IVirtualTripRepository : IRepository<VirtualTrip>
    {
        IEnumerable<VirtualTrip> GetAllVirtualTripWithPost();

        IEnumerable<VirtualTrip> GetAllVirtualTripWithPost(string userId);
    }
}

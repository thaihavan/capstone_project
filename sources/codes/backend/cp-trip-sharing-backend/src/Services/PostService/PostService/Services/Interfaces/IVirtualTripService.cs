using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IVirtualTripService
    {
        IEnumerable<VirtualTrip> GetAllTripWithPost();

        VirtualTrip GetById(string id);
    }
}

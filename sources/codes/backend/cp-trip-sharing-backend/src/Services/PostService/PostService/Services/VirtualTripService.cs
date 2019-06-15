using PostService.Models;
using PostService.Repositories;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class VirtualTripService : IVirtualTripService
    {
        private readonly VirtualTripRepository _virtualTripRepository = null;

        public VirtualTripService()
        {
            _virtualTripRepository = new VirtualTripRepository();
        }

        public IEnumerable<VirtualTrip> GetAllTripWithPost()
        {
            return _virtualTripRepository.GetAllTripWithPost();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTripRepository.GetById(id);
        }
    }
}

using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class VirtualTripService : IVirtualTripService
    {
        private readonly IVirtualTripRepository _virtualTripRepository = null;

        public VirtualTripService(IOptions<AppSettings> settings)
        {
            _virtualTripRepository = new VirtualTripRepository(settings);
        }

        public VirtualTripService(IVirtualTripRepository virtualTripRepository)
        {
            _virtualTripRepository = virtualTripRepository;
        }

        public VirtualTrip Add(VirtualTrip virtualTrip)
        {
            return _virtualTripRepository.Add(virtualTrip);
        }

        public bool Delete(string id)
        {
            return _virtualTripRepository.Delete(id);
        }

        public VirtualTrip GetVirtualTrip(string id)
        {
            return _virtualTripRepository.GetVirtualTrip(id);
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTripRepository.GetById(id);
        }

        public VirtualTrip Update(VirtualTrip virtualTrip)
        {
            return _virtualTripRepository.Update(virtualTrip);
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTrips(PostFilter postFilte)
        {
            return _virtualTripRepository.GetAllVirtualTrips(postFilte);
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripsByUser(string userId, PostFilter postFilte)
        {
            return _virtualTripRepository.GetAllVirtualTripsByUser(userId, postFilte);
        }
    }
}

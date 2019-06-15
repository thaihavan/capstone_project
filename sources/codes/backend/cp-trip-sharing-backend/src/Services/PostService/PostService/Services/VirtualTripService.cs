using Microsoft.Extensions.Options;
using PostService.Helpers;
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

        public VirtualTripService(IOptions<AppSettings> settings)
        {
            _virtualTripRepository = new VirtualTripRepository(settings);
        }

        public VirtualTrip Add(VirtualTrip virtualTrip)
        {
            return _virtualTripRepository.Add(virtualTrip);
        }

        public bool Delete(string id)
        {
            return _virtualTripRepository.Delete(id);
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripWithPost()
        {
            return _virtualTripRepository.GetAllVirtualTripWithPost();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTripRepository.GetById(id);
        }

        public VirtualTrip Update(VirtualTrip virtualTrip)
        {
            return _virtualTripRepository.Update(virtualTrip);
        }
    }
}

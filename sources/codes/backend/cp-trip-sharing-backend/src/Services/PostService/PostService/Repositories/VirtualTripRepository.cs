using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Models;
using PostService.Repositories.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories
{
    public class VirtualTripRepository : IRepository<VirtualTrip>
    {
        private readonly IMongoCollection<VirtualTrip> _virtualTrips = null;

        public VirtualTripRepository()
        {
            var dbContext = new MongoDbContext();
            _virtualTrips = dbContext.VirtualTrips;
        }

        public VirtualTrip Add(VirtualTrip param)
        {
            _virtualTrips.InsertOne(param);
            return param;
        }

        public IEnumerable<VirtualTrip> GetAll()
        {
            return _virtualTrips.Find(x => true).ToList();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTrips.Find(Builders<VirtualTrip>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }

        
    }
}

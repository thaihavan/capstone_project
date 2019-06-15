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
        private readonly IMongoCollection<Post> _post = null;

        public VirtualTripRepository()
        {
            var dbContext = new MongoDbContext();
            _virtualTrips = dbContext.VirtualTrips;
            _post = dbContext.Posts;
        }

        public VirtualTrip Add(VirtualTrip param)
        {
            _virtualTrips.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            return _virtualTrips.DeleteOne(x => x.Id.Equals(new BsonObjectId(id))).IsAcknowledged;
        }

        public IEnumerable<VirtualTrip> GetAll()
        {
            return _virtualTrips.Find(x => true).ToList();
        }

        public IEnumerable<VirtualTrip> GetAllTripWithPost()
        {
            var virtualTrips = from vt in _virtualTrips.AsQueryable()
                               join p in _post.AsQueryable() on vt.PostId equals p.Id into joined
                               from post in joined
                               select new VirtualTrip
                               {
                                   Id = vt.Id,
                                   PostId = vt.PostId,
                                   Items = vt.Items,
                                   Post = post
                               };
            return virtualTrips.ToList();
        }

        public VirtualTrip GetById(string id)
        {
            return _virtualTrips.Find(Builders<VirtualTrip>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }

        public VirtualTrip Update(VirtualTrip param)
        {
            throw new NotImplementedException();
        }
    }
}

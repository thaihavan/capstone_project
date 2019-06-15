using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Helpers;
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

        public VirtualTripRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
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
            return _virtualTrips.Find(v => true).ToList();
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
            return _virtualTrips.Find(v => v.Id.Equals(new BsonObjectId(id))).FirstOrDefault();
        }

        public VirtualTrip Update(VirtualTrip param)
        {
            var filter = Builders<VirtualTrip>.Filter.Eq(v => v.Id, param.Id);
            var relult = _virtualTrips.ReplaceOne(filter, param);
            if (!relult.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public IEnumerable<VirtualTrip> GetAllVirtualTripWithPost()
        {
            var virtualTrips = from v in _virtualTrips.AsQueryable()
                           join p in _post.AsQueryable() on v.PostId equals p.Id into joined
                           from post in joined
                           select new VirtualTrip
                           {
                               Id = v.Id,
                               PostId = v.PostId,
                               Items = v.Items,
                               Post = post
                           };
            return virtualTrips.ToList();
        }
    }
}

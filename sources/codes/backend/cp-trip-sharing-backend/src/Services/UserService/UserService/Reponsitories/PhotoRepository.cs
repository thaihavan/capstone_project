using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServices.Helpers;
using UserServices.Reponsitories.DbContext;
using MongoDB.Bson;
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMongoCollection<Photo> _photos = null;

        public PhotoRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _photos = dbContext.PhotoCollection;
        }

        public Photo Add(Photo document)
        {
            _photos.InsertOne(document);
            return document;
        }

        public Photo Delete(Photo document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetAll(string userId)
        {
            List<Photo> photos = _photos.Find(temp => temp.Author.Equals(new BsonObjectId(ObjectId.Parse(userId)))).ToList();
            return photos;
        }

        public Photo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Photo Update(Photo document)
        {
            throw new NotImplementedException();
        }
    }
}

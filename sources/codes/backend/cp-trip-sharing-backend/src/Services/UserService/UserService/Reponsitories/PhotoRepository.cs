using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserServices.Helpers;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private readonly IMongoCollection<Photo> _photos = null;

        public PhotoRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDBContext(settings);
            _photos = dbContext.PhotosCollection;
        }

        public bool Add(Photo document)
        {
            _photos.InsertOne(document);
            return true;
        }

        public bool Delete(Photo document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Photo> GetAll(string id)
        {
            List<Photo> photos = _photos.Find(temp => temp.Author.Equals(id)).ToList();
            return photos;
        }

        public Photo GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Photo document)
        {
            throw new NotImplementedException();
        }
    }
}

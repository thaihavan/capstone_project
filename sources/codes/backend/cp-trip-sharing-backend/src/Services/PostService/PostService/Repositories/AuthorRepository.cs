using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.DbContext;
using PostService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMongoCollection<Author> _authors = null;

        public AuthorRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _authors = dbContext.Authors;
        }

        public Author Add(Author param)
        {
            _authors.InsertOne(param);
            return param;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public Author GetById(string id)
        {
            return _authors.Find(a => a.Id == id).FirstOrDefault();
        }

        public Author InsertOrUpdate(Author author)
        {
            var filter = Builders<Author>.Filter.Eq("_id", author.Id);
            var updateDefinition = Builders<Author>.Update
                .Set("display_name", author.DisplayName)
                .Set("profile_image", author.ProfileImage);

            var result = _authors.UpdateOne(
                filter,
                updateDefinition, 
                new UpdateOptions { IsUpsert = true });

            if(!result.IsAcknowledged)
            {
                return null;
            }
            return author;
        }

        public Author Update(Author param)
        {
            throw new NotImplementedException();
        }
    }
}

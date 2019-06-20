using Microsoft.Extensions.Options;
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
            throw new NotImplementedException();
        }

        public Author Update(Author param)
        {
            throw new NotImplementedException();
        }
    }
}

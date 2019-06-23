using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;
using UserServices.Reponsitories.Interfaces;

namespace UserServices.Reponsitories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly IMongoCollection<Block> _blocks = null;

        public BlockRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _blocks = dbContext.BlockCollection;
        }

        public BlockRepository()
        { 
        }

        public Block Add(Block document)
        {
            _blocks.InsertOne(document);
            return document;
        }

        public Block Delete(Block document)
        {
            _blocks.DeleteOne(temp => temp.BlockedId.Equals(document.BlockedId) && temp.BlockerId.Equals(document.BlockerId));
            return document;
        }

        public Block GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Block Update(Block document)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Block> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories.DbContext;

namespace UserServices.Reponsitories
{
    public class BlockRepository : IRepository<Block>
    {
        private readonly IMongoCollection<Block> _blocks = null;

        public BlockRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDBContext(settings);
            _blocks = dbContext.BlockCollection;
        }

        public bool Add(Block document)
        {
            _blocks.InsertOne(document);
            return true;
        }

        public bool Delete(Block document)
        {
            return _blocks.DeleteOne(temp => temp.BlockedId.Equals(document.BlockedId) && temp.BlockerId.Equals(document.BlockerId)).IsAcknowledged;
        }

        public IEnumerable<Block> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public Block GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Block document)
        {
            throw new NotImplementedException();
        }
    }
}

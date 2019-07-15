using ApiGateway.Helpers;
using ApiGateway.Models;
using ApiGateway.Repositories.DbContext;
using ApiGateway.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Repositories
{
    public class BlacklistTokenRepository : IBlacklistTokenRepository
    {
        private readonly IMongoCollection<BlacklistToken> _blacklistTokens = null;

        public BlacklistTokenRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _blacklistTokens = dbContext.BlackListTokens;
        }

        public BlacklistToken Add(BlacklistToken param)
        {
            throw new NotImplementedException();
        }

        public async Task<BlacklistToken> AddAsync(string token)
        {
            await _blacklistTokens.InsertOneAsync(new BlacklistToken() { Token = token });
            return new BlacklistToken() { Token = token };
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public BlacklistToken Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlacklistToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BlacklistToken> GetTokenAsync(string token)
        {
            var result = await _blacklistTokens.FindAsync(x => x.Token.Equals(token)).Result.ToListAsync();
            return result.FirstOrDefault();
        }

        public BlacklistToken Update(BlacklistToken param)
        {
            throw new NotImplementedException();
        }
    }
}

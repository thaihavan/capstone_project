using EmailService.Helpers;
using EmailService.Models;
using EmailService.Repositories.DbContext;
using EmailService.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IMongoCollection<Email> _emails = null;

        public EmailRepository(IMongoCollection<Email> emails)
        {
            _emails = emails;
        }

        public EmailRepository(IOptions<AppSettings> settings)
        {
            var dbContext = new MongoDbContext(settings);
            _emails = dbContext.Emails;
        }

        public IEnumerable<Email> GetAll()
        {
            return _emails.Find(x => true).ToList();
        }

        public Email Add(Email param)
        {
            _emails.InsertOne(param);
            return param;
        }

        public Email Update(Email param)
        {
            var filter = Builders<Email>.Filter.Eq(e => e.Id, param.Id);
            var result = _emails.ReplaceOne(filter, param);
            if (!result.IsAcknowledged)
            {
                return null;
            }
            return param;
        }

        public bool Delete(string id)
        {
            var filter = Builders<Email>.Filter.Eq(a => a.Id, new BsonObjectId(new ObjectId(id)));
            return _emails.DeleteOne(filter).IsAcknowledged;
        }
    }
}

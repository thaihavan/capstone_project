//using IdentityProvider.Helpers;
//using Microsoft.Extensions.Options;
//using IdentityProvider.Repositories;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using IdentityProvider.Models;
//using System.Threading.Tasks;

//namespace IdentityProvider.Test
//{
//    [TestFixture]
//    public class BlacklistTokenRepositoryTest
//    {
//        BlacklistTokenRepository _blacklistTokenRepository = null;
//        [SetUp]
//        public void config()
//        {
//            var _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-Identity"
//            };
//            _blacklistTokenRepository = new BlacklistTokenRepository(Options.Create(_setting));
//        }

//        [TestCase]
//        public void TestAddAsync() {
//            string token = "token fake test";
//            Task<BlacklistToken> blacklistToken = _blacklistTokenRepository.AddAsync(token);
//            Assert.IsNotNull(blacklistToken);
//        }

//        [TestCase]
//        public void TestGetTokenAsync()
//        {
//            string token = "token fake test";
//            Task<BlacklistToken> blacklistToken = _blacklistTokenRepository.GetTokenAsync(token);
//            Assert.IsNotNull(blacklistToken);
//        }
//    }
//}

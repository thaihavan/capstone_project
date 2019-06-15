using IdentityProvider.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityProvider.Helpers;
using Microsoft.Extensions.Options;

namespace IdentityProvider.Test
{
    [TestFixture]
    public class Test
    {
        AccountRepository _accountRepository = null;
        [SetUp]
        public void config()
        {
            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-Identity"
            };
            _accountRepository = new AccountRepository(Options.Create(_setting));
        }


        [TestCase]
        public void test()
        {
            int number = 3;
            Assert.AreEqual(number,3);
        }

        [TestCase]
        public void TestGet()
        {
            
            Models.Account account = _accountRepository.Get("5d027ea59b358d247cd21a55");
            Assert.IsNull(account);

        }
    }
}

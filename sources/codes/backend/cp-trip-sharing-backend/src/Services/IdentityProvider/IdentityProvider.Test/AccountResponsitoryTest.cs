//using IdentityProvider.Models;
//using IdentityProvider.Repositories;
//using IdentityProvider.Repositories.DbContext;
//using MongoDB.Driver;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using IdentityProvider.Helpers;
//using Microsoft.Extensions.Options;

//namespace IdentityProvider.Test
//{
//    [TestFixture]
//    public class AccountResponsitoryTest
//    {
//        AccountRepository _accountRepository = null;
//        [SetUp]
//        public void config()
//        {
//            var _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-Identity"
//            };
//            _accountRepository = new AccountRepository(Options.Create(_setting));
//        }

//        [TestCase]
//        public void TestAddTrue()
//        {
//            Account acc = new Account() {
//                Email = "phongtv@gmail.com",
//                Password = "123456789",
//                Id = "5d111299f3b75e0001f4ed73",
//                PasswordSalt = "passwordtest",
//                Role = "memeber",
//                UserId = "5d111299f3b75e0001f4ed74",
//                Token = "TokenTest" 
//            };
//            Account account = _accountRepository.Add(acc);
//            Assert.IsNotNull(account);
//        }

//        [TestCase]
//        public void TestDelete()
//        {            
//            Assert.AreEqual(true,true);
//        }


//        [TestCase]
//        public void TestGetTrue()
//        {
//            Account account = _accountRepository.Get("5d111299f3b75e0001f4ed73");
//            Assert.IsNotNull(account);
//        }

//        [TestCase]
//        public void TestGetFalse()
//        {
//            Account account = _accountRepository.Get("5d027f10de896f17a87b1044");
//            Assert.IsNull(account);
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            IEnumerable<Account> accounts = _accountRepository.GetAll();
//            Assert.IsNotNull(accounts);
//        }

//        [TestCase]
//        public void TestUpdate()
//        {
//            Account acc = new Account() {
//                Id = "5d111299f3b75e0001f4ed73",
//                Email = "phongtv@gmail.com",
//                Password = "12345678910",
//                PasswordSalt = "PasswordSaltTest",
//                Role = "member",
//                Token = "TokenTest",
//                UserId = "5d111299f3b75e0001f4ed74"
//            };
//           Account account = _accountRepository.Update(acc);
//            Assert.IsNotNull(account);
//        }


//        [TestCase]
//        public void TestGetByEmailTrue()
//        {
//            Account acc = _accountRepository.GetByEmail("phongtv@gmail.com");
//            Assert.IsNotNull(acc);
//        }

//        public void TestGetByEmailFalse()
//        {
//            Account acc = _accountRepository.GetByEmail("linhl111p@fpt.edu.vn");
//            Assert.IsNull(acc);
//        }

//    }
//}

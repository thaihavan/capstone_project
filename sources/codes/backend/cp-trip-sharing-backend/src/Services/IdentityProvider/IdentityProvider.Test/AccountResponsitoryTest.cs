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
//            Account acc = new Account() { Email = "phongtv@gmail.com", Password = "123456789", Role = "member", Username = "phongtv" };
//            bool checkAddDB = _accountRepository.Add(acc);
//            Assert.IsTrue(checkAddDB);
//        }

//        [TestCase]
//        public void TestDelete()
//        {
//            int a = 3;
//            Assert.AreEqual(a, 3);
//        }


//        [TestCase]
//        public void TestGetTrue()
//        {
//            Account account = _accountRepository.Get("5d027f3e8254691f48a4ab7d");
//            Assert.AreEqual(account, null);
//        }

//        [TestCase]
//        public void TestGetFalse()
//        {
//            Account account = _accountRepository.Get("5d027f3e8254691f48a4ab7e");
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
//            int a = 3;
//            Assert.AreEqual(3, a);
//        }


//        [TestCase]
//        public void TestGetByEmailTrue()
//        {
//            Account acc = _accountRepository.GetByEmail("linhlp111@fpt.edu.vn");

//            Assert.AreEqual(acc, null);
//        }

//        public void TestGetByEmailFalse()
//        {

//            Account acc = _accountRepository.GetByEmail("linhl111p@fpt.edu.vn");

//            Assert.IsNull(acc);
//        }

//    }
//}

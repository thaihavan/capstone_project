using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Repositories;
using IdentityProvider.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using IdentityProvider.Repositories.Interfaces;
using IdentityProvider.Services.Interfaces;

namespace IdentityProvider.Test
{
    [TestFixture]
    public class AccountServiceTest
    {
        Mock<IAccountRepository> moqIaccountservice;
        Mock<IPublishToTopic> moqIpublishtotopic;
        AppSettings _setting;
        [SetUp]
        public void config()
        {
            _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-Identity"
            };
            moqIaccountservice = new Mock<IAccountRepository>();
            moqIpublishtotopic = new Mock<IPublishToTopic>();
        }

        [TestCase]
        public void TestAuthenticate()
        {
            Account acc = new Account() { UserId ="5d027f3e8254691f48a4ab7c", Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = "5d027f3e8254691f48a4ab7d" };
            moqIaccountservice.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account result = testService.Authenticate("abc@gmail..com", "new_password");
            Assert.IsNotNull(result);
        }

        [TestCase]
        public void TestGetAll()
        {
            Account acc = new Account() { Email = "phongtv@gmail.com", Password = "123456789", Role = "member", PasswordSalt = "asvfav", Token = "asvaanbfbsgnnsn" };
            List<Account> accounts = new List<Account>();
            accounts.Add(acc);
            moqIaccountservice.Setup(x => x.GetAll()).Returns(accounts);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            IEnumerable<Account> resultaccounts = testService.GetAll();
            Assert.IsNotNull(resultaccounts);
        }

        [TestCase]

        public void TestRegister()
        {
            Account accNull = null;
            Account acc = new Account() { Email = "linhlppp@fpt.edu.vn", Password = "125436458569679" };
            moqIaccountservice.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(accNull);
            moqIaccountservice.Setup(x => x.Add(It.IsAny<Account>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account accResult = testService.Register(acc);
            Assert.AreEqual(acc.Email, accResult.Email);
        }

        [TestCase]

        public void TestRegisterIfreturnNull()
        {
            Account acc = new Account() { Email = "linhlppp@fpt.edu.vn", Password = "125436458569679"};
            moqIaccountservice.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account accResult = testService.Register(acc);
            Assert.IsNull(accResult);
        }

        [TestCase]

        public void TestChangePassword()
        {
            Account acc = new Account() { UserId = "5d027f3e8254691f48a4ab7c", Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = "5d027f3e8254691f48a4ab7d" };
            moqIaccountservice.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            bool changePassResult = testService.ChangePassword("abc@gmail.com", "new_password", "123456789");
            Assert.IsFalse(changePassResult);
        }

        [TestCase]

        public void TestGetResetPasswordToken()
        {
            Account acc = new Account()
            {
                UserId = "5d027f10de896f17a87b1044",
                Email = "linhlp2@fpt.edu.vn",
                Password = "UUFLBkIWy9AeELxK3fi3smamVNdCmeJX4LWHPQM/3X4=",
                Role = "member",
                PasswordSalt = "LQ993XXKV5Eo6/IBmoytuQ==",
                Token = "asvaanbfbsgnnsn",
                Id = "5d027f10de896f17a87b1045"
            };
            moqIaccountservice.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string tokenResult = testService.GetResetPasswordToken("abc@gmail.com");
            Assert.AreNotEqual(tokenResult, "");
        }

        [TestCase]
        public void TestRandomPassword()
        {
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string passwordRandomResult = testService.GenerateRandomPassword();
            Assert.AreNotEqual(passwordRandomResult, "");
        }

        [TestCase]

        public void TestResetPassword()
        {
            Account acc = new Account() { UserId = "5d027f10de896f17a87b1044",
                Email = "linhlp2@fpt.edu.vn",
                Password = "UUFLBkIWy9AeELxK3fi3smamVNdCmeJX4LWHPQM/3X4=",
                Role = "member", PasswordSalt = "LQ993XXKV5Eo6/IBmoytuQ==",
                Token = "asvaanbfbsgnnsn",
                Id = "5d027f10de896f17a87b1045" };
            moqIaccountservice.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIaccountservice.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            bool checkResetPassword = testService.ResetPassword("", "123456789");
            Assert.IsFalse(checkResetPassword);
        }
    }
}

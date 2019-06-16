//using IdentityProvider.Helpers;
//using IdentityProvider.Models;
//using IdentityProvider.Repositories;
//using IdentityProvider.Services;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.Options;
//using MongoDB.Bson;
//using IdentityProvider.Repositories.Interfaces;

//namespace IdentityProvider.Test
//{
//    [TestFixture]
//    public class AccountServiceTest
//    {
//        Mock<IAccountRepository> moqResult;
//        AppSettings _setting;
//        [SetUp]
//        public void config()
//        {
//            _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-Identity"
//            };
//            moqResult = new Mock<IAccountRepository>();
//        }

//        [TestCase]
//        public void TestAuthenticate()
//        {
//            Account acc = new Account() { UserId = ObjectId.Parse("5d027f3e8254691f48a4ab7c"), Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", Username = "linhlp", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = ObjectId.Parse("5d027f3e8254691f48a4ab7d") };
//            moqResult.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            Account result = testService.Authenticate("abc@gmail..com", "new_password");
//            Assert.IsNotNull(result);
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            Account acc = new Account() { Email = "phongtv@gmail.com", Password = "123456789", Role = "member", Username = "phongtv", PasswordSalt = "asvfav", Token = "asvaanbfbsgnnsn" };
//            List<Account> accounts = new List<Account>();
//            accounts.Add(acc);
//            moqResult.Setup(x => x.GetAll()).Returns(accounts);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            IEnumerable<Account> resultaccounts = testService.GetAll();
//            Assert.IsNotNull(resultaccounts);
//        }

//        [TestCase]

//        public void TestRegister()
//        {
//            Account accNull = null;
//            Account acc = new Account() { Email = "phongtv@gmail.com", Password = "123456789" };
//            moqResult.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(accNull);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            Account accResult = testService.RegisterAsync(acc);
//            Assert.AreEqual(acc.Email, accResult.Email);
//        }

//        [TestCase]

//        public void TestChangePassword()
//        {
//            Account acc = new Account() { UserId = ObjectId.Parse("5d027f3e8254691f48a4ab7c"), Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", Username = "linhlp", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = ObjectId.Parse("5d027f3e8254691f48a4ab7d") };
//            moqResult.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            bool changePassResult = testService.ChangePassword("abc@gmail.com", "new_password", "123456789");
//            Assert.IsFalse(changePassResult);
//        }

//        [TestCase]

//        public void TestGetResetPasswordToken()
//        {
//            Account acc = new Account() { UserId = ObjectId.Parse("5d027f3e8254691f48a4ab7c"), Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", Username = "linhlp", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = ObjectId.Parse("5d027f3e8254691f48a4ab7d") };
//            moqResult.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            string tokenResult = testService.GetResetPasswordTokenAsync("abc@gmail.com");
//            Assert.AreNotEqual(tokenResult, "");
//        }

//        [TestCase]
//        public void TestRandomPassword()
//        {
//            AccountService accService = new AccountService(Options.Create(_setting));
//            string passwordRandomResult = accService.GenerateRandomPassword();
//            Assert.AreNotEqual(passwordRandomResult, "");
//        }

//        [TestCase]

//        public void TestResetPassword()
//        {
//            Account acc = new Account()
//            {
//                UserId = ObjectId.Parse("5d027f3e8254691f48a4ab7c"),
//                Email = "linhlp4@fpt.edu.vn",
//                Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=",
//                Role = "member",
//                Username = "linhlp",
//                PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==",
//                Token = "asvaanbfbsgnnsn",
//                Id = ObjectId.Parse("5d027f3e8254691f48a4ab7d")
//            };
//            moqResult.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
//            var testService = new AccountService(moqResult.Object, Options.Create(_setting));
//            bool checkResetPassword = testService.ResetPassword("", "123456789");
//            Assert.IsFalse(checkResetPassword);
//        }
//    }
//}

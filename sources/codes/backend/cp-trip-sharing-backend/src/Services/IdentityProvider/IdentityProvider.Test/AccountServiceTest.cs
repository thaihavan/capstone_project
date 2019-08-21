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
        Mock<IAccountRepository> moqIAccountRepository;
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
            moqIAccountRepository = new Mock<IAccountRepository>();
            moqIpublishtotopic = new Mock<IPublishToTopic>();
        }

        [TestCase]
        public void TestAuthenticate()
        {
            Account acc = new Account() { UserId = "5d027f3e8254691f48a4ab7c", Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = "5d027f3e8254691f48a4ab7d" };
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account result = testService.Authenticate("abc@gmail.com", "new_password");
            Assert.IsNotNull(result);
        }

        [TestCase]
        public void TestAuthenticateReturnNull()
        {
            Account acc = null;
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account result = testService.Authenticate("abc@gmail.com", "new_password");
            Assert.IsNull(result);
        }

        [TestCase]
        public void TestGetAll()
        {
            Account acc = new Account() { Email = "phongtv@gmail.com", Password = "123456789", Role = "member", PasswordSalt = "asvfav", Token = "asvaanbfbsgnnsn" };
            List<Account> accounts = new List<Account>();
            accounts.Add(acc);
            moqIAccountRepository.Setup(x => x.GetAll()).Returns(accounts);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            IEnumerable<Account> resultaccounts = testService.GetAll();
            Assert.IsNotNull(resultaccounts);
        }

        [TestCase]

        public void TestRegister()
        {
            Account accNull = null;
            Account acc = new Account() { Email = "linhlppp@fpt.edu.vn", Password = "125436458569679" };
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(accNull);
            moqIAccountRepository.Setup(x => x.Add(It.IsAny<Account>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account accResult = testService.Register(acc);
            Assert.AreEqual(acc.Email, accResult.Email);
        }

        [TestCase]

        public void TestRegisterReturnNull()
        {
            Account acc = new Account() { Email = "linhlppp@fpt.edu.vn", Password = "125436458569679" };
            Account account = new Account();
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Add(It.IsAny<Account>())).Returns(account);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            Account accResult = testService.Register(acc);
            Assert.IsNull(accResult);
        }

        [TestCase]

        public void TestChangePassword()
        {
            Account acc = new Account() { UserId = "5d027f3e8254691f48a4ab7c", Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = "5d027f3e8254691f48a4ab7d" };
            moqIAccountRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Update(It.IsAny<Account>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            bool changePassResult = testService.ChangePassword("abc@gmail.com", "new_password", "123456789");
            Assert.IsTrue(changePassResult);
        }

        [TestCase]

        public void TestChangePasswordReturnFalse()
        {
            Account acc = new Account() { UserId = "5d027f3e8254691f48a4ab7c", Email = "linhlp4@fpt.edu.vn", Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=", Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==", Token = "asvaanbfbsgnnsn", Id = "5d027f3e8254691f48a4ab7d" };
            moqIAccountRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Update(It.IsAny<Account>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            bool changePassResult = testService.ChangePassword("abc@gmail.com", "newpassword", "123456789");
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
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string tokenResult = testService.GetResetPasswordToken("abc@gmail.com");
            Assert.AreNotEqual(tokenResult, "");
        }

        [TestCase]
        public void TestGenerateRandomPassword()
        {
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string passwordRandomResult = testService.GenerateRandomPassword();
            Assert.AreNotEqual(passwordRandomResult, "");
        }

        [TestCase]

        public void TestResetPassword()
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
            moqIAccountRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            moqIAccountRepository.Setup(x => x.Update(It.IsAny<Account>())).Returns(acc);
            bool checkResetPassword = testService.ResetPassword("", "123456789");
            Assert.IsTrue(checkResetPassword);
        }

        [TestCase]
        public void TestVerifyEmail()
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
            moqIAccountRepository.Setup(x => x.Get(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Update(It.IsAny<Account>())).Returns(acc);
            var testService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            bool result = testService.VerifyEmail("5d027f10de896f17a87b1044");
            Assert.IsTrue(result);
        }

        [TestCase]
        public void TestGetGoogleUserInformation()
        {
            var accountService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVkMGExNzcwMWEwYTQyMDAwMTdkZTZjOCIsInJvbGUiOiJtZW1iZXIiLCJ1c2VyX2lkIjoiNWQwYTE3NzAxYTBhNDIwMDAxN2RlNmM3IiwibmJmIjoxNTYyOTA2NjI3LCJleHAiOjE1NjI5MjgyMjcsImlhdCI6MTU2MjkwNjYyNywiaXNzIjoiYXV0aC50cmlwc2hhcmluZy5jb20ifQ.ou0CVGErEDI7DNGSpHsm3Q6aT5g8u2tVekWM9jklvY0";
            GoogleUser googleUser = accountService.GetGoogleUserInformation(token);
            Assert.IsNull(googleUser);
        }

        [TestCase]
        public void TestGoogleAuthenticate()
        {
            //Mock<IAccountService> moqaccountService = new Mock<IAccountService>();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVkMGExNzcwMWEwYTQyMDAwMTdkZTZjOCIsInJvbGUiOiJtZW1iZXIiLCJ1c2VyX2lkIjoiNWQwYTE3NzAxYTBhNDIwMDAxN2RlNmM3IiwibmJmIjoxNTYyOTA2NjI3LCJleHAiOjE1NjI5MjgyMjcsImlhdCI6MTU2MjkwNjYyNywiaXNzIjoiYXV0aC50cmlwc2hhcmluZy5jb20ifQ.ou0CVGErEDI7DNGSpHsm3Q6aT5g8u2tVekWM9jklvY0";
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
            GoogleUser googleUser = new GoogleUser()
            {
                Email = "phongtv@gmail.com",
                Gender = "Nam",
                Given_name = "Phongtv",
                Id = "5d027f10de896f17a87b104s",
                Name = "Tran Van Phong",
                Picture = ""
            };
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Add(It.IsAny<Account>())).Returns(acc);
            //moqaccountService.Setup(x => x.GetGoogleUserInformation(It.IsAny<string>())).Returns(googleUser);
            var accountService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            var account = accountService.GoogleAuthenticate(token);
            Assert.IsNull(account);
        }

        [TestCase]
        public void TestGetFacebookUserInformation()
        {
            var accountService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVkMGExNzcwMWEwYTQyMDAwMTdkZTZjOCIsInJvbGUiOiJtZW1iZXIiLCJ1c2VyX2lkIjoiNWQwYTE3NzAxYTBhNDIwMDAxN2RlNmM3IiwibmJmIjoxNTYyOTA2NjI3LCJleHAiOjE1NjI5MjgyMjcsImlhdCI6MTU2MjkwNjYyNywiaXNzIjoiYXV0aC50cmlwc2hhcmluZy5jb20ifQ.ou0CVGErEDI7DNGSpHsm3Q6aT5g8u2tVekWM9jklvY0";
            FacebookUser facebookUser = accountService.GetFacebookUserInformation(token);
            Assert.IsNull(facebookUser);
        }

        [TestCase]
        public void TestFacebookAuthenticate()
        {
            //Mock<IAccountService> moqaccountService = new Mock<IAccountService>();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVkMGExNzcwMWEwYTQyMDAwMTdkZTZjOCIsInJvbGUiOiJtZW1iZXIiLCJ1c2VyX2lkIjoiNWQwYTE3NzAxYTBhNDIwMDAxN2RlNmM3IiwibmJmIjoxNTYyOTA2NjI3LCJleHAiOjE1NjI5MjgyMjcsImlhdCI6MTU2MjkwNjYyNywiaXNzIjoiYXV0aC50cmlwc2hhcmluZy5jb20ifQ.ou0CVGErEDI7DNGSpHsm3Q6aT5g8u2tVekWM9jklvY0";
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
            FacebookUser googleUser = new FacebookUser()
            {
                Email = "phongtv@gmail.com",
                Id = "5d027f10de896f17a87b104s",
                Name = "Tran Van Phong"
            };
            moqIAccountRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).Returns(acc);
            moqIAccountRepository.Setup(x => x.Add(It.IsAny<Account>())).Returns(acc);
            //moqaccountService.Setup(x => x.GetGoogleUserInformation(It.IsAny<string>())).Returns(googleUser);
            var accountService = new AccountService(moqIAccountRepository.Object, Options.Create(_setting), moqIpublishtotopic.Object);
            var account = accountService.FacebookAuthenticate(token);
            Assert.IsNull(account);
        }
    }
}

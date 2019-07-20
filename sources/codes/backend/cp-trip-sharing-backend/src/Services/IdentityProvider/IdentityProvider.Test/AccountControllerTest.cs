using IdentityProvider.Controllers;
using IdentityProvider.Models;
using IdentityProvider.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Test
{
    [TestFixture]
    class AccountControllerTest
    {
        Mock<IAccountService> mockAccountService;
        Mock<ITokenManager> _tokenManager = null;
        Account acc = null;

       [SetUp]
        public void Config()
        {
           acc = new Account() {
                UserId = "5d027f3e8254691f48a4ab7c",
                Email = "linhlp4@fpt.edu.vn",
                Password = "ERatj0gngbZJh/tYJC4cHlVZBfLrT43vtIedGicemPk=",
                Role = "member", PasswordSalt = "hpyty+EEZw70tE8OqHN9Ow==",
                Token = "asvaanbfbsgnnsn",
                Id = "5d027f3e8254691f48a4ab7d"
            };
            mockAccountService = new Mock<IAccountService>();
            _tokenManager = new Mock<ITokenManager>();
        }

        [TestCase]
        public void TestAuthenticate()
        {
            mockAccountService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(acc);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            var accountReturn = accountController.Authenticate(acc);
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAuthenticateReturnBadRequest()
        {
            Account account = null;
            mockAccountService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(account);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            var accountReturn = accountController.Authenticate(acc);
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestRegister()
        {
            mockAccountService.Setup(x => x.Register(It.IsAny<Account>())).Returns(acc);
            mockAccountService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(acc);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            var accountReturn = accountController.Register(acc);
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestRegisterReturnBadRequest()
        {
            Account acccount = null;
            mockAccountService.Setup(x => x.Register(It.IsAny<Account>())).Returns(acccount);
            mockAccountService.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(acc);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult accountReturn = accountController.Register(acc);
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel()
            {
                CurrentPassword = "old_password",
                NewPassword = "new_password"
            };
            /*Chưa test*/
        }

        [TestCase]
        public void TestChangePasswordReturnBadRequest()
        {
            ChangePasswordModel model = new ChangePasswordModel()
            {
                CurrentPassword = "old_password",
                NewPassword = "new_password"
            };
            /*Chưa test*/
        }

        [TestCase]
        public void TestForgotPassword()
        {
            mockAccountService.Setup(x => x.GetResetPasswordToken(It.IsAny<string>())).Returns("asdsafasfafafaffsa");
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult accountReturn = accountController.ForgotPassword("phongtv@gmail.com");
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestVerifyEmailAddress()
        {

        }

        [TestCase]
        public void TestVerifyEmailAddressReturnBadRequest()
        {

        }
        
        [TestCase]
        public void TestResetPassword()
        {

        }

        [TestCase]
        public void TestResetPasswordReturnBadRequest()
        {

        }

        [TestCase]
        public void TestGoogleAuthenticate()
        {
            mockAccountService.Setup(x => x.GoogleAuthenticate(It.IsAny<string>())).Returns(acc);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult accountReturn = accountController.GoogleAuthenticate("phongtv@gmail.com");
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGoogleAuthenticateReturnBadRequest()
        {
            Account account = null;
            mockAccountService.Setup(x => x.GoogleAuthenticate(It.IsAny<string>())).Returns(account);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult accountReturn = accountController.GoogleAuthenticate("phongtv@gmail.com");
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }


        [TestCase]
        public void TestFacebookAuthenticate()
        {
            mockAccountService.Setup(x => x.FacebookAuthenticate(It.IsAny<string>())).Returns(acc);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult accountReturn = accountController.FacebookAuthenticate("phongtv@gmail.com");
            var type = accountReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestFacebookAuthenticateReturnBadRequest()
        {
            Account account = null;
            mockAccountService.Setup(x => x.FacebookAuthenticate(It.IsAny<string>())).Returns(account);
            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            IActionResult actionResult = accountController.FacebookAuthenticate("phongtv@gmail.com");
            var type = actionResult.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }

        [TestCase]
        public void TestLogout()
        {
            //var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            //Task<ActionResult> actionResult = accountController.Logout();
            //var type = actionResult.GetType();
            //Assert.AreEqual(type.Name, "AsyncStateMachineBox`1");
        }
    }
}

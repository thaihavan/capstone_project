using IdentityProvider.Controllers;
using IdentityProvider.Models;
using IdentityProvider.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        ChangePasswordModel changePasswordModel = null;
        ClaimsIdentity claims = null;
        ResetPasswordModel resetPasswordModel = null;

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
            changePasswordModel = new ChangePasswordModel()
            {
                CurrentPassword = "old_Password",
                NewPassword = "new_Password"
            };

            claims = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
               });

            resetPasswordModel = new ResetPasswordModel()
            {
                NewPassword = "new_password"
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
            var contextMock = new Mock<HttpContext>();    
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var changePasswordResult = _accountController.ChangePassword(changePasswordModel);
            var type = changePasswordResult.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestChangePasswordReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var changePasswordResult = _accountController.ChangePassword(changePasswordModel);
            var type = changePasswordResult.GetType();
            Assert.AreEqual(type.Name,"BadRequestObjectResult");
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
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.VerifyEmail(It.IsAny<string>())).Returns(true);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var verifyResult = _accountController.VerifyEmailAddress();
            var type = verifyResult.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestVerifyEmailAddressReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.VerifyEmail(It.IsAny<string>())).Returns(false);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var verifyResult = _accountController.VerifyEmailAddress();
            var type = verifyResult.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }
        
        [TestCase]
        public void TestResetPassword()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.ResetPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var verifyResult = _accountController.ResetPassword(resetPasswordModel);
            var type = verifyResult.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestResetPasswordReturnBadRequest()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAccountService.Setup(x => x.ResetPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var _accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            _accountController.ControllerContext.HttpContext = contextMock.Object;
            var verifyResult = _accountController.ResetPassword(resetPasswordModel);
            var type = verifyResult.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
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

            var accountController = new AccountController(mockAccountService.Object, _tokenManager.Object);
            var actionResult = accountController.Logout();
            Assert.IsNotNull(actionResult);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class LikeControllerTest
    {
        Mock<ILikeService> mockLikeService;
        Mock<IPostService> mockPostService;
        Like like = null;

        [SetUp]
        public void Config()
        {
           like = new Like()
           {
               ObjectId = "5d027ea59b358d247cd21a55",
               ObjectType = "post",
               UserId = "5d027ea59b358d247cd21a54",
               Date = DateTime.Now
           };
            
            mockLikeService = new Mock<ILikeService>();
            mockPostService = new Mock<IPostService>();
        }

        [TestCase]
        public void TestLike()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockLikeService.Setup(x => x.Add(It.IsAny<Like>())).Returns(like);
            var _likeController = new LikeController(mockLikeService.Object, mockPostService.Object);
            _likeController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult likeReturn = _likeController.Like(like);
            var type = likeReturn.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUnLike()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockLikeService.Setup(x => x.Delete(It.IsAny<Like>())).Returns(true);
            var _likeController = new LikeController(mockLikeService.Object, mockPostService.Object);
            _likeController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult likeReturn = _likeController.Unlike(like);
            var type = likeReturn.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }
    }
}

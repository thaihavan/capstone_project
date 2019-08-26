using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UserServices.Controllers;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserService.Test
{
    [TestFixture]
    class BlockControllerTest
    {
        Mock<IBlockService> mockBlockService;
        Block block = null;
        ClaimsIdentity claims = null;
        User user, userSecond = null;

        [SetUp]
        public void Config()
        {
            user = new User()
            {
                Id = "5d027ea59b358d247cd21aa3",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "",
                ContributionPoint  = 0,
                CreatedDate = DateTime.Now,
                DisplayName = "PhongTv",
                Dob = DateTime.Parse("02/01/1997"),
                FirstName = "Tran",
                FollowerCount = 0,
                FollowingCount = 34,
                Gender = true,
                Interested = null,
                IsFirstTime = false,
                LastName = "phong",
                UserName= "phongtv"
            };

            userSecond = new User()
            {
                Id = "5d027ea59b358d212o3iu456b",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "",
                ContributionPoint = 0,
                CreatedDate = DateTime.Now,
                DisplayName = "PhongTv",
                Dob = DateTime.Parse("02/01/1997"),
                FirstName = "Tran",
                FollowerCount = 0,
                FollowingCount = 34,
                Gender = true,
                Interested = null,
                IsFirstTime = false,
                LastName = "phong",
                UserName = "phongtv"
            };

            block = new Block()
            {
                Id = "5d027ea59b358d247cd21a55",
                BlockedId = "5d027ea59b358d247cd21a5x",
                BlockerId = "5d027ea59b358d247cd21a5s"
            };

            claims = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d027ea59b358d247cd51a2s")
               });

            mockBlockService = new Mock<IBlockService>();
        }

        

        [TestCase]
        public void TestAddBlock()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.Block(It.IsAny<Block>())).Returns(block);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addBlock = blockController.AddBlock("5d027ea59b358d247cd21a5x");
            var type = addBlock.GetType();
            Assert.AreEqual(type.Name,"OkResult");                
        }

        [TestCase]
        public void TestAddBlockReturnNotFound()
        {
            Block blockNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.Block(It.IsAny<Block>())).Returns(blockNull);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addBlock = blockController.AddBlock("5d027ea59b358d247cd21a5x");
            var type = addBlock.GetType();
            Assert.AreEqual(type.Name, "NotFoundResult");
        }

        [TestCase]
        public void TestUnBlockReturnNotFound()
        {
            Block blockNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.UnBlock(It.IsAny<Block>())).Returns(blockNull);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult unBlock = blockController.UnBlock("5d027ea59b358d247cd21a5x");
            var type = unBlock.GetType();
            Assert.AreEqual(type.Name, "NotFoundResult");
        }

        [TestCase]
        public void TestUnBlock()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.UnBlock(It.IsAny<Block>())).Returns(block);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult unBlock = blockController.UnBlock("5d027ea59b358d247cd21a5x");
            var type = unBlock.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestGetBlockedUsers()
        {
            IEnumerable<User> ienumableUser = new List<User>(){user,userSecond};           
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.GetBlockedUsers(It.IsAny<string>())).Returns(ienumableUser);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getBlockedUsers = blockController.GetBlockedUsers();
            var type = getBlockedUsers.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestGetBlockers()
        {
            IEnumerable<User> ienumableUser = new List<User>() { user, userSecond };
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockBlockService.Setup(x => x.GetBlockers(It.IsAny<string>())).Returns(ienumableUser);
            var blockController = new BlockController(mockBlockService.Object);
            blockController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getBlockedUsers = blockController.GetBlockers();
            var type = getBlockedUsers.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

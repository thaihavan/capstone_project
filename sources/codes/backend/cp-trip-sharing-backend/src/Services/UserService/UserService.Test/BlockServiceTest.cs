using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServices.Models;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services;

namespace UserService.Test
{
    [TestFixture]
    class BlockServiceTest
    {
        Mock<IBlockRepository> mockBlockRepository;
        Block block = null;
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

            mockBlockRepository = new Mock<IBlockRepository>();
        }

        [TestCase]
        public void TestBlock()
        {
            mockBlockRepository.Setup(x => x.Add(It.IsAny<Block>())).Returns(block);
            var blockService = new BlockService(mockBlockRepository.Object);
            Block blockReturn = blockService.Block(block);
            Assert.AreEqual(blockReturn, block);
        }

        [TestCase]
        public void TestGetBlockedUsers()
        {
            IEnumerable<User> ienumableUser = new List<User>() { user, userSecond };
            mockBlockRepository.Setup(x => x.GetBlockedUsers(It.IsAny<string>())).Returns(ienumableUser);
            var blockService = new BlockService(mockBlockRepository.Object);
            var getBlockedUsers = blockService.GetBlockedUsers("5d027ea59b358d247cd21a5s");
            User userActual = getBlockedUsers.FirstOrDefault();
            Assert.AreEqual(userActual, user);
        }

        [TestCase]
        public void TestGetBlockers()
        {
            IEnumerable<User> ienumableUser = new List<User>() { user, userSecond };
            mockBlockRepository.Setup(x => x.GetBlockers(It.IsAny<string>())).Returns(ienumableUser);
            var blockService = new BlockService(mockBlockRepository.Object);
            var getBlockedUsers = blockService.GetBlockers("5d027ea59b358d247cd21a5s");
            User userActual = getBlockedUsers.FirstOrDefault();
            Assert.AreEqual(userActual, user);
        }

        [TestCase]
        public void TestUnBlock()
        {
            mockBlockRepository.Setup(x => x.Delete(It.IsAny<Block>())).Returns(block);
            var blockService = new BlockService(mockBlockRepository.Object);
            Block blockActual = blockService.UnBlock(block);
            Assert.AreEqual(blockActual, block);
        }
    }
}

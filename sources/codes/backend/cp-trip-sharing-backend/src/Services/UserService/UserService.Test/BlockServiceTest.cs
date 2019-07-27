using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        User user = null;

        [SetUp]
        public void Config()
        {
            user = new User()
            {
                Id = "",
                AccountId = "",
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

            mockBlockRepository = new Mock<IBlockRepository>();
        }

        IEnumerable<User> ienumableUser()
        {
            yield return user;
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
            mockBlockRepository.Setup(x => x.GetBlockedUsers(It.IsAny<string>())).Returns(ienumableUser);
            var blockService = new BlockService(mockBlockRepository.Object);
            var getBlockedUsers = blockService.GetBlockedUsers("5d027ea59b358d247cd21a5s");
            Assert.IsNotEmpty(getBlockedUsers);
        }

        [TestCase]
        public void TestUnBlock()
        {
            mockBlockRepository.Setup(x => x.Delete(It.IsAny<Block>())).Returns(block);
            var blockService = new BlockService(mockBlockRepository.Object);
            Block blockReturn = blockService.UnBlock(block);
            Assert.IsNotNull(blockReturn);
        }
    }
}

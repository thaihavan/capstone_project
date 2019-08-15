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
    class FollowServiceTest
    {
        Mock<IFollowRepository> mockFollowRepository;
        Follow follow = null;
        User user = null;
        List<string> getAllFollowerId = new List<string>();
        List<string> getAllFollowingId = new List<string>();

        [SetUp]
        public void Config()
        {
            getAllFollowerId.Add("5d0a17701a0a4200017de6c7");
            getAllFollowerId.Add("5d0a17701a0a4200017de6cv");
            getAllFollowingId.Add("5d0a17701a0a4200017de6cv");
            getAllFollowingId.Add("5d0a17701a0a4200017de6c7");

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

            follow = new Follow()
            {
                Id = "5d2e0e4cbb113a00010368c3",
                Follower = "5d0a17701a0a4200017de6c7",
                Following = "5d111299f3b75e0001f4ed78"
            };

            mockFollowRepository = new Mock<IFollowRepository>();
        }

        public IEnumerable<User> ienumableUsers()
        {
            yield return user;
        }

        [TestCase]
        public void TestAddFollows()
        {
            mockFollowRepository.Setup(x => x.Add(It.IsAny<Follow>())).Returns(follow);
            var followService = new FollowService(mockFollowRepository.Object);
            Follow followReturn = followService.AddFollows(follow);
            Assert.AreEqual(followReturn.Follower,follow.Follower);
        }

        [TestCase]
        public void TestGetAllFollower()
        {
            mockFollowRepository.Setup(x => x.GetAllFollower(It.IsAny<string>())).Returns(ienumableUsers);
            var followService = new FollowService(mockFollowRepository.Object);
            IEnumerable<User> ienumableReturn = followService.GetAllFollower("5d111299f3b75e0001f4ed78");
            Assert.IsNotEmpty(ienumableReturn);
        }

        [TestCase]
        public void TestGetAllFollowerId()
        {
            mockFollowRepository.Setup(x => x.GetAllFollowerId(It.IsAny<string>())).Returns(getAllFollowerId);
            var followService = new FollowService(mockFollowRepository.Object);
            List<string> getAllFollowerIds = followService.GetAllFollowerId("5d111299f3b75e0001f4ed78");
            Assert.IsTrue(getAllFollowerIds.Count == 2);
        }

        [TestCase]
        public void TestGetAllFollowing()
        {
            mockFollowRepository.Setup(x => x.GetAllFollowing(It.IsAny<string>())).Returns(ienumableUsers);
            var followService = new FollowService(mockFollowRepository.Object);
            IEnumerable<User> getAllFollowing = followService.GetAllFollowing("5d111299f3b75e0001f4ed78");
            Assert.IsNotEmpty(getAllFollowing);
        }

        [TestCase]
        public void TestGetAllFollowingId()
        {
            mockFollowRepository.Setup(x => x.GetAllFollowingId(It.IsAny<string>())).Returns(getAllFollowingId);
            var followService = new FollowService(mockFollowRepository.Object);
            List<string> getAllFollowingIds = followService.GetAllFollowingId("5d111299f3b75e0001f4ed78");
            Assert.IsTrue(getAllFollowingIds.Count == 2);
        }

        [TestCase]
        public void TestIsFollowed()
        {
            mockFollowRepository.Setup(x => x.IsFollowed(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var followService = new FollowService(mockFollowRepository.Object);
            bool checkFolled = followService.IsFollowed("5d111299f3b75e0001f4ed78","5d111299f3b75e0001f4ed78");
            Assert.IsTrue(checkFolled);
        }

        [TestCase]
        public void TestUnfollow()
        {
            mockFollowRepository.Setup(x => x.Unfollow(It.IsAny<Follow>())).Returns(follow);
            var followService = new FollowService(mockFollowRepository.Object);
            Follow followReturn = followService.Unfollow(follow);
            Assert.AreEqual(followReturn,follow);
        }
    }
}

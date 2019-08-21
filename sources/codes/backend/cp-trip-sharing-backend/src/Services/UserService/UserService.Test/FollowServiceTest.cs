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
    class FollowServiceTest
    {
        Mock<IFollowRepository> mockFollowRepository;
        Follow follow = null;
        User user, userSecond = null;
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
                Id = "5d027ea59b358d247cd21aa3",
                AccountId = "5d027ea59b358d247cd12re78",
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
                AccountId = "5d027ea59b358d247cd12re12",
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
            IEnumerable<User> ienumableUsers = new List<User>() { user, userSecond };
            mockFollowRepository.Setup(x => x.GetAllFollower(It.IsAny<string>())).Returns(ienumableUsers);
            var followService = new FollowService(mockFollowRepository.Object);
            IEnumerable<User> ienumableReturn = followService.GetAllFollower("5d111299f3b75e0001f4ed78");
            User userActual = ienumableReturn.FirstOrDefault();
            Assert.AreEqual(userActual,user);
        }

        [TestCase]
        public void TestGetAllFollowerId()
        {
            mockFollowRepository.Setup(x => x.GetAllFollowerId(It.IsAny<string>())).Returns(getAllFollowerId);
            var followService = new FollowService(mockFollowRepository.Object);
            List<string> getAllFollowerIds = followService.GetAllFollowerId("5d111299f3b75e0001f4ed78");
            string followerIdFirstActual = getAllFollowerIds.FirstOrDefault();
            Assert.AreEqual(followerIdFirstActual, "5d0a17701a0a4200017de6c7");
        }

        [TestCase]
        public void TestGetAllFollowing()
        {
            IEnumerable<User> ienumableUsers = new List<User>() { user, userSecond };
            mockFollowRepository.Setup(x => x.GetAllFollowing(It.IsAny<string>())).Returns(ienumableUsers);
            var followService = new FollowService(mockFollowRepository.Object);
            IEnumerable<User> getAllFollowing = followService.GetAllFollowing("5d111299f3b75e0001f4ed78");
            User userActual = getAllFollowing.FirstOrDefault();
            Assert.AreEqual(userActual, user);
        }

        [TestCase]
        public void TestGetAllFollowingId()
        {
            mockFollowRepository.Setup(x => x.GetAllFollowingId(It.IsAny<string>())).Returns(getAllFollowingId);
            var followService = new FollowService(mockFollowRepository.Object);
            List<string> getAllFollowingIds = followService.GetAllFollowingId("5d111299f3b75e0001f4ed78");
            string followeingIdFirstActual = getAllFollowingIds.FirstOrDefault();
            Assert.AreEqual(followeingIdFirstActual, "5d0a17701a0a4200017de6cv");
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

using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServices.Models;
using UserServices.Reponsitories.Interfaces;
using UserServices.Services.Interfaces;

namespace UserService.Test
{
    [TestFixture]
    class UserServiceTest
    {
        Mock<IUserRepository> mockUserRepository;
        Mock<IPublishToTopic> mockPublishToTopic;
        User user,userSecond = null;
        StatisticsFilter statisticsFilter = null;

        [SetUp]
        public void Config()
        {
            user = new User()
            {
                Id = "5d300f07a346270001a5bef4",
                Active = true,
                Address = "Nam Dinh",
                Avatar = "https://storage.googleapis.com/trip-sharing-final-image-bucket/image-default-user-avatar.png",
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

            statisticsFilter = new StatisticsFilter()
            {
                From = DateTime.Parse("01/01/2019"),
                To = DateTime.Now
            };

            mockUserRepository = new Mock<IUserRepository>();
            mockPublishToTopic = new Mock<IPublishToTopic>();
        }      

        [TestCase]
        public void TestAdd()
        {
            mockUserRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(user);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            User userReturn = userService.Add(user);
            Assert.AreEqual(userReturn.DisplayName, "PhongTv");
        }

        [TestCase]
        public void TestAddReturnNull()
        {
            User user = null;
            mockUserRepository.Setup(x => x.Add(It.IsAny<User>())).Returns(user);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            User userReturn = userService.Add(user);
            Assert.IsNull(userReturn);
        }

        [TestCase]
        public void TestBanAnUser()
        {
            mockUserRepository.Setup(x => x.BanAnUser(It.IsAny<string>())).Returns(true);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            bool banAnUser = userService.BanAnUser("5d300f07a346270001a5bef4");
            Assert.IsTrue(banAnUser);
        }

        [TestCase]
        public void TestCheckUsername()
        {
            mockUserRepository.Setup(x => x.CheckUsername(It.IsAny<string>())).Returns(true);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            bool checkUsername = userService.CheckUsername("phongtv");
            Assert.IsTrue(checkUsername);
        }

        [TestCase]
        public void TestGetUserById()
        {
            mockUserRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(user);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            User userActual = userService.GetUserById("5d300f07a346270001a5bef2");
            Assert.AreEqual(userActual, user);
        }

        [TestCase]
        public void TestGetUsers()
        {
            IEnumerable<User> ienumerableUser = new List<User>() { user, userSecond };
            mockUserRepository.Setup(x => x.GetUsers(It.IsAny<string>(),It.IsAny<int>())).Returns(ienumerableUser);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            IEnumerable<User> ienumableReturn = userService.GetUsers("5d300f07a346270001a5bef2",6);
            User userActual = ienumableReturn.FirstOrDefault();
            Assert.AreEqual(userActual, user);
        }

        [TestCase]
        public void TestGetUserStatistics()
        {
            mockUserRepository.Setup(x => x.GetUserStatistics(It.IsAny<StatisticsFilter>())).Returns(statisticsFilter);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            var objectReturn = userService.GetUserStatistics(statisticsFilter);
            Assert.IsNotNull(objectReturn);
        }

        [TestCase]
        public void TestUnBanAnUser()
        {
            mockUserRepository.Setup(x => x.UnBanAnUser(It.IsAny<string>())).Returns(true);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            bool unbanUser = userService.UnBanAnUser("5d300f07a346270001a5bef2");
            Assert.IsTrue(unbanUser);
        }

        [TestCase]
        public void TestUpdate()
        {
            user.DisplayName = "PHONGTV UPDATE";
            mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).Returns(user);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            User userReturn = userService.Update(user);
            Assert.AreEqual(userReturn.DisplayName, "PHONGTV UPDATE");
        }       

        [TestCase]
        public void TestUpdateReturnNull()
        {
            User user = null;
            mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).Returns(user);
            var userService = new UserServices.Services.UserService(mockUserRepository.Object, mockPublishToTopic.Object);
            User userReturn = userService.Update(user);
            Assert.IsNull(userReturn);
        }
    }
}

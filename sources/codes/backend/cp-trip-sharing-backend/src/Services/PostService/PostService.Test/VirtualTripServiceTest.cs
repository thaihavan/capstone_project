using Moq;
using NUnit.Framework;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class VirtualTripServiceTest
    {
        Mock<IVirtualTripRepository> mockVirtualTripRepository;
        PostFilter postFilter = null;
        VirtualTrip virtualTrip = null;
        Post post = null;

        [SetUp]
        public void Config()
        {
            post = new Post()
            {
                Id = "5d07d847a2c5f845707dc69a",
                Content = "<p>Post Test</p>",
                AuthorId = "5d0b2b0b1c9d440000d8e9a1",
                CommentCount = 0,
                IsActive = true,
                IsPublic = true,
                LikeCount = 0,
                CoverImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png",
                PostType = "article",
                PubDate = DateTime.Now,
                liked = false,
                Title = "Post Test",
            };
            List<string> listTopics = new List<string>() { "5d247a04eff1030d7c5209a3" };
            postFilter = new PostFilter()
            {
                LocationId = "5sd239asdd8fass7",
                Search = "ha noi",
                TimePeriod = "Tuan qua",
                Topics = listTopics
            };
            virtualTrip = new VirtualTrip()
            {
                Id = "a6sfa7fafaf65fa58fa7f",
                PostId = "asf7f6af9sfafaf7af0aaf",
                Post = post
            };

            mockVirtualTripRepository = new Mock<IVirtualTripRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockVirtualTripRepository.Setup(x => x.Add(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripService.Add(virtualTrip);
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            mockVirtualTripRepository.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            bool isDelete = virtualTripService.Delete("asds7fafsfa5asfasf4");
            Assert.IsTrue(isDelete);
        }

        [TestCase]
        public void TestGetVirtualTrip()
        {
            mockVirtualTripRepository.Setup(x => x.GetVirtualTrip(It.IsAny<string>())).Returns(virtualTrip);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripService.GetVirtualTrip("asds7fafsfa5asfasf4");
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestGetById()
        {
            mockVirtualTripRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(virtualTrip);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripService.GetById("asds7fafsfa5asfasf4");
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            virtualTrip.Post.Content = "Update Content";
            mockVirtualTripRepository.Setup(x => x.Update(It.IsAny<VirtualTrip>())).Returns(virtualTrip);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripService.Update(virtualTrip);
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestGetAllVirtualTrips()
        {
            IEnumerable<VirtualTrip> virtualTrips = new List<VirtualTrip>()
            {
                virtualTrip
            };

            mockVirtualTripRepository.Setup(x => x.GetAllVirtualTrips(It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(virtualTrips);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            IEnumerable < VirtualTrip > virtualTripsReturn = null;
            virtualTripsReturn = virtualTripService.GetAllVirtualTrips(postFilter, 8);
            Assert.IsNotNull(virtualTripsReturn);
        }

        [TestCase]
        public void TestGetAllVirtualTripsByUser()
        {
            IEnumerable<VirtualTrip> virtualTrips = new List<VirtualTrip>()
            {
                virtualTrip
            };

            mockVirtualTripRepository.Setup(x => x.GetAllVirtualTripsByUser(It.IsAny<string>(), It.IsAny<PostFilter>(), It.IsAny<int>())).Returns(virtualTrips);
            var virtualTripService = new VirtualTripService(mockVirtualTripRepository.Object);
            IEnumerable<VirtualTrip> virtualTripsReturn = null;
            virtualTripsReturn = virtualTripService.GetAllVirtualTripsByUser("hfdd5dfggfd6dhff5df", postFilter, 8);
            Assert.IsNotNull(virtualTripsReturn);
        }
    }
}

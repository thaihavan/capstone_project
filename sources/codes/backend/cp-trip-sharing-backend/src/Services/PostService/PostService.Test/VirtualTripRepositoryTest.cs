using Microsoft.Extensions.Options;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class VirtualTripRepositoryTest
    {
        AppSettings _settings;
        VirtualTripRepository virtualTripRepository;
        VirtualTrip virtualTrip = null;
        Post post = null;
        PostFilter postFilter = null;

        [SetUp]
        public void Config()
        {
            _settings = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };

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
           
            virtualTrip = new VirtualTrip()
            {
                Id = "a6sfa7fafaf65fa58fa7f",
                PostId = "5d07d847a2c5f845707dc69a",
                Post = post
            };

            postFilter = new PostFilter()
            {
                TimePeriod = "this_week",
                Search = null,
                LocationId = null,
                Topics = null
            };

            virtualTripRepository = new VirtualTripRepository(Options.Create(_settings));
        }

        [TestCase]
        public void TestAdd()
        {
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripRepository.Add(virtualTrip);
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestDelete()
        {
            bool isDeleted = virtualTripRepository.Delete("a6sfa7fafaf65fa58fa7f");
            Assert.IsTrue(isDeleted);
        }

        [TestCase]
        public void TestGetAll()
        {
            IEnumerable<VirtualTrip> virtualTrips = null;
            virtualTrips = virtualTripRepository.GetAll();
            Assert.IsNotNull(virtualTrips);
        }

        [TestCase]
        public void TestGetById()
        {
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripRepository.GetById("a6sfa7fafaf65fa58fa7f");
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestUpdate()
        {
            virtualTrip.Post.Content = "Update Content Test";
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripRepository.Update(virtualTrip);
            Assert.AreEqual(virtualTripReturn.Post.Content, "Update Content Test");
        }

        //[TestCase]
        //public void TestUpdateReturnNull()
        //{
        //    VirtualTrip newVirtualTrip = new VirtualTrip()
        //    {
        //        Id = "5d247a04eff1030d7c5209a0"
        //    };
        //    VirtualTrip virtualTripReturn = virtualTripRepository.Update(newVirtualTrip);
        //    Assert.IsNull(virtualTripReturn.Post.Content);
        //}

        [TestCase]
        public void TestGetVirtualTrip()
        {
            VirtualTrip virtualTripReturn = null;
            virtualTripReturn = virtualTripRepository.GetVirtualTrip("5d2580821b385b0001597795");
            Assert.IsNotNull(virtualTripReturn);
        }

        [TestCase]
        public void TestGetAllVirtualTrips()
        {
            IEnumerable<VirtualTrip> iEnumerable = null;
            iEnumerable = virtualTripRepository.GetAllVirtualTrips(postFilter,2);
            Assert.IsEmpty(iEnumerable);
        }

        [TestCase]
        public void TestGetAllVirtualTripsByUser()
        {
            IEnumerable<VirtualTrip> iEnumerable = null;
            iEnumerable = virtualTripRepository.GetAllVirtualTripsByUser("5d0a17701a0a4200017de6c7", postFilter, 2);
            Assert.IsEmpty(iEnumerable);
        }

        [TestCase]
        public void TestGetVirtualTripStatistics()
        {
            StatisticsFilter statisticsFilter = new StatisticsFilter()
            {
                From = DateTime.Parse("10/7/2019"),
                To = DateTime.Now
            };
            object objectReturn  = virtualTripRepository.GetVirtualTripStatistics(statisticsFilter);
            Assert.IsNotNull(objectReturn);
        }


    }
}

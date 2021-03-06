﻿using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories.Interfaces;
using PostService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    public class AuthorServiceTest
    {
        AppSettings _setting;
        Mock<IAuthorRepository> mockAuthorRepository;
        Author author;

        [SetUp]
        public void Config()
        {
            author = new Author()
            {
                Id = "5d15941f197c3400015db0aa",
                DisplayName = "PhongTV",
                ProfileImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png"
            };

            _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            mockAuthorRepository = new Mock<IAuthorRepository>();
        }

        [TestCase]
        public void TestAdd()
        {
            mockAuthorRepository.Setup(x => x.Add(It.IsAny<Author>())).Returns(author);
            var authorService = new AuthorService( Options.Create(_setting), mockAuthorRepository.Object);
            Author authorReturn = authorService.Add(author);
            Assert.IsNotNull(authorReturn);
        }

        [TestCase]
        public void TestGetById()
        {
            mockAuthorRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns(author);
            var authorService = new AuthorService(Options.Create(_setting), mockAuthorRepository.Object);
            Author authorReturn = authorService.GetById("5d15941f197c3400015db0aa");
            Assert.IsNotNull(authorReturn);
        }
    }
}

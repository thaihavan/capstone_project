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
    public class AuthorRepositoryTest
    {
        AuthorRepository _authorRepository = null;
        Author author = null;

        [SetUp]
        public void Config()
        {
            author = new Author()
            {
                Id = "5d15941f197c3400015db0aa",
                DisplayName = "PhongTV",
                ProfileImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png"
            };

            var _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };
            _authorRepository = new AuthorRepository(Options.Create(_setting));
        }

        [TestCase]
        public void TestAdd()
        {
            Author authorReturn = _authorRepository.Add(author);
            Assert.IsNotNull(authorReturn);
        }

        [TestCase]
        public void TestGetById()
        {
            Author authorReturn = _authorRepository.GetById("5d15941f197c3400015db0aa");
            Assert.IsNotNull(authorReturn);
        }

        [TestCase]
        public void TestGetByIdReturnNull()
        {
            Author authorReturn = _authorRepository.GetById("5d15941f197c3400015db0ac");
            Assert.IsNull(authorReturn);
        }

        [TestCase]
        public void TestInsertOrUpdate()
        {
            author.DisplayName = "Tran Van Phong";
            Author authorReturn = _authorRepository.InsertOrUpdate(author);
            Assert.AreEqual(authorReturn, author);
        }
    }
}

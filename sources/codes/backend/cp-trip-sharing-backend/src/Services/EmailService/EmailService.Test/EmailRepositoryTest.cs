//using EmailService.Helpers;
//using EmailService.Repositories;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.Options;
//using EmailService.Models;

//namespace EmailService.Test
//{
//    [TestFixture]
//    public class EmailRepositoryTest
//    {
//        EmailRepository _emailRepository = null;

//        [SetUp]
//        public void Config()
//        {
//            var _setting = new AppSettings()
//            {
//                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
//                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
//                DatabaseName = "TripSharing-PostService"
//            };
//            _emailRepository = new EmailRepository(Options.Create(_setting));
//        }

//        [TestCase]
//        public void TestGetAll()
//        {
//            IEnumerable<Email> listEmails = _emailRepository.GetAll();
//            Assert.IsNotNull(listEmails);
//        }

//        [TestCase]
//        public void TestAdd()
//        {
//            Email email = new Email()
//            {
//                To = "phongtvse05048@fpt.edu.vn",
//                Date = new DateTime(),
//                EmailType = "EmailConfirm",
//                Subject = "Test Email Service",
//                Url = "google.com"
//            };
//            Email email_return = _emailRepository.Add(email);
//            Assert.AreEqual(email, email_return);
//        }

//        [TestCase]
//        public void TestAddFail()
//        {
//            Email email = new Email()
//            {
//                Id = "5d241ee6e5c99316e45aa54b",
//                To = "phongtvse05048@fpt.edu.vn",
//                Date = new DateTime(),
//                EmailType = "EmailConfirm",
//                Subject = "Test Email Service",
//                Url = "google.com"
//            };
//            Email email_return = _emailRepository.Add(email);
//            Assert.IsNull(email_return);
//        }

//        [TestCase]
//        public void TestUpdate()
//        {
//            Email email = new Email()
//            {
//                Id = "5d241ee6e5c99316e45aa54b",
//                To = "phongvpt1997@gmail.com",
//                Date = new DateTime(),
//                EmailType = "EmailConfirm",
//                Subject = "Test Email Change Email",
//                Url = "google.com"
//            };
//            Email email_return = _emailRepository.Update(email);
//            Assert.AreEqual(email_return,email);
//        }

//        //[TestCase]
//        //public void TestUpdateFail()
//        //{
//        //    Email email = new Email()
//        //    {
//        //        Id = "5d241ee6e5c99316e45aa54c",
//        //        To = "phongvpt1997@gmail.com",
//        //        Date = new DateTime(),
//        //        EmailType = "EmailConfirm",
//        //        Subject = "Test Email Change Email",
//        //        Url = "google.com"
//        //    };
//        //    Email email_return = _emailRepository.Update(email);
//        //    Assert.IsNull(email_return);
//        //}

//        [TestCase]
//        public void TestDelete()
//        {
//            string emailId = "5d241ee6e5c99316e45aa54b";
//            bool check = _emailRepository.Delete(emailId);
//            Assert.IsTrue(check);
//        }
//    }
//}

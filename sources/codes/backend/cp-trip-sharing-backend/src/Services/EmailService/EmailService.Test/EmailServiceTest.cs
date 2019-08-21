
using EmailService.Helpers;
using EmailService.Models;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Test
{
    [TestFixture]
    class EmailServiceTest
    {
        AppSettings _setting = null;
        Email email = null;
        [SetUp]
        public void Config()
        {
            _setting = new AppSettings()
            {
                Secret = "VGhpcyBpcyB0aGUgc2VjcmV0IGtleQ==",
                ConnectionString = "mongodb://tripsharing:tripsharing@cluster0-shard-00-00-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-01-vkzdk.gcp.mongodb.net:27017,cluster0-shard-00-02-vkzdk.gcp.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority",
                DatabaseName = "TripSharing-PostService"
            };

            email = new Email()
            {
                Date = DateTime.Now,
                EmailType = "EmailConfirm",
                To = "phongvpt1997@gmail.com",
                Url = "http://localhost:4200/email-confirm/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVkMGEwOTEwMWEwYTQyMDAwMTdkZTZjNCIsInJvbGUiOiJ1bnZlcmlmaWVkIiwidXNlcl9pZCI6IjVkMGEwOTEwMWEwYTQyMDAwMTdkZTZjMyIsIm5iZiI6MTU2MDkzODc2OCwiZXhwIjoxNTYwOTYwMzY4LCJpYXQiOjE1NjA5Mzg3NjgsImlzcyI6ImF1dGgudHJpcHNoYXJpbmcuY29tIn0.80kafHYCsAOuVh777sV0FKbFEaiM6qPR_wkussftZs4",
                Subject = "Test Email Service"
            };
        }

        [TestCase]
        public void TestSendEmailAsync()
        {
            var emailService = new EmailService.Services.EmailService(Options.Create(_setting));
            Task<HttpResponseMessage> sendEmailAsync =  emailService.SendEmailAsync(email);
            Assert.IsNotNull(sendEmailAsync);
        }


        [TestCase]
        public void TestSendEmailAsyncOtherEmailType()
        {
            email.EmailType = "EmailResetPassword";
            var emailService = new EmailService.Services.EmailService(Options.Create(_setting));
            Task<HttpResponseMessage> sendEmailAsync = emailService.SendEmailAsync(email);
            Assert.IsNotNull(sendEmailAsync);
        }
    }
}

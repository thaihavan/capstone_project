using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class UploadFileControllerTest
    {
        Mock<IUploadFileService> mockUploadFileService;
        ImageParam imageParam = null;

        [SetUp]
        public void Config()
        {
            imageParam = new ImageParam
            {
                Image = "base64",
                Type = "img/png"
            };

            mockUploadFileService = new Mock<IUploadFileService>();
        }

        [TestCase]
        public void TestUploadImage()
        {
            mockUploadFileService.Setup(x => x.UploadImage(imageParam));
            var uploadController = new UploadFileController(mockUploadFileService.Object);
            IActionResult actionResult = uploadController.UploadImage(imageParam);
            var type = actionResult.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestUploadImageReturnBadRequest()
        {
            imageParam.Type = null;
            mockUploadFileService.Setup(x => x.UploadImage(imageParam));
            var uploadController = new UploadFileController(mockUploadFileService.Object);
            IActionResult actionResult = uploadController.UploadImage(imageParam);
            var type = actionResult.GetType();
            Assert.AreEqual(type.Name, "BadRequestObjectResult");
        }
    }
}

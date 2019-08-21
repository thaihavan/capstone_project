using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UserServices.Controllers;
using UserServices.Models;
using UserServices.Services.Interfaces;

namespace UserService.Test
{
    [TestFixture]
    class PhotoControllerTest
    {
        Mock<IPhotoService> mockPhotoService;
        Photo photo = null;
        ClaimsIdentity claims = null;

        [SetUp]
        public void Config()
        {
            photo = new Photo()
            {
                Id = "5d0b233b1a0a4200017de6c9",
                Author = "Phongtv",
                Date = DateTime.Now,
                Url = "https://lakewangaryschool.sa.edu.au/wp-content/uploads/2017/11/placeholder-profile-sq.jpg"
            };

            claims = new ClaimsIdentity(new Claim[]
              {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d0b233b1a0a4200017de612")
              });

            mockPhotoService = new Mock<IPhotoService>();
        }
        
        [TestCase]
        public void TestGetAllPhoto()
        {

            IEnumerable<Photo> ienumablePhoto = new List<Photo>() { photo };
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPhotoService.Setup(x => x.GetAllPhoto(It.IsAny<string>())).Returns(ienumablePhoto);
            var photoController = new PhotoController(mockPhotoService.Object);
            photoController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult getAllPhoto = photoController.GetAllPhoto();
            var type = getAllPhoto.GetType();
            Assert.AreEqual(type.Name,"OkObjectResult");
        }

        [TestCase]
        public void TestAddPhoto()
        {
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPhotoService.Setup(x => x.AddPhoto(It.IsAny<Photo>())).Returns(photo);
            var photoController = new PhotoController(mockPhotoService.Object);
            photoController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addPhoto = photoController.AddPhoto("5d0b233b1a0a4200017de6cx", DateTime.Now);
            var type = addPhoto.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }

        [TestCase]
        public void TestAddPhotoReturnBadRequest()
        {
            Photo photoNull = null;
            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockPhotoService.Setup(x => x.AddPhoto(It.IsAny<Photo>())).Returns(photoNull);
            var photoController = new PhotoController(mockPhotoService.Object);
            photoController.ControllerContext.HttpContext = contextMock.Object;
            IActionResult addPhoto = photoController.AddPhoto("5d0b233b1a0a4200017de6cx", DateTime.Now);
            var type = addPhoto.GetType();
            Assert.AreEqual(type.Name, "BadRequestResult");
        }
    }
}

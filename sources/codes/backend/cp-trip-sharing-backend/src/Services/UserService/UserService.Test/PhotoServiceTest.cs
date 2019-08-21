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
    class PhotoServiceTest
    {
        Mock<IPhotoRepository> mockPhotoRepository;
        Photo photo = null;

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

            mockPhotoRepository = new Mock<IPhotoRepository>();
        }


        [TestCase]
        public void AddPhoto()
        {
            mockPhotoRepository.Setup(x => x.Add(It.IsAny<Photo>())).Returns(photo);
            var photoService = new PhotoService(mockPhotoRepository.Object);
            Photo photoReturn = photoService.AddPhoto(photo);
            Assert.AreEqual(photoReturn.Author,"Phongtv");
        }

        [TestCase]
        public void TestGetAllPhoto()
        {
            IEnumerable<Photo> ienumablePhoto = new List<Photo>() { photo };
            mockPhotoRepository.Setup(x => x.GetAll(It.IsAny<string>())).Returns(ienumablePhoto);
            var photoService = new PhotoService(mockPhotoRepository.Object);
            IEnumerable<Photo> ienumablePhotoReturn = photoService.GetAllPhoto("5d0b233b1a0a4200017de6c9");
            Photo photoActual = ienumablePhotoReturn.FirstOrDefault();
            Assert.AreEqual(photoActual,photo);
        }
    }
}

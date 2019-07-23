using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PostService.Controllers;
using PostService.Models;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class CommentControllerTest
    {
        Mock<ICommentService> mockCommentService;
        Mock<IAuthorService> mockAuthorService;
        Comment cmt = null;
        Author author = null;

        [SetUp]
        public void Config()
        {
            cmt = new Comment()
            {
                Id = "5d027ea59b358d247cd219a0",
                AuthorId = "5d15941f197c3400015db0aa",
                PostId = "5d027ea59b358d247cd219a2",
                Content = "day la test commentservice",
                Date = DateTime.Now,
                Active = true,
                Liked = false,
                LikeCount = 0
            };

            author = new Author()
            {
                Id = "5d15941f197c3400015db0aa",
                DisplayName = "PhongTV",
                ProfileImage = @"https://storage.googleapis.com/trip-sharing-final-image-bucket/image-201907131748509069-dy8beuyj1kfgwx98.png"
            };
            mockCommentService = new Mock<ICommentService>();
            mockAuthorService = new Mock<IAuthorService>();
        }

        [TestCase]
        public void TestGetCommentByPost()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });

            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
        }

        [TestCase]
        public void TestAddComment()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockAuthorService.Setup(x => x.GetById(It.IsAny<string>())).Returns(author);
            var _commenController = new CommentController(mockCommentService.Object, mockAuthorService.Object);
            _commenController.ControllerContext.HttpContext = contextMock.Object;
            var comment = _commenController.AddComment(cmt);
            Assert.IsNotNull(comment);
        }

        [TestCase]
        public void TestDelCommentReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockCommentService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var _commenController = new CommentController(mockCommentService.Object, mockAuthorService.Object);
            _commenController.ControllerContext.HttpContext = contextMock.Object;
            var comment = _commenController.DelComment("asfa5ffa4fafaf","afa5fafaf4aga4g");
            var type = comment.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestDelCommentSuccess()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","afa5fafaf4aga4g")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockCommentService.Setup(x => x.Delete(It.IsAny<string>())).Returns(true);
            var _commenController = new CommentController(mockCommentService.Object, mockAuthorService.Object);
            _commenController.ControllerContext.HttpContext = contextMock.Object;
            var comment = _commenController.DelComment("asfa5ffa4fafaf", "afa5fafaf4aga4g");
            var type = comment.GetType();
            Assert.AreEqual(type.Name, "OkResult");
        }

        [TestCase]
        public void TestUpdateCommentReturnUnauthorized()
        {
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","authorId")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockCommentService.Setup(x => x.Update(It.IsAny<Comment>())).Returns(cmt);
            var _commenController = new CommentController(mockCommentService.Object, mockAuthorService.Object);
            _commenController.ControllerContext.HttpContext = contextMock.Object;
            var comment = _commenController.UpdateComment(cmt);
            var type = comment.GetType();
            Assert.AreEqual(type.Name, "UnauthorizedResult");
        }

        [TestCase]
        public void TestUpdateCommentSuccess()
        {
            cmt.Content = "Update content";
            var contextMock = new Mock<HttpContext>();

            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "abc"),
                    new Claim(ClaimTypes.Role, "member"),
                    new Claim("user_id","5d15941f197c3400015db0aa")
                });
            contextMock.Setup(x => x.User).Returns(new ClaimsPrincipal(claims));
            mockCommentService.Setup(x => x.Update(It.IsAny<Comment>())).Returns(cmt);
            var _commenController = new CommentController(mockCommentService.Object, mockAuthorService.Object);
            _commenController.ControllerContext.HttpContext = contextMock.Object;
            var comment = _commenController.UpdateComment(cmt);
            var type = comment.GetType();
            Assert.AreEqual(type.Name, "OkObjectResult");
        }
    }
}

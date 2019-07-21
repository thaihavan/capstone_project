using Moq;
using NUnit.Framework;
using PostService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Test
{
    [TestFixture]
    class CommentControllerTest
    {
        Mock<ICommentService> mockCommentService;
        Mock<IAuthorService> mockAuthorService;

        [SetUp]
        public void Config()
        {
            mockCommentService = new Mock<ICommentService>();
            mockAuthorService = new Mock<IAuthorService>();
        }

    }
}

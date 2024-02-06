using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectSE.Controllers;
using System.Web.Mvc;
using ProjectSE.Models;
using Moq;
using System.Web;

using System.Collections.Specialized;
using System.Collections.Generic;

namespace UnitTestProjectSE.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);       
            Assert.AreEqual("Index",result.ViewName);
        }
       
       
            [TestMethod]
            public void MenuRepair_()
            {
                // Arrange
                var controller = new HomeController();

                // Mock the session
                var httpContextMock = new Mock<HttpContextBase>();
                var sessionMock = new Mock<HttpSessionStateBase>();
                httpContextMock.Setup(x => x.Session).Returns(sessionMock.Object);
                controller.ControllerContext = new ControllerContext { HttpContext = httpContextMock.Object };

                // Act
                var result = controller.MenuRepair();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                var viewResult = (ViewResult)result;
                Assert.IsNotNull(viewResult.Model);
                Assert.IsInstanceOfType(viewResult.Model, typeof(Repair));
            }
        

    }
}

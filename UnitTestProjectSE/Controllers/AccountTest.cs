using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectSE.Controllers;
using System;
using System.Web.Mvc;

namespace UnitTestProjectSE.Controllers
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void Login_Get_ReturnsView()
        {
            // Arrange
            var controller = new AccountsController();

            // Act
            var result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.ViewName);
        }

        [TestMethod]
        public void LogOff_ReturnsLoginView()
        {
            // Arrange
            var controller = new AccountsController();

            // Act
            var result = controller.LogOff() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }
    }
}

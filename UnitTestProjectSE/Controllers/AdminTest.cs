using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ProjectSE.Controllers;
using System.Web.Mvc;
using ProjectSE.Models;
using Moq;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace UnitTestProjectSE.Controllers
{
    
    [TestClass]
    public class AdminTest
    {
        DatabaseSEEntities db = new DatabaseSEEntities();

        [TestMethod]
        public void AddTech_ReturnsView()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.AddTech() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Reportdmin()
        {
            // Arrange
            var controller = new AdminController();

            // Act
            var result = controller.ReportAdmin() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

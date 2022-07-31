using System;
using System.Web.Mvc;
using ExamPortal.Controllers;
using ExamPortal.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExamPortal.Tests
{
    [TestClass]
    public class Coordinator
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            var controller = new HomeController();
            var view = controller.TeacherLogin() as ViewResult;
            var viewData = (TeacherLogin)controller.ViewData.Model;

            //Act
            viewData.username = "teacher1u";
            viewData.password = "teacher1p";

            //Assert
        }
    }
}

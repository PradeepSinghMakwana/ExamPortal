using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamPortal;
using ExamPortal.Controllers;
using ExamPortal.Models;

namespace ExamPortal.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        /*
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        */

        [TestMethod]
        public void AdminsLogin_Get()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var view = controller.AdminLogin() as ViewResult;

            //Assert
            Assert.IsNotNull(view);
        }
        
        [TestMethod]
        public void AdminsLogin_PostRedirectAdminDesk()
        {
            //Arrange
            var controller = new HomeController();
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.username = "admin1u";
            adminLogin.password = "admin1p";

            //Act
            var redirect = controller.AdminLogin(adminLogin) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Admins",redirect.RouteValues["controller"]);
        }

        [TestMethod]
        public void TeachersLogin_Get() {
            var controller = new HomeController();

            //Act
            var view = controller.TeacherLogin() as ViewResult;

            //Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        public async void TeachersLoginNotCoordinator_PostRedirectTeacherDesk()
        {
            var controller = new HomeController();
            TeacherLogin teacherLogin = new TeacherLogin();
            teacherLogin.username = "teacher2u";
            teacherLogin.password = "teacher2p";

            //Act
            var redirect = await controller.TeacherLogin(teacherLogin) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Teachers",redirect.RouteValues["controller"]);
        }

        [TestMethod]
        public void TeachersLoginIsCoordinator_PostRedirectCoordinatorDesk()
        {
            var controller = new HomeController();
            TeacherLogin teacherLogin = new TeacherLogin();
            teacherLogin.username = "teacher1u";
            teacherLogin.password = "teacher1p";

            //Act
            var redirect = controller.TeacherLogin() as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Coordinators",redirect.RouteValues["controller"]);
        }

        [TestMethod]
        public void StudentsLogin_Get()
        {
            //Arrange
            var controller = new HomeController();
            string sessionMessage = "";

            //Act
            var view = controller.StudentLogin(sessionMessage) as ViewResult;

            //Assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        public void StudentsLogin_PostRedirectStudentsDesk()
        {
            //Arrange
            var controller = new HomeController();
            StudentLogin studentLogin = new StudentLogin();
            studentLogin.username = "student1u";
            studentLogin.password = "student1p";

            //Act
            var redirect = controller.StudentLogin(studentLogin) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Students",redirect.RouteValues["controller"]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ExamPortal.Controllers;
using ExamPortal.Data;
using ExamPortal.Models;
using ExamPortal.Models.ViewModels;
using ExamPortal.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExamPortal.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetailsViewData()
        {
            //Arrange
            var controller = new AdminsController();
            var result = controller.Index() as ViewResult;

            //Act
            var product = (AdminDeskVM)result.ViewData.Model;
            List<Test> tests = product.TestAdminDesks;
            
            //Assert
            Assert.AreNotEqual(0, tests.Count);
        }
        [TestMethod]
        public void Test_AssignTest_Teachers() {
            //Arrange
            var controller = new TeachersController();
            var result = controller.listClasses() as ViewResult;
            var product = (List<Class>)result.ViewData.Model;
            //List<Tuple<string, int>> classes = product.listClassesITeach;
            
            //Assert
            Assert.AreNotEqual(0,product.Count);
        }
        /*[TestMethod]
        public void Test_IndexViewData_Coordinators()
        {
            //Arrange
            var controller = new CoordinatorsController();
            var result = controller.Index(4) as ViewResult;

            //Act
            var product = (CoordinatorVM)result.ViewData.Model;
            List<Student> students = product.studentsImCoordinating;

            //Assert
            Assert.AreNotEqual(0, students.Count);
        }*/

        [TestMethod]
        public async void Test_AddNewStudentLoginViewData_Coordinators()
        {
            //Arrange
            var controller = new CoordinatorsController();
            var result = await controller.AddNewStudentLogin() as ViewResult;

            //Act
            var product = (AddStudentLoginCoordinatorVM)result.ViewData.Model;

            //Assert
            Assert.AreNotEqual(0, product.password.Length);
        }

        [TestMethod]
        public async void Test_AddNewStudentLoginPostActionRedirect_Coordinators()
        {
            //Arrange
            var controller = new CoordinatorsController();
            AddStudentLoginCoordinatorVM product = new AddStudentLoginCoordinatorVM();
            Random random = new Random();
            product.password = "85651"+random.Next(0,1000);
            product.username = "V16R"+product.password.ToString()+"0";
            var result2 = await controller.AddNewStudentLogin(product) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", result2.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_GET_NewStudentLoginViewData_Home()
        {
            //Arrange
            var controller = new HomeController();
            var result = controller.StudentLogin("") as ViewResult;

            //Act
            var product = (StudentLogin)result.ViewData.Model;
            
            //Assert
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void Test_POST_NewStudentLoginRedirect_ActionName_Home()
        {
            //Arrange
            var controller = new HomeController();
            StudentLogin product = new StudentLogin();
            product.username = "V16R856510000";
            product.password = "85651";

            //Act
            var result2 = (RedirectToRouteResult)controller.StudentLogin(product);

            //Assert
            Assert.AreEqual("NewStudent", result2.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_NewStudent_LoginRedirect_UserId_Home()
        {
            //Arrange
            int? expected = 13;
            var controller = new HomeController();
            StudentLogin product = new StudentLogin();
            product.username = "V16R856510000";
            product.password = "85651";

            //Act
            var result2 = (RedirectToRouteResult)controller.StudentLogin(product);
            int? actual = (int?)result2.RouteValues["user_id"];

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async void Test_NewStudentViewData_SubjectsIn10th_Students()
        {
            //Arrange
            int? userId = 13;
            var stuController = new StudentsController();
            var result3 = await stuController.NewStudent(userId) as ViewResult;

            //Act
            var product2 = (CreateStudentVM)result3.ViewData.Model;

            //Assert
            Assert.AreNotEqual(0, product2.qualification_In_10s.Count);
        }

        [TestMethod]
        public async void Test_NewStudentViewData_SubjectsIn12th_Students()
        {
            //Arrange
            int? userId = 13;
            var stuController = new StudentsController();
            var result3 = await stuController.NewStudent(userId) as ViewResult;

            //Act
            var product2 = (CreateStudentVM)result3.ViewData.Model;

            //Assert
            Assert.AreNotEqual(0, product2.qualification_In_12s.Count);
        }
    }
}

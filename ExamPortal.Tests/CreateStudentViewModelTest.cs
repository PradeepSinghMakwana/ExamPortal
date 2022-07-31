using ExamPortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamPortal.Models.ViewModels;

namespace ExamPortal.Tests.Models
{
    [TestClass]
    public class CreateStudentViewModelTest
    {
        /*
        [TestMethod]
        public void Test_CreateStudentViewModel_listYearsByCourse_Given_Valid_Classes_ExpectNoErrors()
        {
            // Arrange
            ExamPortal.Models.Class class1 = TestModelHelper.MockClass();
            ExamPortal.Models.Class class2 = TestModelHelper.MockClass();

            class2.year = 2;

            List<int> expected = new List<int> { 1,2 };

            List<ExamPortal.Models.Class> classesList = new List<ExamPortal.Models.Class>() { class1, class2 };
            ExamPortal.Models.CreateStudentViewModel viewmodel = new ExamPortal.Models.CreateStudentViewModel();
            viewmodel.Classes = classesList;

            // Act
            List<int> actual = viewmodel.listYearsByCourse(class1.course_name);


            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        */
        /*
        [TestMethod]
        public void Test_CreateStudentViewModel_listAllCourses_Given_Valid_Courses_ExpectNoErrors()
        {
            // Arrange
            ExamPortal.Models.Course course1 = TestModelHelper.MockCourse();
            ExamPortal.Models.Course course2 = TestModelHelper.MockCourse();
            ExamPortal.Models.Course course3 = TestModelHelper.MockCourse();

            course2.course_name = "Btec-Mechanical";
            course3.course_name = "B.tec-Electrical";

            List<string> expected = new List<string> { "B.Com(CA)", "Btec-Mechanical", "B.tec-Electrical" };

            List<ExamPortal.Models.Course> coursesList = new List<ExamPortal.Models.Course>() { course1, course2, course3 };
            ExamPortal.Models.CreateStudentViewModel viewmodel = new ExamPortal.Models.CreateStudentViewModel();
            viewmodel.Courses = coursesList;

            // Act
            List<string> actual = viewmodel.listAllCourses;


            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        */
        [TestMethod]
        public void Validate_CoursesList_Given_Valid_CoursesList_ExpectNoValidationErrors()
        {
            // Arrange
            var model1 = TestModelHelper.MockCourse();
            var model2 = TestModelHelper.MockCourse();
            var model3 = TestModelHelper.MockCourse();
            model2.course_name = "Btec-Mechanical";
            model3.course_name = "B.tec-Electrical";
            List<object> modelsList = new List<object>() { model1, model2, model3 };

            // Act
            var results = TestModelHelper.Validate(modelsList);

            // Assert
            Assert.AreEqual(0, results.Count);
        }
        [TestMethod]
        public void Validate_CreateTeacher_Given_Valid_Teacher_ExpectNoValidationErrors() {
            // Arrange
            ExamPortal.Models.ViewModels.TeacherVM viewModel = TestModelHelper.MockTeacher();

            // Act
            var results = TestModelHelper.Validate(viewModel);

            // Assert
            Assert.AreEqual(0, results.Count);
        }
        /*
        [TestMethod]
        public void Validate_CreateCoordinator_Given_Valid_Coordinator_ExpectNoValidationErrors()
        {
            // Arrange
            TeacherVM viewModel = TestModelHelper.MockTeacher();

            // Act
            var results = TestModelHelper.Validate(viewModel);

            // Assert
            Assert.AreEqual(0, results.Count);
        }
        */
    }
}

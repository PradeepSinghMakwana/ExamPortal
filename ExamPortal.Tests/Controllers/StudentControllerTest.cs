using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ExamPortal.Controllers;
using ExamPortal.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExamPortal.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    { 
        [TestMethod]
        public async void NewStudent_Post()
        {
            //Arrange
            int? id = 13;
            var controller = new StudentsController();

            var view = await controller.NewStudent(id) as ViewResult;
            var vM = (CreateStudentVM)view.ViewData.Model;

            //Add marks in one subject in 10th qualification
            var QualificationIn10th = vM.qualification_In_10s.ToArray();
            for (int i=0;i<QualificationIn10th.Length;i++)
            {
                if (QualificationIn10th[i].subject_code == 1) {
                    QualificationIn10th[i].percent_marks = 0.4400M;
                }
            }
            vM.qualification_In_10s = new List<Qualification_In_10thVM>(QualificationIn10th);

            //Add marks in one subject in 12th qualification
            var QualificationIn12th = vM.qualification_In_12s.ToArray();
            for (int i = 0; i < QualificationIn12th.Length; i++)
            {
                if (QualificationIn12th[i].subject_code == 1)
                {
                    QualificationIn12th[i].has_subject = true;
                    QualificationIn12th[i].percent_marks = 0.4300M;
                }
            }
            vM.qualification_In_12s = new List<Qualification_In_12thVM>(QualificationIn12th);

            //add student profile details
            vM.unique_name = "Ankita Wangde";
            vM.dob = DateTime.Today.AddYears(-22);
            vM.fathers_name = "Rishikesh Wangde";
            vM.mothers_name = "Meena Wangde";
            vM.is_valid_student = false;
            vM.parents_phone = 9341868890;
            vM.residential_address = "H.no 32, Ralod Colony, Bas Nagar, Dewas";
            vM.permanent_address = "H.no 32, Ralod Colony, Bas Nagar, Dewas";
            vM.blood_group = "B+";
            vM.mobile = 7642582374;
            vM.enrollment_no = "V16R061100082";
            vM.adhar_no = 618287656786;

            //create an empty image
            vM.image_file = new MemoryFile(new FileStream(@"D:\ExamPortal\ExamPortalv2\ExamPortal\ExamPortal\Resources\TeachersPic\12.jpg", FileMode.Open),"images/jpeg", @"D:\ExamPortal\ExamPortalv2\ExamPortal\ExamPortal\Resources\TeachersPic\12.jpg");


            //Act
            var redirect = await controller.NewStudent(vM) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("StudentLogin",redirect.RouteValues["action"]);
        }

        /*[TestMethod]
        public async void Test_10thQualification() {
            //Arrange
            var controller = new StudentsController();

            int scholar_no = 11;

            var QualificationIn10th = new List<Qualification_In_10thVM>();
            for(int i = 1; i <= 5; i++)
            {
                QualificationIn10th.Add(new Qualification_In_10thVM(i,scholar_no, 1, 44.00M));
            }

            var QualificationIn12th = new List<Qualification_In_12thVM>();
            var qual12 = new Qualification_In_12thVM();
            qual12.scholar_no = scholar_no;
            qual12.has_subject = true;
            qual12.subject_code = 1;
            qual12.percent_marks = 43.00M;
            QualificationIn12th.Add(qual12);
            

            //Act
            bool result = await controller.Save10thN12thQualification(scholar_no, QualificationIn10th, QualificationIn12th);

            //Assert
            Assert.IsTrue(result);
            }*/
    }
}

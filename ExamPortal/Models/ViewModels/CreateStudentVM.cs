using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public partial class CreateStudentVM : Student
    {
        public CreateStudentVM(Student s) {
            classVM = new ClassVM(s.Class);
            Pretty_Class_name = classVM.Pretty_Class_name;
            scholar_no = s.scholar_no;
            unique_name = s.unique_name;
            parents_phone = s.parents_phone;
            mobile = s.mobile;
            photo = s.photo;
            enrollment_no = s.enrollment_no;
            residential_address = s.residential_address;
            permanent_address = s.permanent_address;
            fathers_name = s.fathers_name;
            mothers_name = s.mothers_name;
            adhar_no = s.adhar_no;
            blood_group = s.blood_group;
            dob = s.dob;
            Class = s.Class;
            class_id = s.class_id;
            user_id = s.user_id;
            UserLogin = s.UserLogin;
            is_valid_student = s.is_valid_student;
            age = DateTime.Today.Year - s.dob.Year;
        }
        public int age { get; set; }
        public ClassVM classVM { get; set; }
        public UserLogin userLogin { get; set; }
        public List<Qualification_In_10thVM> qualification_In_10s { get; set; }
        public List<Qualification_In_12thVM> qualification_In_12s { get; set; }
        public string Pretty_Class_name { get; set; }
        public HttpPostedFileBase image_file { get; set; }

        public Student extractStudent() {
            Student student = new Student();
            student.adhar_no = adhar_no;
            student.blood_group = blood_group;
            student.class_id = class_id;
            student.dob = dob;
            student.enrollment_no = enrollment_no;
            student.fathers_name = fathers_name;
            student.is_valid_student = is_valid_student;
            student.mobile = mobile;
            student.mothers_name = mothers_name;
            student.parents_phone = parents_phone;
            student.permanent_address = permanent_address;
            student.photo = "x";
            student.residential_address = residential_address;
            student.unique_name = unique_name;
            student.user_id = user_id;
            return student;
        }

        public List<Student_10th_Qualification> extractQualificationsIn10th()
        {
            var QuaIn10s = new List<Student_10th_Qualification>();
            qualification_In_10s.ForEach(item=>
                    QuaIn10s.Add(item.extractStudent_10th_Qualification())
                );
            return QuaIn10s;
        }

        public List<Student_12th_Qualification> extractQualificationsIn12th()
        {
            var QuaIn12s = new List<Student_12th_Qualification>();
            foreach (var item in qualification_In_12s) {
                var x = item.extractStudent_12th_Qualification();
                if (x != null) {
                    QuaIn12s.Add(x);
                }
            }
            return QuaIn12s;
        }
    }
}
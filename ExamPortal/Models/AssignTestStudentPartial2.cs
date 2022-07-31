using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public partial class AssignTestStudentPartial2
    {
        public int scholar_no { get; set; }
        public string photo { get; set; } 
        public string unique_name { get; set; }
        public string student_mobile { get; set; }
        public string parents_phone { get; set; }
        public string residential_address { get; set; }
        public string class_coordinator { get; set; }
        public IEnumerable<Student_Current_Qualification> student_current_qualifications { get; set; }
        
        public AssignTestStudentPartial2(Student s) {
            scholar_no = s.scholar_no;
            photo = s.photo;
            unique_name = s.unique_name;
            student_mobile = s.mobile.ToString();
            parents_phone = s.parents_phone.ToString();
            residential_address = s.residential_address;
            class_coordinator = s.Class.Teacher.faculty_name;
            student_current_qualifications = s.Student_Current_Qualification;
        }
    }
}
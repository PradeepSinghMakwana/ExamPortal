namespace ExamPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public partial class StudentProfileVM
    {
        public Student StudentBasicProfile { get; set; }
        public IEnumerable<Student_Current_Qualification> Student_Current_Qualifications { get; set; }
        public IEnumerable<Student_12th_Qualification> Student_12th_Qualifications { get; set; }
        public IEnumerable<Student_10th_Qualification> Student_10th_Qualifications { get; set; }
    }
}
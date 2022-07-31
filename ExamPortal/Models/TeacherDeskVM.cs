namespace ExamPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using ExamPortal.Models.ViewModels;
    using System.Linq;

    public partial class TeacherDeskVM
    {
        
        private int faculty_id;
        public IEnumerable<UnassignedTest> UnassignedTestTeacherDesks { get; set; }
        public IEnumerable<UnapprovedResult> UnapprovedResultsTeacherDesks { get; set; }
        public IEnumerable<CreateStudentVM> studentsITeach { get; set; }
        public IEnumerable<Subject> subjectsITeach { get; set; }
        public TeacherDeskVM(int faculty_id)
        {
            this.faculty_id = faculty_id;
        }
    }
}
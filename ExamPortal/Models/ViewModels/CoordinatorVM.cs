using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class CoordinatorVM
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public int facultyId { get; set; }
        public List<CreateStudentVM> studentsImCoordinating { get; set; }
        public List<CreateStudentVM> studentsITeach { get; set; }
        public List<CreateStudentVM> UnapprovedStudents { get; set; }
        
        public CoordinatorVM(int facultyId) {
            this.facultyId = facultyId;
        }
        
    }
}
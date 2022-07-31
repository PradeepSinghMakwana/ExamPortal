using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class AssignTeacherSubjectVM:Teach
    {
        public int course_name { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
        public IEnumerable<SelectListItem> Subjects { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
        public IEnumerable<Object[]> Teaches { get; set; }
    }
}
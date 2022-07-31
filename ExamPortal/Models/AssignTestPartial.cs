using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public partial class AssignTestPartial
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public int scholar_no { get; set; }
        public string student_name { get; set; }
        public List<string> student_ATKT_subjects { get { return db.Student_Current_Qualification.Include(s=>s.Subject).Where(a=>a.scholar_no==this.scholar_no).Where(s=>s.marks<=33).Select(s=>s.Subject.subject_name).ToList(); } }
        public string pretty_class { get; set; }

        public AssignTestPartial(int scholar_no)
        {
            this.scholar_no = scholar_no;
        }
    }
}
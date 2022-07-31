namespace ExamPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public partial class AdminDeskVM
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public List<Test> TestAdminDesks { get { return db.Tests.ToList();  } }
        public IEnumerable<TeacherAdminDesk> TeacherAdminDesks { get { return db.TeacherAdminDesks.AsEnumerable(); } }
        public IEnumerable<Class> ClassAdminDesks { get { return db.Classes.AsEnumerable(); } }
        public IEnumerable<Course> CourseAdminDesks { get { return db.Courses.AsEnumerable(); } }
        public IEnumerable<Subject> SubjectAdminDesks { get { return db.Subjects.AsEnumerable(); } }
        public IEnumerable<Subjects_In_10th> Subjects_In_10ths { get { return db.Subjects_In_10th.AsEnumerable(); } }

        public AdminDeskVM() {
        }
    }
}
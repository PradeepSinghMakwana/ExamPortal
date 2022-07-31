using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class CreateClassVM:Class
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public IEnumerable<Teacher> teachers
        {
            get
            {
                return db.Teachers.AsEnumerable();
            }
        }
        public IEnumerable<Course> courses
        {
            get
            {
                return db.Courses.AsEnumerable();
            }
        }
        public IEnumerable<Class> classes
        {
            get
            {
                return db.Classes.AsEnumerable();
            }
        }
        public enum Year
        {
            Year1,
            Year2,
            Year3,
            Year4
        }
        public enum Semester
        {
            Semester1,
            Semester2,
            Semester3,
            Semester4,
            Semester5,
            Semester6,
            Semester7,
            Semester8
        }
        public enum TimeType {
            Yearly,
            Semester_based
        }

    }
}
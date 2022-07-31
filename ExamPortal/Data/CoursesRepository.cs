using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class CoursesRepository
    {
        public IEnumerable<SelectListItem> GetCourses()
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> courses = db.Courses.AsNoTracking().OrderBy(s => s.course_name).Select(s => new SelectListItem
                {
                    Value = s.course_name,
                    Text = s.course_name
                }).ToList();
                var coursetip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Course ---"
                };
                courses.Insert(0, coursetip);
                return new SelectList(courses, "Value", "Text");
            }
        }
    }
}
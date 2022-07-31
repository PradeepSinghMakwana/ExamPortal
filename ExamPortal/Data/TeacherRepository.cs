using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class TeacherRepository
    {
        public IEnumerable<SelectListItem> GetTeachers()
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> teachers = db.Teachers.OrderBy(s => s.faculty_name).Select(s => new SelectListItem
                {
                    Value = s.faculty_id.ToString(),
                    Text = s.faculty_name
                }).ToList();
                var subjecttip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Teacher ---"
                };
                teachers.Insert(0, subjecttip);
                return new SelectList(teachers, "Value", "Text");
            }
        }
    }
}
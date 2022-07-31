using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class Subjects_In_10thRepository
    {
        public IEnumerable<SelectListItem> GetSubjects_In_10th()
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> subjects = db.Subjects_In_10th.AsNoTracking().OrderBy(s => s.subject_name).Select(s => new SelectListItem
                {
                    Value = s.subject_code.ToString(),
                    Text = s.subject_name
                }).ToList();
                var subjecttip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Subject ---"
                };
                subjects.Insert(0, subjecttip);
                return new SelectList(subjects, "Value", "Text");
            }
        }
    }
}
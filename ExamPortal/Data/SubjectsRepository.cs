using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ExamPortal.Data
{
    public class SubjectsRepository
    {
        public IEnumerable<SelectListItem> GetSubjects()
        {
            using (var db = new ExamPortalEntities()) {
                List<SelectListItem> subjects = db.Subjects.OrderBy(s => s.subject_name).Select(s => new SelectListItem
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
        public async Task<List<Subject>> subjectsITeach(int facultyId)
        {
            List<Subject> subjects = null;
            using (var db = new ExamPortalEntities())
            {
                subjects = await db.Teaches.Include(t => t.Subject).Select(t=>t.Subject).ToListAsync();
            }
            return subjects;
        }
    }
}
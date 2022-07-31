using ExamPortal.Models;
using ExamPortal.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class ClassesRepository
    {   
        public IEnumerable<SelectListItem> ListClassesITeach(int facultyId)
        {
            List<ClassVM> classes = new List<ClassVM>();
            using (var db = new ExamPortalEntities())
            {
                IAsyncEnumerable<Class> data = db.Teaches.Include(t => t.Class).Where(t => t.faculty_id == facultyId).Select(c => c.Class).Distinct().ToAsyncEnumerable();
                data.ForEach(c => classes.Add(new ClassVM(c)));
            }
            List<SelectListItem> selectList = classes.Select(s => new SelectListItem
                {
                    Value = s.class_id.ToString(),
                    Text = s.Pretty_Class_name
                }).ToList();
            return new SelectList(selectList, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetClassesByCourse(string course_name)
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> classes = db.Classes.AsNoTracking().OrderBy(s => s.Course).Where(c => c.course_name == course_name).Select(s => new SelectListItem
                {
                    Value = s.class_id.ToString(),
                    Text = (new Models.ViewModels.ClassVM(s)).Pretty_Class_name
                }).ToList();
                var classtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Class ---"
                };
                classes.Insert(0, classtip);
                return new SelectList(classes, "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> GetClassesImCoordinating(int facultyId)
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> classes = db.Classes.AsNoTracking().OrderBy(s => s.course_name).Where(c=>c.class_coordinator==facultyId).AsEnumerable().Select(s => new SelectListItem
                {
                    Value = s.class_id.ToString(),
                    Text = (new Models.ViewModels.ClassVM(s)).Pretty_Class_name
                }).ToList();
                var classtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Class ---"
                };
                classes.Insert(0, classtip);
                return new SelectList(classes, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> GetClassesImCoordinatingByCourse(int facultyId, string course_name)
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> classes = db.Classes.AsNoTracking().OrderBy(s => s.course_name).Where(c => c.class_coordinator == facultyId).Where(c=>c.course_name==course_name).AsEnumerable().Select(s => new SelectListItem
                {
                    Value = s.class_id.ToString(),
                    Text = (new Models.ViewModels.ClassVM(s)).Pretty_Class_name
                }).ToList();
                var classtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Class ---"
                };
                classes.Insert(0, classtip);
                return new SelectList(classes, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> GetClasses()
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> classes = db.Classes.AsNoTracking().OrderBy(s => s.course_name).AsEnumerable().Select(s => new SelectListItem
                {
                    Value = s.class_id.ToString(),
                    Text = (new Models.ViewModels.ClassVM(s)).Pretty_Class_name
                }).ToList();
                var classtip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Class ---"
                };
                classes.Insert(0, classtip);
                return new SelectList(classes, "Value", "Text");
            }
        }
    }
}
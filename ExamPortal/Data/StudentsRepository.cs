using ExamPortal.Models;
using ExamPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class StudentsRepository
    {
        public async Task<List<CreateStudentVM>> StudentsImCoordinating(int facultyId) {
            using (var db = new ExamPortalEntities())
            {
                return await db.Students.Include(s => s.Class).Include(s=>s.UserLogin).Where(s => s.Class.class_coordinator == facultyId).Select(s=>new CreateStudentVM(s)).ToListAsync();
            }
        }
        public async Task<List<Student>> UnapprovedStudents() {
            using (var db = new ExamPortalEntities())
            {
                return await db.Students.Where(s => s.is_valid_student.Equals(0)).ToListAsync();
            }
        }
        public async Task<IEnumerable<SelectListItem>> student_Fail_subjects(int scholar_no)
        {
            List<Subject> fails = new List<Subject>();
            using (var db = new ExamPortalEntities())
            {
                fails = await db.Student_Current_Qualification.Include(s => s.Subject).Where(a => a.scholar_no == scholar_no).Where(s => s.marks <= 33).Select(s => s.Subject).ToListAsync();
            }
            List<SelectListItem> failSubjects = fails.Select(s => new SelectListItem
            {
                Value = s.subject_code.ToString(),
                Text = s.subject_name
            }).ToList();
            return new SelectList(failSubjects, "Value", "Text");
        }
    public async Task<List<Student>> ListStudentITeach(int facultyId)
        {
            List<Student> students = new List<Student>();
            using (var db = new ExamPortalEntities())
            {
                /*Warning*/
                //EF Core does not support multiple parallel operations being run on the same context instance. 
                //You should always wait for an operation to complete before beginning the next operation.
                //This is typically done by using the await keyword on each asynchronous operation.
                // For more information see https://docs.microsoft.com/en-us/ef/core/querying/async

                List<Class> classes = await db.Teaches.Include(t => t.Class).Where(t => t.faculty_id == facultyId).Select(c => c.Class).Distinct().ToListAsync();
                classes.ForEach(async c => students.AddRange(await db.Students.ToListAsync()));
            }
            return students;
        }
        public async Task<IEnumerable<UserLogin>> ListStudentOnlyWithLoginDetails()
        {
            string role = "student";
            List<UserLogin> allStudents = new List<UserLogin>();
            List<UserLogin> OnlyStudents = new List<UserLogin>();
            using (var db = new ExamPortalEntities())
            {
                allStudents = await db.UserRoles.Include(u=>u.UserLogin).Where(s => s.role == role).Select(u => u.UserLogin).ToListAsync();
                OnlyStudents = await db.Students.Include(s=>s.UserLogin).Select(s => s.UserLogin).ToListAsync();
            }
            OnlyStudents.ForEach(s => allStudents.RemoveAll(u => u.Equals(s)));
            List<UserLogin> students = allStudents;
            return students;
        }
    }
}
using ExamPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public class ReusableFunctions
    {
        ExamPortalEntities db;
        public List<ClassVM> classesITeach(int facultyId) {
            List<ClassVM> classes = new List<ClassVM>();
            List<int> classIds = new List<int>();
            //List<Teach> teaches = db.Teaches.ToList();
            classIds = db.Teaches.Where(t => t.faculty_id == facultyId).Select(t => t.class_id).ToList();
            //classes = db.Teaches.Include("Classes").Where(t => t.faculty_id == facultyId).Select(t=>t.Class).ToList();
            classIds.ForEach(async c => classes.Add(new ClassVM(await db.Classes.FindAsync(c))));
            return classes;
        }
        public IEnumerable<Student> studentsITeach(int facultyId)
        {
            var classes = classesITeach(facultyId).ToList();
            var class_ids=new List<int>();
            classes.ForEach(x => class_ids.Add(x.class_id));
            return db.Students.Where(s => (class_ids.Contains(s.Class.class_id)));
        }
        public IEnumerable<Subject> subjectsITeach(int facultyId) {
            return db.Teaches.Where(t => t.faculty_id == facultyId).Select(t => t.Subject);
        }
        public IEnumerable<Test> TestsTakenByStudent(int scholar_no)
        {
            return db.Registered_Candidates.Where(rc => rc.Student.scholar_no == scholar_no).Where(rc => rc.approved == true).Select(rc => rc.Test).AsEnumerable();
        }

        public ReusableFunctions()
        {
            db = new ExamPortalEntities();
        }
        public ReusableFunctions(ExamPortalEntities db){
            this.db = db;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class AssignTestVM
    {
        public int facultyId;
        ExamPortalEntities db = new ExamPortalEntities();
        public IEnumerable<Test> Tests { get { return db.Tests.AsEnumerable(); } }
        public List<Tuple<string, int>> listClassesITeach { get
            {
                List<Tuple<string, int>> pretty_class_names = new List<Tuple<string, int>>();
                var Classes = new ReusableFunctions(db).classesITeach(this.facultyId).ToList();
                Classes.ForEach(x => pretty_class_names.Add(new Tuple<string, int>(x.Pretty_Class_name, x.class_id)));
                return pretty_class_names;
            } }
        public IEnumerable<Student> AllStudents { get
            {
                return db.Students.AsEnumerable();
            } }

        public IEnumerable<Student> studentITeach
        {
            get
            {
                return new ReusableFunctions(db).studentsITeach(this.facultyId);
            }
        }
        public IEnumerable<Test> TestsTaken(int scholar_no)
        {
            return new ReusableFunctions(db).TestsTakenByStudent(scholar_no);
        }
        public List<AssignTestPartial> assignTestPartials { get {
                var testPartials = new List<AssignTestPartial>();
                studentITeach.ToList().ForEach(x => testPartials.Add(new AssignTestPartial(x.scholar_no)));
                return testPartials;
            } }
        public AssignTestStudentPartial2 studentBriefDetails(int scholar_no) { return new AssignTestStudentPartial2(db.Students.Single(s=>s.scholar_no==scholar_no)); }
        public AssignTestVM(int facultyId)
        {
            this.facultyId = facultyId;
        }
    }
}
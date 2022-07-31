using ExamPortal.Models;
using ExamPortal.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Controllers
{
    public class CoordinatorsController : Controller
    {
        ExamPortalEntities db = new ExamPortalEntities();
        // GET: Coordinator
        public async Task<ActionResult> Index()
        {
            int? facultyId = (int)Session["facultyId"];
            bool? valid = true;
            /*var sRepo = new Data.StudentsRepository();
            var task1 = sRepo.ListStudentITeach(facultyId.Value);
            task1.Start();
            var task2 = sRepo.StudentsImCoordinating(facultyId.Value);
            task2.Start();
            */
            Models.ViewModels.CoordinatorVM vM = new CoordinatorVM(facultyId.Value);
            List<CreateStudentVM> studentsITeach = new List<CreateStudentVM>();
            List<CreateStudentVM> studentsImCoordinating = new List<CreateStudentVM>();
            db.Database.Connection.Open();
                
            //get List of students I teach
            List<int> classes = await db.Teaches.Where(t => t.faculty_id == facultyId).Select(c => c.class_id).Distinct().ToListAsync();
            classes.ForEach(c => studentsITeach.AddRange(db.Students.Include(l=>l.Class).Include(u=>u.UserLogin).Where(s=>s.class_id==c).AsEnumerable().Select(s=> new CreateStudentVM(s)).ToList()));
            //get List of students I am class coordinator of
            List<int> classes2 = await db.Classes.Where(t => t.class_coordinator == facultyId).Select(c => c.class_id).ToListAsync();
            classes2.ForEach(c =>studentsImCoordinating.AddRange(db.Students.Include(l => l.Class).Include(u => u.UserLogin).Where(s=>s.class_id==c).AsEnumerable().Select(s => new CreateStudentVM(s)).ToList()));
            //get List of all Student Registrations pending to be approved by any class coordinators
            vM.UnapprovedStudents = db.Students.Include(s => s.Class).Include(s => s.UserLogin).Where(s => s.is_valid_student!=valid).AsEnumerable().Select(s=>new CreateStudentVM(s)).ToList();
            /*
            //get List of subjects I teach
            subjects = await db.Teaches.Include(t => t.Subject).Select(t => t.Subject).ToListAsync();
            */
            db.Database.Connection.Close();

            //task1.Wait();
            //task2.Wait();

            vM.studentsITeach = studentsITeach;
            vM.studentsImCoordinating = studentsImCoordinating;
            return View(vM);
        }
        // GET: Coordinators/AddNewStudentLogin
        public async Task<ActionResult> AddNewStudentLogin()
        {
            AddStudentLoginCoordinatorVM vM = new AddStudentLoginCoordinatorVM();
            Generators.OTPGenerator generator = new Generators.OTPGenerator(Generators.OTPGenerator.OTPOptions.Alphanumeric, 5);
            vM.password = generator.password;

            Data.StudentsRepository sRepo = new Data.StudentsRepository();
            vM.UnregisteredStudents = await sRepo.ListStudentOnlyWithLoginDetails();
            return View(vM);
        }

        // POST: Coordinators/AddNewStudentLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewStudentLogin([Bind(Include = "username,password")] AddStudentLoginCoordinatorVM vM)
        {
            UserRole studentRole = null;
            UserLogin userStudent = null;
            try
            {
                UserLogin user = new UserLogin();
                user.username = vM.username;
                user.password = vM.password;
                db.UserLogins.Add(user);
                await db.SaveChangesAsync();
                userStudent = db.UserLogins.Single(u => (u.username == vM.username) && (u.password == vM.password));
                studentRole = new UserRole();
            }
            catch (Exception e) {
                throw e;
            }
            if (userStudent != null)
            {
                try
                {
                    //set the role of new user to student                    
                    studentRole.UserLogin = userStudent;
                    studentRole.user_id = userStudent.user_id;
                    studentRole.role = "student";
                    db.UserRoles.Add(studentRole);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    //roll back
                    db.UserLogins.Remove(userStudent);
                    await db.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult AssignTeacherSubject() {
            int facultyId = (int)Session["facultyId"];
            Models.ViewModels.AssignTeacherSubjectVM vM = new Models.ViewModels.AssignTeacherSubjectVM();
            Data.TeacherRepository teacherRepository = new Data.TeacherRepository();
            Data.ClassesRepository classesRepository = new Data.ClassesRepository();
            Data.CoursesRepository coursesRepository  = new Data.CoursesRepository();
            Data.SubjectsRepository subjectsRepository = new Data.SubjectsRepository();
            Data.TeachesRepository teachesRepository = new Data.TeachesRepository();
            vM.Teachers = teacherRepository.GetTeachers();
            vM.Classes = classesRepository.GetClassesImCoordinating(facultyId);
            vM.Courses = coursesRepository.GetCourses();
            vM.Subjects = subjectsRepository.GetSubjects();
            vM.Teaches = teachesRepository.GetTeaches();
            return View(vM);
        }
        [HttpPost]
        public ActionResult AssignTeacherSubject([Bind(Include ="faculty_id,class_id,subject_code")] AssignTeacherSubjectVM vM)
        {
            Teach teach = new Teach();
            teach.faculty_id = vM.faculty_id;
            teach.class_id = vM.class_id;
            teach.subject_code = vM.subject_code;
            db.Teaches.Add(teach);
            db.SaveChanges();
            return RedirectToAction("AssignTeacherSubject");
        }
        public ActionResult DeleteTeacherSubjectRelation(int c, int t, int s) {
            Teach teach = new Teach();
            teach.faculty_id = t;
            teach.class_id = c;
            teach.subject_code = s;
            db.Teaches.Remove(teach);
            db.SaveChanges();
            return RedirectToAction("AssignTeacherSubject");
        }
        public JsonResult getClassesImCoordinatingByCourse(string course) {
            int facultyId = (int)Session["facultyId"];
            Data.ClassesRepository classesRepository = new Data.ClassesRepository();
            var x = classesRepository.GetClassesImCoordinatingByCourse(facultyId,course);
            return Json(x, JsonRequestBehavior.AllowGet);
        }
            public ActionResult getDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Include(c=>c.Class).Include(u=>u.UserLogin).Single(s => s.scholar_no == id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }

            CreateStudentVM stu = new CreateStudentVM(student);
            return PartialView("_StudentUpdateLogin", stu);
        }
        //POST: /Coordinators/ChangeStudentLoginDetails
        public async Task<ActionResult> ChangeStudentLoginDetails([Bind(Include = "user_id,UserLogin.username,UserLogin.password")] CreateStudentVM vM) {
            string failMsg = "Failed To Update Student Login";
            string successMsg = "Student Login Updated Successfuly";
            if (vM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.Include(c => c.Class).Include(u => u.UserLogin).SingleAsync(s => s.user_id == vM.user_id);
            if (student == null)
            {
                return HttpNotFound();
            }
            UserLogin user = student.UserLogin;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ViewBag.message = successMsg;
                return RedirectToAction("Index");
            }
            ViewBag.message=failMsg;
            return View(vM);
        }
        /*
        public ActionResult EditCurrentQualification(int scholar_no, int subject_code)
        {

        }

        public ActionResult DeleteCurrentQualification(int scholar_no, int subject_code)
        {

        }
        */
    }
}
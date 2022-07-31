using ExamPortal.Models;
using ExamPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Controllers
{
    
    public class AdminsController : Controller
    {
        ExamPortalEntities db = new ExamPortalEntities();
        // GET: Admins/Desktop
        
        public ActionResult Index()
        {
            AdminDeskVM viewModel = new AdminDeskVM();
            return View(viewModel);
        }
        public ActionResult CreateClass()
        {
            CreateClassVM viewModel = new CreateClassVM();
            return View(viewModel);
        }

        // POST: Admins/CreateCourse
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateClass(CreateClassVM vm)
        {
            string selTeacher = Request.Form["selTeacher"].ToString();
            string selCourse = Request.Form["selCourse"].ToString();
            string yrSelected = Request.Form["yrSelected"].ToString();
            string semSelected = Request.Form["semSelected"].ToString();
            Teacher teacher = await db.Teachers.FindAsync(Convert.ToInt32(selTeacher));
            byte year = 30;
            byte sem = 30;
            if (teacher == null) {
                return HttpNotFound();
            }
            Class newClass = new Class();
            newClass.course_name = selCourse;
            newClass.Teacher = teacher;
            switch (semSelected)
            {
                case "Semester 1": year = 1; sem = 1; break;
                case "Semester 2": year = 1; sem = 2; break;
                case "Semester 3": year = 2; sem = 1; break;
                case "Semester 4": year = 2; sem = 2; break;
                case "Semester 5": year = 3; sem = 1; break;
                case "Semester 6": year = 3; sem = 2; break;
                case "Semester 7": year = 4; sem = 1; break;
                case "Semester 8": year = 4; sem = 2; break;
            }
            if (semSelected.Equals(""))
            {
                switch (yrSelected)
                {
                    case "Year 1": year = 1; sem = 0; break;
                    case "Year 2": year = 2; sem = 0; break;
                    case "Year 3": year = 3; sem = 0; break;
                    case "Year 4": year = 4; sem = 0; break;
                }
            }
            if ((year == 30 )||( sem == 30)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            newClass.year = year;
            newClass.semester = sem;
            db.Classes.Add(newClass);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: Admins/DeleteClass/5
        public async Task<ActionResult> DeleteClass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class class1 = await db.Classes.FindAsync(id);
            if (class1 == null)
            {
                return HttpNotFound();
            }
            return View(class1);
        }

        // POST: Admins/DeleteCourse/5
        [HttpPost, ActionName("DeleteClass")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteClassConfirmed(int id)
        {
            Class class1 = await db.Classes.FindAsync(id);
            //delete class
            db.Classes.Remove(class1);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Admins/SetDistributionSystem
        public ActionResult SetDistributionSystem() {
            string[] ipAddresses = new string[50];//assume only IPv4 addresses are there, i.e. length is same.
            SetDistributionSystemVM viewModel = new SetDistributionSystemVM();
            Array.Sort(ipAddresses);
            List<SelectListItem> selList = new List<SelectListItem>();
            for (int i = 0; i < ipAddresses.Length; i++) {
                selList.Add(new SelectListItem { Selected = ((i == 0) ? true : false), Text = ipAddresses[i], Value = i.ToString() });
            }
            viewModel.IPaddresses = selList;
            return View(viewModel);
        }

        // GET: Admins/IndexCourse
        public async Task<ActionResult> IndexCourse()
        {
            return View(await db.Courses.ToListAsync());
        }

        // GET: Admins/CreateCourse
        public ActionResult CreateCourse()
        {
            return View();
        }

        // POST: Admins/CreateCourse
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCourse([Bind(Include = "course_name")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexCourse");
            }

            return View(course);
        }


        // GET: Admins/EditCourse/5
        public async Task<ActionResult> EditCourse(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admins/EditCourse/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCourse([Bind(Include = "course_name")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexCourse");
            }
            return View(course);
        }

        // GET: Admins/DeleteCourse/5
        public async Task<ActionResult> DeleteCourse(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Admins/DeleteCourse/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCourseConfirmed(string id)
        {
            Course course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexCourse");
        }

        // GET: Admins/Subjects
        public async Task<ActionResult> IndexSubject()
        {
            return View(await db.Subjects.ToListAsync());
        }

        // GET: Admins/CreateSubjects
        public ActionResult CreateSubject()
        {
            return View();
        }

        // POST: Admins/CreateSubject
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubject([Bind(Include = "subject_code,subject_name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubject");
            }

            return View(subject);
        }

        // GET: Admins/EditSubject/5
        public async Task<ActionResult> EditSubject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Admins/EditSubject/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubject([Bind(Include = "subject_code,subject_name")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubject");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<ActionResult> DeleteSubject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Admins/DeleteSubject/5
        [HttpPost, ActionName("DeleteSubject")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSubjectConfirmed(int id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexSubject");
        }

        // GET: Admins/Subjects_In_10th
        public async Task<ActionResult> IndexSubjects_In_10th()
        {
            return View(await db.Subjects_In_10th.ToListAsync());
        }

        // GET: Admins/CreateSubjects_In_10th
        public ActionResult CreateSubjects_In_10th()
        {
            return View();
        }

        // POST: Subjects_In_10th/CreateSubjects_In_10th
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubjects_In_10th([Bind(Include = "subject_code,subject_name")] Subjects_In_10th subjects_In_10th)
        {
            if (ModelState.IsValid)
            {
                db.Subjects_In_10th.Add(subjects_In_10th);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubjects_In_10th");
            }

            return View(subjects_In_10th);
        }

        // GET: Admins/EditSubjects_In_10th/5
        public async Task<ActionResult> EditSubjects_In_10th(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_10th subjects_In_10th = await db.Subjects_In_10th.FindAsync(id);
            if (subjects_In_10th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_10th);
        }

        // POST: Admins/EditSubjects_In_10th/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubjects_In_10th([Bind(Include = "subject_code,subject_name")] Subjects_In_10th subjects_In_10th)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects_In_10th).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubjects_In_10th");
            }
            return View(subjects_In_10th);
        }

        // GET: Admins/DeleteSubjects_In_10th/5
        public async Task<ActionResult> DeleteSubjects_In_10th(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_10th subjects_In_10th = await db.Subjects_In_10th.FindAsync(id);
            if (subjects_In_10th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_10th);
        }

        // POST: Admins/DeleteSubjects_In_10th/5
        [HttpPost, ActionName("DeleteSubjects_In_10th")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSubjects_In_10thConfirmed(int id)
        {
            Subjects_In_10th subjects_In_10th = await db.Subjects_In_10th.FindAsync(id);
            db.Subjects_In_10th.Remove(subjects_In_10th);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexSubjects_In_10th");
        }

        // GET: Admins/Subjects_In_12th
        public async Task<ActionResult> IndexSubjects_In_12th()
        {
            return View(await db.Subjects_In_12th.ToListAsync());
        }
        // GET: Admins/CreateSubjects_In_12th
        public ActionResult CreateSubjects_In_12th()
        {
            return View();
        }

        // POST: Admins/CreateSubjects_In_12th
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSubjects_In_12th([Bind(Include = "subject_code,subject_name")] Subjects_In_12th subjects_In_12th)
        {
            if (ModelState.IsValid)
            {
                db.Subjects_In_12th.Add(subjects_In_12th);
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubjects_In_12th");
            }

            return View(subjects_In_12th);
        }

        // GET: Admins/EditSubjects_In_12th/5
        public async Task<ActionResult> EditSubjects_In_12th(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            if (subjects_In_12th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_12th);
        }

        // POST: Admins/EditSubjects_In_12th/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSubjects_In_12th([Bind(Include = "subject_code,subject_name")] Subjects_In_12th subjects_In_12th)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjects_In_12th).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("IndexSubjects_In_12th");
            }
            return View(subjects_In_12th);
        }

        // GET: Admins/DeleteSubjects_In_12th/5
        public async Task<ActionResult> DeleteSubjects_In_12th(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            if (subjects_In_12th == null)
            {
                return HttpNotFound();
            }
            return View(subjects_In_12th);
        }

        // POST: Admins/DeleteSubjects_In_12th/5
        [HttpPost, ActionName("DeleteSubjects_In_12th")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSubjects_In_12thConfirmed(int id)
        {
            Subjects_In_12th subjects_In_12th = await db.Subjects_In_12th.FindAsync(id);
            db.Subjects_In_12th.Remove(subjects_In_12th);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexSubjects_In_12th");
        }


        // GET: Admins/CreateTeacher
        public ActionResult CreateTeacher()
        {
            Models.ViewModels.TeacherVM viewModel = new Models.ViewModels.TeacherVM();
            viewModel.dob = DateTime.Today;
            return View(viewModel);
        }

        // POST: Admins/CreateTeacher
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // comment: We are asking to Include Nullable type year_post_graduated which may cause unexpected behaviour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTeacher([Bind(Include = "faculty_name,graduate,post_graduate,year_graduated,year_post_graduated,dob,blood_group,residential_address,mobile,served,username,password")] Models.ViewModels.TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                UserLogin user = new UserLogin();
                user.username = teacherVM.username;
                user.password = teacherVM.password;
                //check if a teacher with same username already exists
                bool assertUsernameExists = await db.UserRoles.Where(u => u.role == "teacher").AnyAsync(t => t.UserLogin.username == teacherVM.username);
                if (!assertUsernameExists) {
                    db.UserLogins.Add(user);
                    await db.SaveChangesAsync();
                    UserLogin validUser = await db.UserLogins.SingleAsync(u => u.username == user.username);
                    UserRole userRole = new UserRole();
                    userRole.role = "teacher";
                    userRole.UserLogin = validUser;
                    userRole.user_id = userRole.UserLogin.user_id;
                    db.UserRoles.Add(userRole);
                    await db.SaveChangesAsync();

                    Teacher teacher = new Teacher();
                    teacher.UserLogin = validUser;
                    teacher.user_id = teacher.UserLogin.user_id;
                    teacher.faculty_name = teacherVM.faculty_name;
                    teacher.graduate = teacherVM.graduate;
                    teacher.post_graduate = teacherVM.post_graduate;
                    if (!String.IsNullOrWhiteSpace(teacherVM.post_graduate))
                    {
                        if (teacherVM.year_post_graduated != null)
                        {
                            teacher.year_post_graduated = teacherVM.year_post_graduated;
                        }
                    }
                    teacher.year_graduated = teacherVM.year_graduated;
                    teacher.dob = teacherVM.dob;
                    teacher.blood_group = teacherVM.blood_group;
                    teacher.residential_address = teacherVM.residential_address;
                    teacher.mobile = teacherVM.mobile;
                    teacher.served = teacherVM.served;

                    db.Teachers.Add(teacher);
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexTeachers", "Teachers");
                }
                else
                {
                    return View(teacherVM);
                }
                
            }

            return View(teacherVM);
        }

        // GET: Admins/EditTeacher/5
        public async Task<ActionResult> EditTeacher(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            Session["facultyId"] = teacher.faculty_id;
            return RedirectToAction("Edit", "Teachers", new { id=id });
        }

        // GET: Admins/DeleteTeacher/5
        public async Task<ActionResult> DeleteTeacher(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            // don't remove teacher, if (s)he is a class coordinator
            if ( teacher.Classes.Count() > 0) {
                ViewBag.Message = "Couldn't remove teacher because this teacher is a coordinator of one or more classes.";
                return RedirectToAction("IndexTeachers", "Teachers");
            }
            return View(teacher);
        }

        // POST: Admins/DeleteTeacher/5
        [HttpPost, ActionName("DeleteTeacher")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTeacherConfirmed(int id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            foreach(UserRole role in teacher.UserLogin.UserRoles) {
                db.UserRoles.Remove(role);
            }
            db.Teachers.Remove(teacher);
            db.UserLogins.Remove(teacher.UserLogin);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexTeachers", "Teachers");
        }
        
        public ActionResult ListStudents()
        {
            List<CreateStudentVM> studentVMs = new List<CreateStudentVM>();
            var students = db.Students.Include(c=>c.Class).Include(u=>u.UserLogin).ForEachAsync(s =>
            studentVMs.Add(new CreateStudentVM(s))
            );
            return View(studentVMs);
        }

        public ActionResult ListTest() {
            return View(db.Tests.AsEnumerable());
        }
    }
}
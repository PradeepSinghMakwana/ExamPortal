using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExamPortal.Models;
using ExamPortal.Models.ViewModels;

namespace ExamPortal.Controllers
{
    public class StudentsController : Controller
    {
        private ExamPortalEntities db = new ExamPortalEntities();

        // GET: Students
        public ActionResult Index()
        {
            if ((int)Session["scholarNo"] != -1)
            {
                int scholarNo = (int)Session["scholarNo"];
                StudentProfileVM viewModel = new StudentProfileVM();
                viewModel.StudentBasicProfile = db.Students.FirstOrDefault(s => s.scholar_no == scholarNo);
                viewModel.Student_Current_Qualifications = db.Student_Current_Qualification.Where(q => q.Student.scholar_no == scholarNo).AsEnumerable(); ;
                viewModel.Student_12th_Qualifications = db.Student_12th_Qualification.Where(q => q.Student.scholar_no == scholarNo).AsEnumerable();
                viewModel.Student_10th_Qualifications = db.Student_10th_Qualification.Where(q => q.Student.scholar_no == scholarNo).AsEnumerable();
                return View(viewModel);
            }
            return View();
        }
        public ActionResult TestResult(int testId) {
            int scholarNo = (int)Session["scholarNo"];
            ExamPortal.Models.ViewModels.StudentTestResultVM vM = new StudentTestResultVM();
            Registered_Candidates registered_Candidates = db.Registered_Candidates.Include(t=>t.Test).Single(r => (r.scholar_no == scholarNo) && (r.test_id == testId));
            List<Student_Test_Record> student_Test_Records = db.Student_Test_Record.Where(s => s.scholar_no == scholarNo).Where(s => s.test_id == testId).ToList();
            List<TestResult> ts = new List<TestResult>();
            student_Test_Records.ForEach(r => ts.Add(new TestResult(r)));
            vM.test_name = registered_Candidates.Test.test_name;
            vM.testResults = ts;
            return View(vM);
        }
        // GET: Students/UpcomingTest
        public ActionResult UpcomingTest()
        {
            ViewBag.Title = "Upcoming Test";
            if ((int)Session["scholarNo"] != -1)
            {
                int scholarNo = (int)Session["scholarNo"];
                UpcomingTestVM viewModel = new UpcomingTestVM(scholarNo);
                viewModel.UpcomingTests = db.UnregisteredTests.Where(a => a.scholar_no == scholarNo).OrderByDescending(a => a.date).AsEnumerable();
                return View(viewModel);
            }
            return View();
        }

        // GET: Students/UpcomingTest
        public async Task<ActionResult> RegisterMeForTest(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = await db.Tests.FindAsync(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            Student student=null;
            if ((int)Session["scholarNo"] != -1) {
                int scholarNo = (int)Session["scholarNo"];
                student = await db.Students.FindAsync(scholarNo);
            }
            if (student == null)
            {
                RedirectToAction("StudentLogin", "Home");
            }
            Registered_Candidates register_candidate = new Registered_Candidates();
            register_candidate.test_id = test.test_id;
            register_candidate.scholar_no = student.scholar_no;
            db.Registered_Candidates.Add(register_candidate);
            await db.SaveChangesAsync();
            return RedirectToAction("UpcomingTest");
        }
        public ActionResult TestScreen(int? test_id) {
            if (test_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //check if the current logged in student is registered for the test and is assigned the test.
            int scholarNo = (int)Session["scholarNo"];
            Models.ViewModels.TestScreen viewModel = new Models.ViewModels.TestScreen();
            if (db.Registered_Candidates.Any(t => (t.test_id == (int)test_id)&&(t.scholar_no == scholarNo)))
            {
                viewModel.Student = db.Students.Single(s => s.scholar_no == scholarNo);
                var test = db.Tests.Include(t=>t.Subject).Single(t => t.test_id == (int)test_id);
                viewModel.Test = test;
                viewModel.CurrentQuestion = viewModel.Questions.First();
                viewModel.Subject = test.Subject;
            }
            else {
                RedirectToAction("StudentLogin", "Home", new { additional_message = "Sorry, your session has expired. Please login again." });
            }
            
            
            return View(viewModel);
        }

        // GET: Students/setCurrentQuestionInTestScreen/5
        public ActionResult setCurrentQuestionInTestScreen(int q_id) {
            //Todo : is current question tested
            Question ques = db.Test_Question.Where(q => q.test_id == (int)Session["current_test_id"]).Single(q => q.question_id==q_id).Question;
            
            return View(ques);
        }

        public JsonResult IsStudentUsernameExists(string username)
        {
            //check if any of the username matches the username specified in the Parameter using the ANY extension method.  
            return Json(!db.Students.Any(x => x.UserLogin.username == username), JsonRequestBehavior.AllowGet);
        }

        /*
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        */
        // GET: Students/Create
        /*public ActionResult Create()
        {
            ViewBag.scholar_no = new SelectList(db.Student_Previous_Qualification, "scholar_no", "scholar_no");
            return View();
        }*/

        // GET: Students/NewStudent/5
        public async Task<ActionResult> NewStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin user = await db.UserLogins.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Class c = await db.Classes.FirstOrDefaultAsync();
            Student student = new Student();
            student.Class = c;
            student.UserLogin = user;
            student.is_valid_student = false;
            student.dob = DateTime.Today;

            var QualificationIn10th = new List<Qualification_In_10thVM>();
            var QualificationIn12th = new List<Qualification_In_12thVM>();

            Models.ViewModels.CreateStudentVM stu = new Models.ViewModels.CreateStudentVM(student);

            var subjects_In_10th = db.Subjects_In_10th.ToAsyncEnumerable();
            await subjects_In_10th.ForEachAsync(s=>
                QualificationIn10th.Add(new Qualification_In_10thVM(s.subject_code, s.subject_name, 0.00M))
            );
            stu.qualification_In_10s = QualificationIn10th;
            
            var subjects_In_12th = db.Subjects_In_12th.ToAsyncEnumerable();
            await subjects_In_12th.ForEachAsync(s =>
                QualificationIn12th.Add(new Qualification_In_12thVM(s.subject_code, s.subject_name, false, 0.00M))
            );
            stu.qualification_In_12s = QualificationIn12th;

            return View(stu);
        }

        // POST: Students/NewStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewStudent([Bind(Include =
            "class_id,Pretty_Class_name,is_valid_student,user_id,unique_name,parents_phone,mobile,image_file,enrollment_no,residential_address,permanent_address,fathers_name,mothers_name,adhar_no,blood_group,dob,qualification_In_10s,qualification_In_12s")] Models.ViewModels.CreateStudentVM vM)
        {
            if (ModelState.IsValid)
            {
                vM.photo = "x";
                Student student = vM.extractStudent();
                db.Students.Add(student);
                await db.SaveChangesAsync();

                Student foundStudent = db.Students.SingleOrDefault(s=>s.enrollment_no==student.enrollment_no);
                if (foundStudent == null) {
                    //should not reach here.
                    throw new Exception("Error! Failed to find student by enrollment no.");
                }
                int scholar_no = foundStudent.scholar_no;

                //get 10th and 12th qualification details, add them in the db
                {
                    var QualsIn10th = vM.extractQualificationsIn10th();
                    var QualsIn12th = vM.extractQualificationsIn12th();

                    foreach (var item in QualsIn10th)
                    {
                        item.scholar_no = scholar_no;
                        db.Student_10th_Qualification.Add(item);
                    }

                    foreach (var item in QualsIn12th)
                    {
                        item.scholar_no = scholar_no;
                        db.Student_12th_Qualification.Add(item);
                    }
                }

                string filename = foundStudent.scholar_no.ToString()+".jpg";
                string photo = "/Resources/StudentsPic/" + filename;
                vM.image_file.SaveAs(Server.MapPath("~/Resources/StudentsPic") + "/" + filename);
                foundStudent.photo = photo;
                db.Entry(foundStudent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("StudentLogin","Home");
            }

            //process invalid model
            vM.dob = DateTime.Today;

            var QualificationIn10th = new List<Qualification_In_10thVM>();
            var QualificationIn12th = new List<Qualification_In_12thVM>();

            var subjects_In_10th = db.Subjects_In_10th.ToAsyncEnumerable();
            await subjects_In_10th.ForEachAsync(s =>
                QualificationIn10th.Add(new Qualification_In_10thVM(s.subject_code, s.subject_name, 0.00M))
            );
            vM.qualification_In_10s = QualificationIn10th;

            var subjects_In_12th = db.Subjects_In_12th.ToAsyncEnumerable();
            await subjects_In_12th.ForEachAsync(s =>
                QualificationIn12th.Add(new Qualification_In_12thVM(s.subject_code, s.subject_name, false, 0.00M))
            );
            vM.qualification_In_12s = QualificationIn12th;
            
            return View(vM);
        }
        
        /*
        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.scholar_no = new SelectList(db.Student_Previous_Qualification, "scholar_no", "scholar_no", student.scholar_no);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scholar_no,unique_name,parents_phone,mobile,photo,enrollment_no,residential_address,permanent_address,fathers_name,mothers_name,adhar_no,blood_group,dob")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.scholar_no = new SelectList(db.Student_Previous_Qualification, "scholar_no", "scholar_no", student.scholar_no);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.FindAsync(id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

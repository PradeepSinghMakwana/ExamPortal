using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamPortal.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using ExamPortal.Models.ViewModels;
using ExamPortal.Data;
using System.Web.UI;

namespace ExamPortal.Controllers
{
    
    public class TeachersController : Controller
    {
        private ExamPortalEntities db = new ExamPortalEntities();
        
        // GET: Teachers
        public async Task<ActionResult> Index()
        {
            int facultyId = (int) Session["facultyId"];
            TeacherDeskVM vM = new TeacherDeskVM(facultyId);
            /*var stuRepo = new StudentsRepository();
            var subRepo = new SubjectsRepository();
            var task1 = stuRepo.ListStudentITeach(facultyId);
            var task2 = subRepo.subjectsITeach(facultyId);
            var task3 = db.UnassignedTests.ToAsyncEnumerable();
            var task4 = db.UnapprovedResults.ToAsyncEnumerable();

            

            vM.studentsITeach = await task1;
            vM.subjectsITeach = await task2;
            vM.UnassignedTestTeacherDesks = await task3.ToList();
            vM.UnapprovedResultsTeacherDesks = await task4.ToList();
            */
            List<CreateStudentVM> students = new List<CreateStudentVM>();
            //List<Subject> subjects = null;
            db.Database.Connection.Open();
            /*
            //get List of students I teach
            List<Class> classes = await db.Teaches.Include(t => t.Class).Where(t => t.faculty_id == facultyId).Select(c => c.Class).Distinct().ToListAsync();
            classes.ForEach(async c => students.AddRange(await db.Students.Include(l=>l.Class).Include(u=>u.UserLogin).Select(s=> new CreateStudentVM(s)).ToListAsync()));
            //get List of subjects I teach
            subjects = await db.Teaches.Include(t => t.Subject).Select(t => t.Subject).ToListAsync();
            */
            //get Unassigned test this teacher has
            vM.UnassignedTestTeacherDesks = await db.UnassignedTests.ToListAsync();
            //get Unapproved results this teacher has
            vM.UnapprovedResultsTeacherDesks = await db.UnapprovedResults.ToListAsync();

            db.Database.Connection.Close();
            //vM.studentsITeach = students;
            //vM.subjectsITeach = subjects;

            return View(vM);
        }

        public ActionResult IndexTeachers()
        {
            AdminDeskVM viewModel = new AdminDeskVM();
            return View(viewModel);
        }
        
        public async Task<ActionResult> appendToTempCreatedTest(int? questionId, int? unit)
        {
            int faculty_id = (int)Session["facultyId"];
            if ((questionId == null)||(unit == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Neccessarily check if the questionId really exists
            Question question = await db.Questions.FindAsync(questionId);
            if (question == null)
            {
                return HttpNotFound();
            }

            TempCreatedTest ques1 = new TempCreatedTest();
            ques1.faculty_id = faculty_id;
            ques1.q_id = question.q_id;
            ques1.unit = unit.Value;
            db.TempCreatedTests.Add(ques1);
            await db.SaveChangesAsync();
            return RedirectToAction("AddQuestionToTest");
        }

        public async Task<ActionResult> deleteFromTempCreatedTest(int? questionId, int? unit)
        {
            int faculty_id = (int)Session["facultyId"];
            if ((questionId == null) || (unit == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Neccessarily check if the questionId really exists
            Question question = await db.Questions.FindAsync(questionId);
            if (question == null)
            {
                return HttpNotFound();
            }

            TempCreatedTest ques1 = db.TempCreatedTests.Single(q => ((q.faculty_id == faculty_id) && (q.q_id == question.q_id) && (q.unit == unit.Value)));
            db.TempCreatedTests.Remove(ques1);
            await db.SaveChangesAsync();
            return RedirectToAction("AddQuestionToTest");
        }
        
        // GET: Teachers/AddQuestionToTest
        public ActionResult AddQuestionToTest()
        {
            int faculty_id = (int)Session["facultyId"];
            AddQuestionToTestVM vM = new AddQuestionToTestVM();
            vM.faculty_id = faculty_id;

            List<QuestionVM> list = new List<QuestionVM>();
            db.Questions.ToList().ForEach(item => list.Add(new QuestionVM(item)));
            vM.Questions = list;
            
            vM.Tests = db.Tests.AsEnumerable();
            vM.Subjects = db.Subjects.AsEnumerable();

            List<QuestionVM> unit1 = new List<QuestionVM>();
            List<QuestionVM> unit2 = new List<QuestionVM>();
            List<QuestionVM> unit3 = new List<QuestionVM>();
            List<QuestionVM> unit4 = new List<QuestionVM>();
            List<QuestionVM> unit5 = new List<QuestionVM>();
            
            db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).Where(u => u.unit == 1).Select(q => q.Question).ToList().ForEach(q => unit1.Add(new QuestionVM(q)));
            db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).Where(u => u.unit == 2).Select(q => q.Question).ToList().ForEach(q => unit2.Add(new QuestionVM(q)));
            db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).Where(u => u.unit == 3).Select(q => q.Question).ToList().ForEach(q => unit3.Add(new QuestionVM(q)));
            db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).Where(u => u.unit == 4).Select(q => q.Question).ToList().ForEach(q => unit4.Add(new QuestionVM(q)));
            db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).Where(u => u.unit == 5).Select(q => q.Question).ToList().ForEach(q => unit5.Add(new QuestionVM(q)));

            vM.QuestionsInUnit1 = unit1;
            vM.QuestionsInUnit2 = unit2;
            vM.QuestionsInUnit3 = unit3;
            vM.QuestionsInUnit4 = unit4;
            vM.QuestionsInUnit5 = unit5;

            vM.NoOfQuestionsInUnit1 = unit1.Count;
            vM.NoOfQuestionsInUnit2 = unit2.Count;
            vM.NoOfQuestionsInUnit3 = unit3.Count;
            vM.NoOfQuestionsInUnit4 = unit4.Count;
            vM.NoOfQuestionsInUnit5 = unit5.Count;
            return View(vM);
        }
        // POST: Teachers/AddQuestionToTest
        [HttpPost]
        public ActionResult AddQuestionToTest([Bind(Include = "faculty_id")] ExamPortal.Models.ViewModels.AddQuestionToTestVM vM)
        {
            int faculty_id = vM.faculty_id;

            return RedirectToAction("CreateTest");
        }
        [HttpGet]
        public ActionResult GetStartTimes(string date)
        {
            if (!string.IsNullOrWhiteSpace(date))
            {
                if (DateTime.TryParse(date, out DateTime parsedDate)) {
                    var repo = new Data.DateOfTestRepository();

                    IEnumerable<SelectListItem> times = repo.GetStartTimeOfTestOnDate(parsedDate);
                    return Json(times, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }
        [HttpGet]
        public ActionResult GetEndTimes(string startTime)
        {
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                if (DateTime.TryParse(startTime, out DateTime parsedDateTime))
                {
                    var repo = new Data.DateOfTestRepository();

                    IEnumerable<SelectListItem> times = repo.GetEndTimeOfTestByStartTime(parsedDateTime);
                    return Json(times, JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }
        // GET: Teachers/CreateTest
        public ActionResult CreateTest()
        {
            TestVM vM = new TestVM();
            Data.SubjectsRepository sRepo = new SubjectsRepository();
            vM.Subjects = sRepo.GetSubjects();
            Data.DateOfTestRepository testRepo = new DateOfTestRepository();
            vM.datesContainingTest = testRepo.getTestDatesAsString();
            vM.dateOfTest = DateTime.Now;
            vM.StartTimeOfTests = new List<SelectListItem>();
            vM.EndTimeOfTests = new List<SelectListItem>();
            return View(vM);
        }

        // POST: Teachers/CreateTest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTest([Bind(Include = "test_name,description,subject_code,dateOfTest,startTimeOfTest,endTimeOfTest")] TestVM vM)
        {
            int faculty_id = (int)Session["facultyId"];
            if (ModelState.IsValid)
            {
                Test test = new Test();
                test.creator = faculty_id;
                test.test_name = vM.test_name;
                test.description = vM.description;
                vM.start_datetime = vM.dateOfTest.Date.AddHours(vM.startTimeOfTest.Hour).AddMinutes(vM.startTimeOfTest.Minute);
                vM.end_datetime = vM.dateOfTest.Date.AddHours(vM.endTimeOfTest.Hour).AddMinutes(vM.endTimeOfTest.Minute);
                test.start_datetime = vM.start_datetime;
                test.end_datetime = vM.end_datetime;
                test.subject_code = vM.subject_code;
                db.Tests.Add(test);                
                await db.SaveChangesAsync();
                Test validTest = db.Tests.Where(t => 
                t.test_name == test.test_name).Where(t=>
                t.creator==test.creator).Where(t=>
                t.description == test.description).Where(t=>
                t.start_datetime == test.start_datetime).Where(t=>
                t.end_datetime == test.end_datetime).Single();
                try
                {// Add questions to test from
                    List<TempCreatedTest> tempTests = db.TempCreatedTests.Where(t => t.faculty_id == faculty_id).ToList();

                    tempTests.ForEach(q =>
                    {
                        Test_Question tq = new Test_Question();
                        tq.question_id = q.q_id;
                        tq.test_id = validTest.test_id;
                        tq.unit = q.unit;
                        db.Test_Question.Add(tq);
                    });
                    await db.SaveChangesAsync();
                    tempTests.ForEach(q =>
                    {
                        db.TempCreatedTests.Remove(q);
                    });
                    await db.SaveChangesAsync();
                }
                catch (Exception) {//On exception undo changes
                    db.Tests.Remove(validTest);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            else
            {
                Data.SubjectsRepository sRepo = new SubjectsRepository();
                vM.Subjects = sRepo.GetSubjects();
                Data.DateOfTestRepository testRepo = new DateOfTestRepository();
                vM.datesContainingTest = testRepo.getTestDatesAsString();
                vM.StartTimeOfTests = new List<SelectListItem>();
                vM.EndTimeOfTests = new List<SelectListItem>();
            }
            return View(vM);
        }

        public ActionResult AddQuestion()
        {
            QuestionVM vM = new QuestionVM();
            var subjectsRepository = new SubjectsRepository();
            var difficultyRepository = new DifficultyRepository();
            vM.Subjects = subjectsRepository.GetSubjects();
            vM.DifficultyLevels = difficultyRepository.GetDifficultyLevels();
            ViewBag.Title = "Add Question";
            vM.difficulty_level = Convert.ToByte(vM.DifficultyLevels.Skip(1).First().Value.ToString());
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddQuestion([Bind(Include = "question_text,option1,option2,option3,option4,use_option1,use_option2,use_option3,use_option4,is_option1_correct,is_option2_correct,is_option3_correct,is_option4_correct,subject_code,difficulty_level")] ExamPortal.Models.ViewModels.QuestionVM questionVM)
        {
            if (ModelState.IsValid)
            {
                Question question = QuestionVM.ConvertToQuestion(questionVM);
                question.Subject = await db.Subjects.FindAsync(questionVM.subject_code);
                question.subject_code = question.Subject.subject_code;
                question.creator = (int)Session["facultyId"];
                
                db.Questions.Add(question);
                
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("ViewQuestion");
            }

            return View(questionVM);
        }

        public ActionResult ViewQuestion()
        {
            IEnumerable<Question> viewModel = db.Questions.AsEnumerable();
            return View(viewModel);
        }

        public ActionResult CoordinatorDesk()
        {
            Models.ViewModels.CoordinatorVM viewModel = new Models.ViewModels.CoordinatorVM((int)Session["facultyId"]);
            return View(viewModel);
        }
        // GET: Teachers/AssignTest/facultyId=4
        public ActionResult AssignTest(int? facultyId)
        {
            if (facultyId.HasValue)
            {
                Models.ViewModels.AssignTestVM viewModel = new Models.ViewModels.AssignTestVM(facultyId.Value);

                return View(viewModel);
            }
            return View();
        }
        public ActionResult listClasses() {
            List<Class> classes = db.Classes.ToList();
            return View(classes);
        }
        
        public ActionResult AssignStudent(int scholarNo)
        {
            IEnumerable<Test> partialVM = new Models.ViewModels.AssignTestVM((int)Session["facultyId"]).TestsTaken(scholarNo);
            return PartialView("_TestTaken",partialVM);
        }

        public ActionResult StudentBriefDetails(int scholarNo)
        {
            AssignTestStudentPartial2 partialVM = new Models.AssignTestStudentPartial2(db.Students.Single(s=>s.scholar_no==scholarNo));
            return PartialView("_StudentBriefDetails", partialVM);
        }

        public JsonResult IsTeacherUsernameExists(string username)
        {
            //check if any of the username matches the username specified in the Parameter using the ANY extension method.  
            return Json(db.Teachers.Any(x => x.UserLogin.username == username), JsonRequestBehavior.AllowGet);
        }

        // GET: Teachers/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            Models.ViewModels.TeacherVM teacherVM = new Models.ViewModels.TeacherVM();
            teacherVM.faculty_id = teacher.faculty_id;
            teacherVM.faculty_name = teacher.faculty_name;
            teacherVM.UserLogin = teacher.UserLogin;
            teacherVM.user_id = teacher.UserLogin.user_id;
            teacherVM.dob = teacher.dob;
            teacherVM.username = teacher.UserLogin.username;
            teacherVM.password = teacher.UserLogin.password;
            teacherVM.graduate = teacher.graduate;
            teacherVM.post_graduate = teacher.post_graduate;
            if (!String.IsNullOrWhiteSpace(teacher.post_graduate))
            {
                if (teacher.year_post_graduated != null)
                {
                    teacherVM.year_post_graduated = teacher.year_post_graduated;
                }
            }
            teacherVM.year_graduated = teacher.year_graduated;
            teacherVM.dob = teacher.dob;
            teacherVM.blood_group = teacher.blood_group;
            teacherVM.residential_address = teacher.residential_address;
            teacherVM.mobile = teacher.mobile;
            teacherVM.served = teacher.served;
            return View(teacherVM);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "faculty_id,faculty_name,graduate,post_graduate,year_graduated,year_post_graduated,dob,blood_group,residential_address,mobile,served,username,password")] Models.ViewModels.TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = await db.Teachers.FindAsync(teacherVM.faculty_id);

                if (teacher == null)
                {
                    return HttpNotFound();
                }
                UserLogin user = await db.UserLogins.FindAsync(teacher.user_id);
                bool usernameChanged = false;
                bool passwordChanged = false;
                if (user.username != teacherVM.username) {
                usernameChanged = true;
                //check if a teacher with same username already exists
                bool assertUsernameExists = db.UserRoles.Where(u => u.role == "teacher").Any(t => t.UserLogin.username == teacherVM.username);
                if (assertUsernameExists)
                {
                    ModelState.AddModelError("IsTeacherUsernameExists", "Username already exists.");
                    return View(teacherVM);
                }
                }
                if (user.password != teacherVM.password)
                {
                passwordChanged = true;
                user.password = teacherVM.password;
                }
                //if login credentials changed, update database
                if (usernameChanged || passwordChanged) {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                }
                
                teacher.faculty_name = teacherVM.faculty_name;
                teacher.UserLogin = user;
                teacher.user_id = teacher.UserLogin.user_id;
                teacher.faculty_name = teacherVM.faculty_name;
                teacher.graduate = teacherVM.graduate;
                teacher.post_graduate = teacherVM.post_graduate;
                if (!String.IsNullOrWhiteSpace(teacherVM.post_graduate))
                {
                    if (teacherVM.year_post_graduated != null)
                    {
                        teacherVM.year_post_graduated = teacherVM.year_post_graduated;
                    }
                }
                teacher.year_graduated = teacherVM.year_graduated;
                teacher.dob = teacherVM.dob;
                teacher.blood_group = teacherVM.blood_group;
                teacher.residential_address = teacherVM.residential_address;
                teacher.mobile = teacherVM.mobile;
                teacher.served = teacherVM.served;
                //update database
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (((List<string>)Session["role"]).Contains("admin"))
                {
                    return RedirectToAction("IndexTeachers");
                }
                else {
                    return RedirectToAction("Index");
                }
            }
            return View(teacherVM);
        }
    // GET: Teachers/GetDisabledTimeRangesOfTestOnDate/dateTime
    public JsonResult GetDisabledTimeRangesOfTestOnDate(DateTime dateTime)
    {
        DateTime startTime = new DateTime(dateTime.Date.Ticks);//start time set to date and 12:00:00 after midnight
        startTime = startTime.AddHours(7);
        DateTime endTime = new DateTime(startTime.AddHours(12).Ticks);
        List<Tuple<DateTime, DateTime>> startNendTimesOfTests = new List<Tuple<DateTime, DateTime>>();
        using (var db = new ExamPortalEntities())
        {
            startNendTimesOfTests = db.Tests.Where(t => (t.start_datetime.Date == dateTime.Date)).Select(t => new Tuple<DateTime, DateTime>(t.start_datetime, t.end_datetime)).ToList();
        }

        List<string> allTimes = new List<string>();
        List<string> disabledTimes = new List<string>();
        bool isRangeStarted = false;
        string RangeStartTime = null;
        string RangeEndTime = null;
        bool starter = false;
        for (DateTime time = startTime; time.Ticks < endTime.Ticks; time = time.AddMinutes(30))
        {
            string strTime = String.Format("{0:t}",time);
            allTimes.Add(strTime);
            foreach (Tuple<DateTime, DateTime> timeFound in startNendTimesOfTests)
            {
                if ((time >= timeFound.Item1) && (time <= timeFound.Item2.AddMinutes(1)))
                {
                    if (!isRangeStarted)
                    {
                        if (!starter)
                        {
                            starter = true;
                            isRangeStarted = true;
                            RangeStartTime = strTime;
                        }
                        else
                        {
                            RangeEndTime = strTime;
                            disabledTimes.Add("['" + RangeStartTime + "','" + RangeEndTime + "']");
                            RangeStartTime = null;
                            RangeEndTime = null;
                        }
                    }
                }
                else
                {
                    isRangeStarted = false;
                }
            }
        }
        return Json(disabledTimes, JsonRequestBehavior.AllowGet);
    }

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

using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Controllers
{
    public class HomeController : Controller
    {
        ExamPortalEntities db = new ExamPortalEntities();
        // GET: Home
        public ActionResult Index()
        {
            /*string sessionId = System.Web.HttpContext.Current.Session.SessionID;
            Dictionary<string,string> mysessions = (Dictionary<string,string>)MvcApplication.sessions;
            Response.Write("<ol>");
            foreach(var item in mysessions.AsEnumerable())
            {
                Response.Write("<li>"+item.Value+"</li>");
            }
            Response.Write("</ol>");
            */
            return View();
        }

        // GET: Home/StudentLogin
        public ActionResult StudentLogin(string additional_message)
        {
            ViewBag.Title = "Student Login";
            ViewBag.Message = additional_message;
            StudentLogin vM = new StudentLogin();
            return View(vM);
        }
        
        // POST: Home/StudentLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentLogin([Bind(Include ="username,password")] StudentLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                //add spaces to user-password to make it compatiple with database
                userLogin.password = userLogin.password.PadRight(50);

                StudentLogin studentLogin = db.StudentLogins.SingleOrDefault(u => (u.username == userLogin.username) && (u.password == userLogin.password));
                if (studentLogin != null)
                {
                    Session["scholarNo"] = studentLogin.scholar_no;
                    Session["userId"] = studentLogin.user_id;
                    Session["role"] = new List<string>() { studentLogin.role };
                    Session["facultyId"] = -1;

                    return RedirectToAction("Index", "Students");
                }
                else
                {   //Student details missing
                    UserLogin user = db.UserLogins.SingleOrDefault(u => (u.username == userLogin.username) && (u.password == userLogin.password));
                    if (user != null)
                    {
                        string role = user.UserRoles.Single().role;
                        if (role == "student")
                        {
                            int id = user.user_id;
                            Session["userId"] = id;
                            Session["role"] = new List<string>() { role };
                            return RedirectToAction("NewStudent", "Students", new { id });
                        }
                    }
                }
            }

            ViewBag.Title = "Student Login Failed";
            ModelState.AddModelError("", "Invalid username or password");
            return View(userLogin);
        }
        
        // GET: Home/TeacherLogin
        public ActionResult TeacherLogin()
        {
            ViewBag.Title = "Teacher Login";
            TeacherLogin teacherLogin = new TeacherLogin();
            return View(teacherLogin);
        }

        // POST: Home/TeacherLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TeacherLogin([Bind(Include = "username,password")] TeacherLogin userLogin)
        {

            ViewBag.Title = "Teacher Login Failed";
            if (ModelState.IsValid)
            {
                List<TeacherLogin> teacherLogins = await db.TeacherLogins.Where(u => (u.username == userLogin.username) && (u.password == userLogin.password)).ToAsyncEnumerable().ToList();
                Session["scholarNo"] = -1;
                int userId = teacherLogins.Select(t => t.user_id).First();
                int facultyId = teacherLogins.Select(t => t.faculty_id).First();
                List<string> roles = new List<string>();
                teacherLogins.ForEach(t => roles.Add(t.role));
                Session["scholarNo"] = -1;
                Session["userId"] = userId;
                Session["facultyId"] = facultyId;
                Session["role"] = roles;
                return RedirectToAction("Index", "Teachers");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(userLogin);
        }

        // GET: Home/AdminLogin
        public ActionResult AdminLogin()
        {
            ViewBag.Title = "Admin Login";
            AdminLogin vM = new AdminLogin();
            return View(vM);
        }

        // POST: Home/AdminLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin([Bind(Include = "username,password")] AdminLogin userLogin)
        {
            ViewBag.Title = "Admin Login Failed";
            if (ModelState.IsValid)
            {
                AdminLogin adminLogin = db.AdminLogins.SingleOrDefault(u => (u.username == userLogin.username)&&(u.password == userLogin.password));
                if (adminLogin != null)
                {
                    Session["facultyId"] = -1;
                    Session["scholarNo"] = -1;
                    Session["userId"] = adminLogin.user_id;
                    Session["role"] = new List<string>() { adminLogin.role };
                    
                        return RedirectToAction("Index", "Admins");
                    
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(userLogin);
        }
        public ActionResult Logout() {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        // GET: About
        public ActionResult About()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}
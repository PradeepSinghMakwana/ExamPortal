using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ExamPortal.Models;

namespace ExamPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<string,string> sessions = new Dictionary<string, string>();
        static object sessionLock = new object();

        void Application_SessionStart()
        {
            /*lock (sessionLock)
            {
                sessions.Add(Session.SessionID);
            }*/
        }

        void Application_SessionEnd()
        {
            /*lock (sessionLock)
            {
                sessions.Remove(Session.SessionID);
            }*/
        }
        protected async void Application_Start()
        {
            Application.Lock();
            Application.Add("Sessions", sessions);
            Application.UnLock();
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //check if admin exists with role, username and password properly set in the database.
            ExamPortalEntities db = new ExamPortalEntities();
            //create admin if not exist in db
            if (!db.UserRoles.Any(u => u.role == "admin")) {
                //create user new user with username="admin1u" and password="admin1p"
                UserLogin user = new UserLogin();
                user.username = "admin1u";
                user.password = "admin1p";
                db.UserLogins.Add(user);
                await db.SaveChangesAsync();
                //set the role of new user to admin
                UserRole adminRole = new UserRole();
                UserLogin admin = db.UserLogins.Single(u => ((u.username == "admin1u") && (u.password == "admin1p")));
                adminRole.UserLogin = admin;
                adminRole.user_id = admin.user_id;
                adminRole.role = "admin";
                db.UserRoles.Add(adminRole);
                await db.SaveChangesAsync();
                
            }
        }
        protected void Session_Start() {
            lock (sessionLock)
            {
                sessions[Session.SessionID]=(Request.ServerVariables["ALL_RAW"]);
            }
        }
        protected void Session_End()
        {
            lock (sessionLock)
            { 
                sessions.Remove(Session.SessionID);
            }
        }
    }
}

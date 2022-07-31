using ExamPortal.Controllers;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal
{
    public class FilterConfig
    {
        public class SessionExpireFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;

                // check if session is supported
                if ((!(filterContext.Controller is HomeController))&&(HttpContext.Current.Session["role"] == null))
                {
                    // check if a new session id was generated
                    filterContext.Result = new RedirectResult("~");
                    return;
                }

                base.OnActionExecuting(filterContext);
            }
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SessionExpireFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}

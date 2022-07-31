using ExamPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.App_Start
{
    public class IdentityConfig
    {
        public object AuthenticationType { get; private set; }

        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(() => new ExamPortalEntities());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<UserRole>>(
                (options,context) => new RoleManager<UserRole>(
                    new RoleStore<UserRole>(context.Get<ExamPortalEntities>()))
                );
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Index")
            });
            
        }
    }
}
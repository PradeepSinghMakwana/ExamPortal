using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public class AppUserManager : UserManager<UserLogin>
    {
        public AppUserManager(IUserStore<UserLogin> store) : base(store)
        {
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context) {
            var manager = new AppUserManager(new UserStore<UserLogin>(context.Get<ExamPortalEntities>()));
            // optionaly configure the manager here...
            return manager;
        }
    }
}
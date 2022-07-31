using System;
using System.Collections.Generic;
using System.Web.Security;
using ExamPortal.Models;
using System.Linq;
using System.Web;

namespace ExamPortal
{
    public class MyRoleProvider : RoleProvider
    {

        
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (ExamPortalEntities db = new ExamPortalEntities())
            {
                var user = db.UserLogins.FirstOrDefault(x => x.username == username);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    string[] roles = user.UserRoles.Select(x => x.role).ToArray();
                    return roles;
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
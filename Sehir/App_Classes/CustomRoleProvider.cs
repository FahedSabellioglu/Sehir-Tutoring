using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Sehir.App_Classes
{

    public class CustomRoleProvider : RoleProvider
    {

        SehirTutoringEntities ctx = new SehirTutoringEntities();
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
            List<string> Roles = new List<string>();
            if(!string.IsNullOrEmpty(username))
            {
                User usr = ctx.Users.FirstOrDefault(x => x.mail == username);
                Roles = ctx.UserRoles(usr.id).ToList();
                AdminTable adminObject = ctx.AdminTables.FirstOrDefault(x => x.ID == usr.id);
                if (adminObject != null )
                {
                    Roles.Add("Admin");
                }

            }
            return Roles.ToArray();
            
        }

        public override string[] GetUsersInRole(string roleName)
        {


            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] Roles = GetRolesForUser(username);

            if (Roles.Contains(roleName))
            {
                return true;
            }
            return false;

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
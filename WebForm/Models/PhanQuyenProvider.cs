using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Core.BIZ;
using Core.DAL;
namespace WebForm.Models
{
    public class PhanQuyenProvider : RoleProvider
    {
        

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

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
            NguoiDung nd;
            var param = new Dictionary<string, dynamic>();
            param.Add(NguoiDungManager.Properties.TenNguoiDung, username);
            nd = NguoiDungManager.findBy(param).SingleOrDefault();
            if (nd != null)
            {
                return nd.getPhanQuyen();
            }
            return null;
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
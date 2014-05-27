namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetAllRoles()
        {
           var result = new string[] {};
           int index = 0;
           var roleList = BusinessLogicHelper._logic.GetAllRoles();
           foreach (var role in roleList)
           {
               result[index]=role.RoleName;
               index++;
           }

           return result;
        }

        public override string[] GetRolesForUser(string username)
        {
            var result = new List<string>();
            var account = BusinessLogicHelper._logic.GetAccount(username);
            var roleList = BusinessLogicHelper._logic.GetAccountRoles(account);
            foreach (var role in roleList)
            {
                result.Add(role.RoleName);                
            }

            return result.ToArray(); 
        }

        public static Account GetAccount(Guid id)
        {
            return BusinessLogicHelper._logic.GetAccount(id);
        }

        public static bool AddRoleToAccount(Guid AccountID, Guid RoleID)
        {
            return BusinessLogicHelper._logic.AddRoleToAccount(AccountID, RoleID);
        }
    }
}
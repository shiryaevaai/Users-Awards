namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    public class LoginModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        internal bool TryToLogin()
        {
            if (Username == "1" && Password == "1")
            {
                FormsAuthentication.RedirectFromLoginPage(Username, createPersistentCookie: true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static void CreateAccount(LoginModel model)
        {
            var account = new Account()
            {
                ID = new Guid(),
                Login = model.Username,
                Password = model.Password,
            };
            
            BusinessLogicHelper._logic.CreateAccount(account);
        }
    }
}
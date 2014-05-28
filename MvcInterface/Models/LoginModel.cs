namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;

    using MvcInterface.Models;

    public class LoginModel
    {
        [Required]
        [Remote("CheckAccountName", "Account")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 255 символов")]
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

        internal bool TryToLogin(string login, string password)
        {
            var account = BusinessLogicHelper._logic.GetAccount(login);

            if (account != null)
            {
                if (account.Login == login && account.Password == password)
                {
                    FormsAuthentication.RedirectFromLoginPage(login, createPersistentCookie: true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            //if (Username == "1" && Password == "1")
            //{
            //    FormsAuthentication.RedirectFromLoginPage(Username, createPersistentCookie: true);
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public static void CreateAccount(LoginModel model)
        {
            var account = new Account()
            {
                ID = Guid.NewGuid(),
                Login = model.Username,
                Password = model.Password,
            };
            
            BusinessLogicHelper._logic.CreateAccount(account);
        }

        public static bool CheckAccountName(string Username)
        {
            var list = BusinessLogicHelper._logic.GetAllAccounts();
            foreach (var user in list)
            {
                if (user.Login== Username)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
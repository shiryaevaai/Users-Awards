namespace MvcInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }


        internal bool TryToLogin()
        {
            //if (Username == "1" && Password == "1")
            //{
            //    FormsAuthentication.RedirectFromLoginPage()
                    return true;
            //}
        }
    }
}
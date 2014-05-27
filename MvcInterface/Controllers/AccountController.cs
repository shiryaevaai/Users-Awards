namespace MvcInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using MvcInterface.Models;
    using System.Web.UI;
    using EpamTask6_1.UserList.Entities;
    
    public class AccountController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = BusinessLogicHelper._logic.GetAllAccounts();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            // returnUrl
            if (ModelState.IsValid)
            {
                if (model.TryToLogin(model.Username, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            
            return PartialView();
        }

        public ActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout(ConfirmationModel model)
        {
          
            if (model.Confirm)
            {
                LoginModel.Logout();
            }
           
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Create
         
        public ActionResult CreateAccount()
        {
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult CreateAccount(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                LoginModel.CreateAccount(model);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        //
        // GET: /Account/Edit/5
         [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: /Account/Delete/5
         [Authorize(Roles = "Admin")]
        public ActionResult DeleteRole(Guid AccountID)
        {
            var model = MyRoleProvider.GetAccount(AccountID);
            model.RoleList = MyRoleProvider.GetRolesForUser(AccountID).ToList();
            return View(model);
        }

        // POST: /Account/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
         public ActionResult DeleteRole(Guid AccountID, Guid RoleID)
        {
            try
            {
                MyRoleProvider.DeleteRoleFromAccount(AccountID, RoleID);
                return RedirectToAction("Index","Account");
            }
            catch
            {
                var model = MyRoleProvider.GetAccount(AccountID);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(Guid AccountID)
        {
            var model = MyRoleProvider.GetAccount(AccountID);
            model.RoleList = MyRoleProvider.GetNoRolesForUser(AccountID).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(Guid AccountID, Guid RoleID)
        {
            var model = MyRoleProvider.GetAccount(AccountID);
            if (MyRoleProvider.AddRoleToAccount(AccountID, RoleID))
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(Guid id)
        {
            var model = MyRoleProvider.GetAccount(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetAccountRoles(Guid id)
        {
            var model = MyRoleProvider.GetRolesForUser(id);
            return PartialView(model);
        }
    }
}

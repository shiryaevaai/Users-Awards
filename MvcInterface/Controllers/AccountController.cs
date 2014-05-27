namespace MvcInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using MvcInterface.Models;
    using System.Web.UI;
    
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.TryToLogin())
                {
                    return Redirect(returnUrl);
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

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Edit/5

        [HttpPost]
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

        //
        // GET: /Account/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

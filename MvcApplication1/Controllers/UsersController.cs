namespace MvcApplication1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using System.IO;

    //using EpamTask6_1.UserList.DAL.Abstract;
    //using EpamTask6_1.UserList.DAL.Fake;
    //using EpamTask6_1.UserList.DAL.Files;
    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;
    using MvcApplication1.Models;

    public class UsersController : Controller
    {
        //
        // GET: /Users/
        //public static UserListLogic _logic = new UserListLogic();

        // watch all users
        public ActionResult Index()
        {
            var model = Users.GetAllUsers();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users model)
        {
            if (ModelState.IsValid)
            {
                Users.CreateUser(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Users model)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    Users.UpdateUser(model);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            //}

            //return View(model);
        }

        
        public ActionResult Delete(Guid id)
        {
            var model = Users.GetUser(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Users model)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    Users.DeleteUser(model);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            //}

           // return View(model);
        }

        public ActionResult UserAwards(Guid id)
        {
            var model = Users.GetUser(id);            
           // var user = Users.GetUser(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetUserAwards(Guid id)
        {
            var model = Users.GetUserAwards(id);
            return PartialView(model);
        }

        public ActionResult UploadAvatar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(HttpPostedFileBase image)
        {
            //image.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"av1"));
            FileWorker.SaveFile(image);
            return View();
        }

    }
}

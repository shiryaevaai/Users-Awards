namespace MvcInterface.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using MvcInterface.Models;
    using System.Web.UI;


    public class UsersController : Controller
    {
        //
        // GET: /Users/

        // watch all users
        public ActionResult Index()
        {
            var model = Users.GetAllUsers();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = Users.GetUser(id);
            return View(model);
        }

        public ActionResult GetUserImage(string path)
        {
            return File(FileWorker.GetFile(path), "image/jpeg", path);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(Users model)
        {
            if (ModelState.IsValid)
            {
                Users.CreateUser(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckUserName(string Name)
        {
            var result = Users.CheckUserName(Name);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("User with this name already exists.", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var model = Users.GetUser(id);
            return View(model);
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult Delete(Users model)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                Users.DeleteUser(model.ID);
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

        //public ActionResult AddAwardToUser(Guid id)
        //{
        //    var model = Users.GetUser(id);
        //    // var user = Users.GetUser(id);
        //    return View(model);
        //}


        public ActionResult UploadImage(Guid id)
        {
            return View();
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult UploadImage(Guid id, HttpPostedFileBase image)
        {
            //image.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"av1"));
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, id.ToString());
            FileWorker.SaveFile(image, path);
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(Users user)
        {
            return File(FileWorker.GetFile(user.ImageAddr), "image/jpeg", user.ImageAddr);
        }

    }
}

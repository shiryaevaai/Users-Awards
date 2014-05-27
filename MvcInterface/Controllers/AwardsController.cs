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

    public class AwardsController : Controller
    {
        //
        // GET: /Awards/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = Awards.GetAllAwards();
            return View(model);
        }

        //
        // GET: /Awards/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Awards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Awards model)
        {
            if (ModelState.IsValid)
            {
                Awards.CreateAward(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckAwardTitle(string Title)
        {
            var result = Awards.CheckAwardTitle(Title);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Award with this Title already exists.", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAwardImage(string path)
        {
            return File(FileWorker.GetFile(path), "image/jpeg", path);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage(Guid id)
        {
            Guid AwardID = id;
            return View(AwardID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage(Guid id, HttpPostedFileBase image)
        {
            Guid AwardID = id;
            try
            {
                Awards award = Awards.GetAward(id);
                string path = Path.Combine(Awards.ImageDirectory, award.ID.ToString());
                FileWorker.SaveFile(image, path);
                award.SetImage();
            }
            catch
            {  
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveImage(Guid id)
        {
            Awards award = Awards.GetAward(id);
            award.RemoveImage();
            Guid AwardID = id;
            return RedirectToAction("Index");            
        }
    }
}

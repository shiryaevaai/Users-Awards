using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcInterface.Models;

namespace MvcInterface.Controllers
{
    public class FilesController : Controller
    {
        //
        // GET: /Files/

        public ActionResult Index()
        {
            var desc = FileWorker.FileName == null? null: new FileDescription{Name = FileWorker.FileName};
        
            return View(desc);
        }

        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase image)
        {
            //image.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"av1"));
            FileWorker.SaveFile(image);
            return RedirectToAction("Index");
        }

        public ActionResult GetImage()
        {
            return File(FileWorker.GetFile("D:\\myfile"), "image/jpeg", "D:\\myfile");
        }


    }
}

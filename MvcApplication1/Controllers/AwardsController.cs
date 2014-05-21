namespace MvcApplication1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using EpamTask6_1.UserList.Entities;
    using EpamTask6_1.UserList.Logic;
    using MvcApplication1.Models;

    public class AwardsController : Controller
    {
        //
        // GET: /Awards/

        public ActionResult Index()
        {
            var model = Awards.GetAllAwards();
            return View(model);
        }

        //
        // GET: /Awards/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Awards/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Awards/Create
        [HttpPost]
        public ActionResult Create(Awards model)
        {
            if (ModelState.IsValid)
            {
                Awards.CreateAward(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Awards/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Awards/Edit/5

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
        // GET: /Awards/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Awards/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

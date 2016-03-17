using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HashController : Controller
    {
        // GET: Hash
        public ActionResult Index()
        {
            return View();
        }

        /*
        // GET: Hash/Details/5
        public ActionResult Details()
        {
            var MD5 = new Models.MD5 { Value = "asdasdsadsacsa" };
            return View(MD5);
        }
        */

        // GET: Hash/Details/5
        public ActionResult Details(Models.customMD5 hash)
        {
            return View(hash);
        }

        public ActionResult ReverseHash(Models.reverseHash hash)
        {
            return View(hash);
        }

        // GET: Hash/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hash/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hash/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hash/Edit/5
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

        // GET: Hash/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hash/Delete/5
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
